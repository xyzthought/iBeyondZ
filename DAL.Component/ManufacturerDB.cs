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
            SqlParameter[] mParams = {
                                    new SqlParameter("@CompanyName", SqlDbType.NVarChar), 
                                    new SqlParameter("@ContactFirstName", SqlDbType.NVarChar),
                                    new SqlParameter("@ContactLastName", SqlDbType.NVarChar),
                                    new SqlParameter("@Address", SqlDbType.NVarChar),
                                    new SqlParameter("@ZIP", SqlDbType.NVarChar),
                                    new SqlParameter("@City", SqlDbType.NVarChar),
                                    new SqlParameter("@Phone", SqlDbType.NVarChar),
                                    new SqlParameter("@Email", SqlDbType.NVarChar),
                                    new SqlParameter("@CreatedBy", SqlDbType.Int),
                                    new SqlParameter("@MessageID", SqlDbType.Int),
                                    new SqlParameter("@Message", SqlDbType.NVarChar)
                                };

            mParams[0].Value = vobjManufacturer.CompanyName;
            mParams[1].Value = vobjManufacturer.ContactFirstName;
            mParams[2].Value = vobjManufacturer.ContactLastName;
            mParams[3].Value = vobjManufacturer.Address;
            mParams[4].Value = vobjManufacturer.ZIP;
            mParams[5].Value = vobjManufacturer.City;
            mParams[6].Value = vobjManufacturer.Phone;
            mParams[7].Value = vobjManufacturer.Email;
            mParams[8].Value = vobjManufacturer.CreatedBy;
            mParams[9].Direction = ParameterDirection.Output;
            mParams[10].Direction = ParameterDirection.Output;

            dBase.ExecuteNonQuery("sprocCS_InsertManufacturer", mParams);

            vobjManufacturer.ReturnValue = Convert.ToInt32(mParams[9].Value.ToString());
            vobjManufacturer.ReturnMessage = mParams[9].Value.ToString();

        }

        /// <summary>
        /// Update Manufacturer in Database
        /// </summary>
        /// <param name="vobjManufacturer">Manufacturer Data Object</param>
        public void Update(ref Manufacturer vobjManufacturer)
        {
            SqlParameter[] mParams = {
                                    new SqlParameter("@ManufacturerID", SqlDbType.Int), 
                                    new SqlParameter("@CompanyName", SqlDbType.NVarChar), 
                                    new SqlParameter("@ContactFirstName", SqlDbType.NVarChar),
                                    new SqlParameter("@ContactLastName", SqlDbType.NVarChar),
                                    new SqlParameter("@Address", SqlDbType.NVarChar),
                                    new SqlParameter("@ZIP", SqlDbType.NVarChar),
                                    new SqlParameter("@City", SqlDbType.NVarChar),
                                    new SqlParameter("@Phone", SqlDbType.NVarChar),
                                    new SqlParameter("@Email", SqlDbType.NVarChar),
                                    new SqlParameter("@UpdatedBy", SqlDbType.Int),
                                    new SqlParameter("@MessageID", SqlDbType.Int),
                                    new SqlParameter("@Message", SqlDbType.NVarChar)
                                };
            mParams[0].Value = vobjManufacturer.ManufacturerID;
            mParams[1].Value = vobjManufacturer.CompanyName;
            mParams[2].Value = vobjManufacturer.ContactFirstName;
            mParams[3].Value = vobjManufacturer.ContactLastName;
            mParams[4].Value = vobjManufacturer.Address;
            mParams[5].Value = vobjManufacturer.ZIP;
            mParams[6].Value = vobjManufacturer.City;
            mParams[7].Value = vobjManufacturer.Phone;
            mParams[8].Value = vobjManufacturer.Email;
            mParams[9].Value = vobjManufacturer.UpdatedBy;
            mParams[10].Direction = ParameterDirection.Output;
            mParams[11].Direction = ParameterDirection.Output;

            dBase.ExecuteNonQuery("sprocCS_UpdateManufacturer", mParams);

            vobjManufacturer.ReturnValue = Convert.ToInt32(mParams[10].Value.ToString());
            vobjManufacturer.ReturnMessage = mParams[11].Value.ToString();

        }

        /// <summary>
        /// Delete Manufacturer having supplied Member ID
        /// </summary>
        /// <param name="vobjManufacturer">Manufacturer Data Object having Manufacturer ID</param>
        public void Delete(ref Manufacturer vobjManufacturer)
        {
            SqlParameter[] mParams = {
                                    new SqlParameter("@ManufacturerID", SqlDbType.Int),
                                    new SqlParameter("@MessageID", SqlDbType.Int),
                                    new SqlParameter("@Message", SqlDbType.NVarChar)
                                };
            mParams[0].Value = vobjManufacturer.ManufacturerID;
            mParams[2].Direction = ParameterDirection.Output;
            mParams[3].Direction = ParameterDirection.Output;

            dBase.ExecuteNonQuery("sprocCS_DeleteManufacturer", mParams);

            vobjManufacturer.ReturnValue = Convert.ToInt32(mParams[2].Value.ToString());
            vobjManufacturer.ReturnMessage = mParams[3].Value.ToString();
        }

        /// <summary>
        /// Change status of Manufacturer from Active to Inactive and vice versa
        /// </summary>
        /// <param name="vblnIsActive"></param>
        /// <param name="vintManufacturerID"></param>
        public void ChangeManufacturerStatus(bool vblnIsActive, int vintManufacturerID)
        {
            SqlParameter[] mParams = {
                                    new SqlParameter("@ManufacturerID", SqlDbType.Int),
                                    new SqlParameter("@IsActive", SqlDbType.Bit)
                                };
            mParams[0].Value = vintManufacturerID;
            mParams[1].Value = vblnIsActive;
            dBase.ExecuteNonQuery("sprocCS_ChangeManufacturerStatus", mParams);
        
        }

        /// <summary>
        /// Returns all the Manufacturer from Database
        /// </summary>
        /// <returns>Manufacturer collection</returns>
        public List<Manufacturer> GetAll()
        {
            object[] mParams = { };
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
            SqlParameter[] mParams = {
                                    new SqlParameter("@ManufacturerID", SqlDbType.Int)
                                };
            mParams[0].Value = vobjManufacturer.ManufacturerID;
            using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetManufacturerByID", mParams))
            {

                while (reader.Read())
                {
                    if (reader["ManufacturerID"] != DBNull.Value)
                        vobjManufacturer.ManufacturerID = Convert.ToInt32(reader["ManufacturerID"]);

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
