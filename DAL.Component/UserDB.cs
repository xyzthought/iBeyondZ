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
using System.Diagnostics;


namespace DAL.Component
{
    public class UserDB
    {
        Database dBase = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");

        public void AuthenticationValidation(ref User vObjUserInfo)
        {

            object[] mParams = {
                                        new SqlParameter("@LoginID", SqlDbType.NVarChar),    
                                        new SqlParameter("@LoginPassword", SqlDbType.NVarChar),    
                                };

            mParams[0] = vObjUserInfo.LoginID;
            mParams[1] = vObjUserInfo.LoginPassword;

            using (IDataReader reader = dBase.ExecuteReader("sprocCS_AuthenticationValidation", mParams))
            {

                while (reader.Read())
                {
                    if (reader["UserID"] != DBNull.Value)
                        vObjUserInfo.UserID = Convert.ToInt32(reader["UserID"]);

                    if (reader["LoginID"] != DBNull.Value)
                        vObjUserInfo.LoginID = Convert.ToString(reader["LoginID"]);

                    if (reader["UserTypeID"] != DBNull.Value)
                        vObjUserInfo.UserTypeID = Convert.ToInt32(reader["UserTypeID"]);

                    if (reader["UserType"] != DBNull.Value)
                        vObjUserInfo.UserType = Convert.ToString(reader["UserType"]);

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

        #region Get All User
        public List<User> GetAllUser(List<User> objData, PageInfo vobjPageInfo)
        {
            List<User> lstobjUser = new List<User>();
            try
            {

                object[] mParams = {
                                        new SqlParameter("@SortColumnName", SqlDbType.NVarChar),                                              
                                        new SqlParameter("@SortDirection", SqlDbType.NVarChar),
                                        new SqlParameter("@SearchText", SqlDbType.NVarChar)
                                };

                mParams[0] = vobjPageInfo.SortColumnName;
                mParams[1] = vobjPageInfo.SortDirection;
                mParams[2] = vobjPageInfo.SearchText;

                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetAllAppUser", mParams))
                {
                    while (reader.Read())
                    {
                        lstobjUser.Add(PopulateUser(reader));
                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return lstobjUser;
        }

        private User PopulateUser(IDataReader drData)
        {
            User objUser = new User();
            try
            {

                if (FieldExists(drData, "UserID") && drData["UserID"] != DBNull.Value)
                {
                    objUser.UserID = Convert.ToInt32(drData["UserID"]);
                }
                if (FieldExists(drData, "UserTypeID") && drData["UserTypeID"] != DBNull.Value)
                {
                    objUser.UserTypeID = Convert.ToInt32(drData["UserTypeID"]);
                }

                if (FieldExists(drData, "UserType") && drData["UserType"] != DBNull.Value)
                {
                    objUser.UserType = Convert.ToString(drData["UserType"]);
                }

                if (FieldExists(drData, "LoginID") && drData["LoginID"] != DBNull.Value)
                {
                    objUser.LoginID = Convert.ToString(drData["LoginID"]);
                }

                if (FieldExists(drData, "LoginPassword") && drData["LoginPassword"] != DBNull.Value)
                {
                    objUser.LoginPassword = Convert.ToString(drData["LoginPassword"]);
                }

                if (FieldExists(drData, "FirstName") && drData["FirstName"] != DBNull.Value)
                {
                    objUser.FirstName = Convert.ToString(drData["FirstName"]);
                }

                if (FieldExists(drData, "LastName") && drData["LastName"] != DBNull.Value)
                {
                    objUser.LastName = Convert.ToString(drData["LastName"]);
                }

                if (FieldExists(drData, "LastLoggedIn") && drData["LastLoggedIn"] != DBNull.Value)
                {
                    objUser.LastLoggedIn = Convert.ToDateTime(drData["LastLoggedIn"]);
                }

                if (FieldExists(drData, "CommunicationEmailID") && drData["CommunicationEmailID"] != DBNull.Value)
                {
                    objUser.CommunicationEmailID = Convert.ToString(drData["CommunicationEmailID"]);
                }

                if (FieldExists(drData, "CreatedOn") && drData["CreatedOn"] != DBNull.Value)
                {
                    objUser.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                }


                if (FieldExists(drData, "IsDeleted") && drData["IsDeleted"] != DBNull.Value)
                {
                    objUser.IsDeleted = Convert.ToBoolean(drData["IsDeleted"]);
                }

                if (FieldExists(drData, "UpdatedOn") && drData["UpdatedOn"] != DBNull.Value)
                {
                    objUser.UpdatedOn = Convert.ToDateTime(drData["UpdatedOn"]);
                }

            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return objUser;
        }
        #endregion


        public bool FieldExists(IDataReader reader, string fieldName)
        {
            try
            {
                reader.GetSchemaTable().DefaultView.RowFilter = string.Format("ColumnName= '{0}'", fieldName);
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return (reader.GetSchemaTable().DefaultView.Count > 0);
        }


        public List<UserTypeBO> GetAllUserType()
        {
            List<UserTypeBO> objUserType = new List<UserTypeBO>();
            UserTypeBO objUT = null;
            try
            {
                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetAllUserType"))
                {
                    while (reader.Read())
                    {
                        objUT = new UserTypeBO();
                        if (reader["UsertypeID"] != DBNull.Value)
                            objUT.UsertypeID = Convert.ToInt32(reader["UsertypeID"]);

                        if (reader["UserType"] != DBNull.Value)
                            objUT.UserType = Convert.ToString(reader["UserType"]);
                        objUserType.Add(objUT);
                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return objUserType;
        }


        public Message InsertUpdatePlatformUser(User objUser)
        {
            Message objMessage = new Message();
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_InsertUpdatePlatformUser");
                dBase.AddInParameter(objCmd, "@UserID", DbType.Int32, objUser.UserID);
                dBase.AddInParameter(objCmd, "@UserTypeID", DbType.Int32, objUser.UserTypeID);
                dBase.AddInParameter(objCmd, "@FirstName", DbType.String, objUser.FirstName);
                dBase.AddInParameter(objCmd, "@LastName", DbType.String, objUser.LastName);
                dBase.AddInParameter(objCmd, "@CommunicationEmailID", DbType.String, objUser.CommunicationEmailID);
                dBase.AddInParameter(objCmd, "@LoginID", DbType.String, objUser.LoginID);
                dBase.AddInParameter(objCmd, "@LoginPassword", DbType.String, objUser.LoginPassword);
                dBase.AddOutParameter(objCmd, "@ReturnValue", DbType.Int32, 4);
                dBase.AddOutParameter(objCmd, "@ReturnMessage", DbType.String, 255);
                dBase.ExecuteNonQuery(objCmd);

                objMessage.ReturnValue = (int)dBase.GetParameterValue(objCmd, "@ReturnValue");
                objMessage.ReturnMessage = (string)dBase.GetParameterValue(objCmd, "@ReturnMessage");
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return objMessage;
        }

        private static void CheckConnectionState(SqlConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }

        public List<User> GetPlatformUserByUserID(ref User objUser)
        {
            List<User> lstobjUser = new List<User>();
            try
            {

                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_GetPlatformUserByUserID");
                dBase.AddInParameter(objCmd, "@UserID", DbType.Int32, objUser.UserID);

                using (IDataReader reader = dBase.ExecuteReader(objCmd))
                {
                    while (reader.Read())
                    {
                        lstobjUser.Add(PopulateUser(reader));

                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return lstobjUser;
        }

        public Message DeletePlatformUser(User objUser)
        {
            Message objMessage = new Message();
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_DeletePlatformUser");
                dBase.AddInParameter(objCmd, "@UserID", DbType.Int32, objUser.UserID);
                dBase.AddOutParameter(objCmd, "@ReturnValue", DbType.Int32, 4);
                dBase.AddOutParameter(objCmd, "@ReturnMessage", DbType.String, 255);
                dBase.ExecuteNonQuery(objCmd);

                objMessage.ReturnValue = (int)dBase.GetParameterValue(objCmd, "@ReturnValue");
                objMessage.ReturnMessage = (string)dBase.GetParameterValue(objCmd, "@ReturnMessage");
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return objMessage;
        }



        public Message ChangePassword(User objUser, string vstrNewPassword)
        {
            Message objMessage = new Message();
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_ChangePassword");
                dBase.AddInParameter(objCmd, "@UserID", DbType.Int32, objUser.UserID);
                dBase.AddInParameter(objCmd, "@LoginPassword", DbType.String, objUser.LoginPassword);
                dBase.AddInParameter(objCmd, "@NewPassword", DbType.String, vstrNewPassword);
                dBase.AddOutParameter(objCmd, "@ReturnValue", DbType.Int32, 4);
                dBase.AddOutParameter(objCmd, "@ReturnMessage", DbType.String, 255);
                dBase.ExecuteNonQuery(objCmd);

                objMessage.ReturnValue = (int)dBase.GetParameterValue(objCmd, "@ReturnValue");
                objMessage.ReturnMessage = (string)dBase.GetParameterValue(objCmd, "@ReturnMessage");
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return objMessage;
        }

        public User ChangeAccountInformation(ref User objUser)
        {

            User lstobjUser = new User();
            try
            {

                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_ChangeAccountInformation");
                dBase.AddInParameter(objCmd, "@UserID", DbType.Int32, objUser.UserID);
                dBase.AddInParameter(objCmd, "@FirstName", DbType.String, objUser.FirstName);
                dBase.AddInParameter(objCmd, "@LastName", DbType.String, objUser.LastName);
                dBase.AddInParameter(objCmd, "@CommunicationEmailID", DbType.String, objUser.CommunicationEmailID);

                using (IDataReader reader = dBase.ExecuteReader(objCmd))
                {
                    while (reader.Read())
                    {
                        lstobjUser=PopulateUser(reader);

                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return lstobjUser;
        }

        
    }
}