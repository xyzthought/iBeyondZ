using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using BLL.BusinessObject;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace DAL.Component
{
    public class ManufacturerDB
    {
        Database dBase = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");

        /// <summary>
        /// Adds Manufacturer in the Database
        /// </summary>
        /// <param name="vobjManufacturer">Manufacturer Data Object</param>
        public void Add(ref Manufacturer vobjManufacturer)
        {
            DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_InsertManufacturer");
            dBase.AddInParameter(objCmd, "@CompanyName", DbType.String, vobjManufacturer.CompanyName);
            dBase.AddInParameter(objCmd, "@ContactFirstName", DbType.String, vobjManufacturer.ContactFirstName);
            dBase.AddInParameter(objCmd, "@ContactLastName", DbType.String, vobjManufacturer.ContactLastName);
            dBase.AddInParameter(objCmd, "@Address", DbType.String, vobjManufacturer.Address);
            dBase.AddInParameter(objCmd, "@ZIP", DbType.String, vobjManufacturer.ZIP);
            dBase.AddInParameter(objCmd, "@City", DbType.String, vobjManufacturer.City);
            dBase.AddInParameter(objCmd, "@Country", DbType.String, vobjManufacturer.Country);
            dBase.AddInParameter(objCmd, "@Phone", DbType.String, vobjManufacturer.Phone);
            dBase.AddInParameter(objCmd, "@Email", DbType.String, vobjManufacturer.Email);
            dBase.AddInParameter(objCmd, "@CreatedBy", DbType.Int32, vobjManufacturer.CreatedBy);
            dBase.AddOutParameter(objCmd, "@MessageID", DbType.Int32, 4);
            dBase.AddOutParameter(objCmd, "@Message", DbType.String, 255);
            dBase.ExecuteNonQuery(objCmd);

            vobjManufacturer.ReturnValue = (int)dBase.GetParameterValue(objCmd, "@MessageID");
            vobjManufacturer.ReturnMessage = (string)dBase.GetParameterValue(objCmd, "@Message");

        }

        /// <summary>
        /// Update Manufacturer in Database
        /// </summary>
        /// <param name="vobjManufacturer">Manufacturer Data Object</param>
        public void Update(ref Manufacturer vobjManufacturer)
        {
            DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_UpdateManufacturer");
            dBase.AddInParameter(objCmd, "@ManufacturerID", DbType.Int32, vobjManufacturer.ManufacturerID);
            dBase.AddInParameter(objCmd, "@CompanyName", DbType.String, vobjManufacturer.CompanyName);
            dBase.AddInParameter(objCmd, "@ContactFirstName", DbType.String, vobjManufacturer.ContactFirstName);
            dBase.AddInParameter(objCmd, "@ContactLastName", DbType.String, vobjManufacturer.ContactLastName);
            dBase.AddInParameter(objCmd, "@Address", DbType.String, vobjManufacturer.Address);
            dBase.AddInParameter(objCmd, "@ZIP", DbType.String, vobjManufacturer.ZIP);
            dBase.AddInParameter(objCmd, "@City", DbType.String, vobjManufacturer.City);
            dBase.AddInParameter(objCmd, "@Country", DbType.String, vobjManufacturer.Country);
            dBase.AddInParameter(objCmd, "@Phone", DbType.String, vobjManufacturer.Phone);
            dBase.AddInParameter(objCmd, "@Email", DbType.String, vobjManufacturer.Email);
            dBase.AddInParameter(objCmd, "@UpdatedBy", DbType.Int32, vobjManufacturer.UpdatedBy);
            dBase.AddOutParameter(objCmd, "@MessageID", DbType.Int32, 4);
            dBase.AddOutParameter(objCmd, "@Message", DbType.String, 255);
            dBase.ExecuteNonQuery(objCmd);

            vobjManufacturer.ReturnValue = (int)dBase.GetParameterValue(objCmd, "@MessageID");
            vobjManufacturer.ReturnMessage = (string)dBase.GetParameterValue(objCmd, "@Message");


        }

        /// <summary>
        /// Delete Manufacturer having supplied Member ID
        /// </summary>
        /// <param name="vobjManufacturer">Manufacturer Data Object having Manufacturer ID</param>
        public void Delete(ref Manufacturer vobjManufacturer)
        {
            DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_DeleteManufacturer");
            dBase.AddInParameter(objCmd, "@ManufacturerID", DbType.Int32, vobjManufacturer.ManufacturerID);
            dBase.AddOutParameter(objCmd, "@MessageID", DbType.Int32, 4);
            dBase.AddOutParameter(objCmd, "@Message", DbType.String, 255);
            dBase.ExecuteNonQuery(objCmd);

            vobjManufacturer.ReturnValue = (int)dBase.GetParameterValue(objCmd, "@MessageID");
            vobjManufacturer.ReturnMessage = (string)dBase.GetParameterValue(objCmd, "@Message");
            
         }

        /// <summary>
        /// Change status of Manufacturer from Active to Inactive and vice versa
        /// </summary>
        /// <param name="vblnIsActive"></param>
        /// <param name="vintManufacturerID"></param>
        public void ChangeManufacturerStatus(bool vblnIsActive, int vintManufacturerID)
        {
            object[] mParams = {
                                    new SqlParameter("@ManufacturerID", SqlDbType.Int),
                                    new SqlParameter("@IsActive", SqlDbType.Bit)
                                };
            mParams[0] = vintManufacturerID;
            mParams[1]= vblnIsActive;
            dBase.ExecuteNonQuery("sprocCS_ChangeManufacturerStatus", mParams);
        
        }

        /// <summary>
        /// Returns all the Manufacturer from Database
        /// </summary>
        /// <returns>Manufacturer collection</returns>
        public List<Manufacturer> GetAll(PageInfo vobjPageInfo)
        {
            object[] mParams = {
                                        new SqlParameter("@SortColumnName", SqlDbType.NVarChar),                                              
                                        new SqlParameter("@SortDirection", SqlDbType.NVarChar),
                                        new SqlParameter("@SearchText", SqlDbType.NVarChar)
                                };

            mParams[0] = vobjPageInfo.SortColumnName;
            mParams[1] = vobjPageInfo.SortDirection;
            mParams[2] = vobjPageInfo.SearchText;

            List<Manufacturer> lstManufacturer = new List<Manufacturer>();
            using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetAllManufacturer", mParams))
            {

                while (reader.Read())
                {
                    Manufacturer objManufacturer = new Manufacturer();

                    if (reader["ManufacturerID"] != DBNull.Value)
                        objManufacturer.ManufacturerID = Convert.ToInt32(reader["ManufacturerID"]);

                    if (reader["CompanyName"] != DBNull.Value)
                        objManufacturer.CompanyName = Convert.ToString(reader["CompanyName"]);

                    if (reader["ContactFirstName"] != DBNull.Value)
                        objManufacturer.ContactFirstName = Convert.ToString(reader["ContactFirstName"]);

                    if (reader["ContactLastName"] != DBNull.Value)
                        objManufacturer.ContactLastName = Convert.ToString(reader["ContactLastName"]);

                    if (reader["Address"] != DBNull.Value)
                        objManufacturer.Address = Convert.ToString(reader["Address"]);

                    if (reader["ZIP"] != DBNull.Value)
                        objManufacturer.ZIP = Convert.ToString(reader["ZIP"]);

                    if (reader["City"] != DBNull.Value)
                        objManufacturer.City = Convert.ToString(reader["City"]);

                    if (reader["Country"] != DBNull.Value)
                        objManufacturer.Country = Convert.ToString(reader["Country"]);

                    if (reader["Phone"] != DBNull.Value)
                        objManufacturer.Phone = Convert.ToString(reader["Phone"]);

                    if (reader["Email"] != DBNull.Value)
                        objManufacturer.Email = Convert.ToString(reader["Email"]);

                    if (reader["IsActive"] != DBNull.Value)
                        objManufacturer.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                    lstManufacturer.Add(objManufacturer);

                }
            }
            return lstManufacturer;
        }

        /// <summary>
        /// Returns Manufacturer details of supplied Manufacturer id
        /// </summary>
        /// <param name="vobjManufacturer">Manufacturer Data Object</param>
        public void GetByID(ref Manufacturer vobjManufacturer)
        {
            object[] mParams = {
                                    new SqlParameter("@ManufacturerID", SqlDbType.Int)
                                };
            mParams[0] = vobjManufacturer.ManufacturerID;
            using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetManufacturerByID", mParams))
            {

                while (reader.Read())
                {
                   
                    if (reader["CompanyName"] != DBNull.Value)
                        vobjManufacturer.CompanyName = Convert.ToString(reader["CompanyName"]);

                    if (reader["ContactFirstName"] != DBNull.Value)
                        vobjManufacturer.ContactFirstName = Convert.ToString(reader["ContactFirstName"]);

                    if (reader["ContactLastName"] != DBNull.Value)
                        vobjManufacturer.ContactLastName = Convert.ToString(reader["ContactLastName"]);

                    if (reader["Address"] != DBNull.Value)
                        vobjManufacturer.Address = Convert.ToString(reader["Address"]);

                    if (reader["ZIP"] != DBNull.Value)
                        vobjManufacturer.ZIP = Convert.ToString(reader["ZIP"]);

                    if (reader["City"] != DBNull.Value)
                        vobjManufacturer.City = Convert.ToString(reader["City"]);

                    if (reader["Country"] != DBNull.Value)
                        vobjManufacturer.Country = Convert.ToString(reader["Country"]);

                    if (reader["Phone"] != DBNull.Value)
                        vobjManufacturer.Phone = Convert.ToString(reader["Phone"]);

                    if (reader["Email"] != DBNull.Value)
                        vobjManufacturer.Email = Convert.ToString(reader["Email"]);

                    if (reader["IsActive"] != DBNull.Value)
                        vobjManufacturer.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                }

            }
        }
    }
}
