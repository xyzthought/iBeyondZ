using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mail;
using System.ComponentModel;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using BLL.BusinessObject;
using CSWeb.Utility;


namespace DAL.Component
{
    public class UserDB
    {
        Database dBase = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");

        public void AuthenticationValidation(ref User vObjUserInfo)
        {
            
            object[] mParams = {
                                        new SqlParameter("@LoginID", SqlDbType.Int),    
                                        new SqlParameter("@Password", SqlDbType.Int),    
                                };

            mParams[0] = vObjUserInfo.LoginID;
            mParams[1] = vObjUserInfo.LoginPassword;

            using (IDataReader reader = dBase.ExecuteReader("sprocCSWeb_AuthenticationValidation", mParams))
            {

                while (reader.Read())
                {
                    if (reader["UserID"] != DBNull.Value)
                    vObjUserInfo.UserID = Convert.ToInt32(reader["UserID"]);

                    if (reader["LoginID"] != DBNull.Value)
                        vObjUserInfo.LoginID = Convert.ToString(reader["LoginID"]);

                    if (reader["LoginPassword"] != DBNull.Value)
                        vObjUserInfo.LoginPassword = Convert.ToString(reader["LoginPassword"]);

                    if (reader["FirstName"] != DBNull.Value)
                        vObjUserInfo.FirstName = Convert.ToString(reader["FirstName"]);

                    if (reader["LastName"] != DBNull.Value)
                        vObjUserInfo.LastName = Convert.ToString(reader["LastName"]);

                    if (reader["CommunicationEmailID"] != DBNull.Value)
                        vObjUserInfo.CommunicationEmailID = Convert.ToString(reader["CommunicationEmailID"]);

                    if (reader["LastLoggedIn"] != DBNull.Value)
                        vObjUserInfo.LastLoggedIn = Convert.ToDateTime(reader["LastLoggedIn"]);
                }
            }
            
        }

        #region Save Data

        public int SaveData(int kk)
        {
            Database dBase = EnterpriseLibraryContainer.Current.GetInstance<Database>("BOLDSN");
            try
            {
                int i = 0;
                string conString = dBase.ConnectionString;
                using (var conn = new SqlConnection(conString))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Open();
                    using (var cmd = new SqlCommand("sprocCS_SaveData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        var ParamUserPage = cmd.Parameters.AddWithValue("@Data", kk);
                        ParamUserPage.SqlDbType = SqlDbType.Structured;

                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();

                    return i;
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        # endregion
        
        #region Return Data Table
        public DataTable GetSourceData()
        {
            DataTable dtData = new DataTable();
            try
            {
                using (DbConnection connection = dBase.CreateConnection())
                {
                    connection.Open();
                    DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_");
                    DataSet dsData = dBase.ExecuteDataSet(objCmd);
                    dtData = dsData.Tables[0];
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                SendMail.MailMessage("Error", ex.ToString());
            }

            return dtData;
        } 
        #endregion

        #region Bulk Insert
        public void SaveSourceDataTable(DataTable dtSource)
        {
            DataTable dtPrevData = new DataTable();

            try
            {
                using (DbConnection connection = dBase.CreateConnection())
                {
                    connection.Open();
                    DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_");
                    DataSet dsData = dBase.ExecuteDataSet(objCmd);
                    dtPrevData = dsData.Tables[0];

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)connection))
                    {
                        bulkCopy.DestinationTableName = "ddd";

                        // needs a column mapping as there a an identity column created
                        for (int i = 0; i < dtSource.Columns.Count; i++)
                        {
                            if (i > 0)
                                bulkCopy.ColumnMappings.Add(dtSource.Columns[i - 1].ToString(), dtSource.Columns[i - 1].ToString());
                            if (i == dtSource.Columns.Count - 1)
                                bulkCopy.ColumnMappings.Add(dtSource.Columns[i].ToString(), dtSource.Columns[i].ToString());
                        }

                        bulkCopy.WriteToServer(dtSource);
                        bulkCopy.Close();
                    }
                    connection.Close();
                }


            }
            catch (Exception ex)
            {

                SendMail.MailMessage("Error", ex.ToString());
                using (DbConnection connection = dBase.CreateConnection())
                {
                    connection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)connection))
                    {
                        bulkCopy.DestinationTableName = "dd";

                        // needs a column mapping as there a an identity column created
                        for (int i = 0; i < dtPrevData.Columns.Count; i++)
                        {
                            if (i > 0)
                                bulkCopy.ColumnMappings.Add(dtPrevData.Columns[i - 1].ToString(), dtPrevData.Columns[i - 1].ToString());
                            if (i == dtSource.Columns.Count - 1)
                                bulkCopy.ColumnMappings.Add(dtPrevData.Columns[i].ToString(), dtPrevData.Columns[i].ToString());
                        }

                        bulkCopy.WriteToServer(dtPrevData);
                        bulkCopy.Close();
                    }
                    connection.Close();
                }

            }

        }
        #endregion

       
    }
}