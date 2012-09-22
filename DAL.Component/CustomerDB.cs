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
    public class CustomerDB
    {
        Database dBase = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");

        public List<Customer> GetCustomerDetailByCustID(ref Customer objCust)
        {
            List<Customer> lstobjCustomer = new List<Customer>();
            try
            {

                object[] mParams = {
                                        new SqlParameter("@CustomerID", SqlDbType.Int),                                              
                                };

                mParams[0] = objCust.CustomerID;

                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetCustomerDetailByCustID", mParams))
                {
                    while (reader.Read())
                    {
                        lstobjCustomer.Add(PopulateCustomer(reader));
                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return lstobjCustomer;
        }

        private Customer PopulateCustomer(IDataReader drData)
        {
            Customer objCustomer = new Customer();
            try
            {

                if (FieldExists(drData, "CustomerID") && drData["CustomerID"] != DBNull.Value)
                {
                    objCustomer.CustomerID = Convert.ToInt32(drData["CustomerID"]);
                }
                if (FieldExists(drData, "FirstName") && drData["FirstName"] != DBNull.Value)
                {
                    objCustomer.FirstName = Convert.ToString(drData["FirstName"]);
                }
                if (FieldExists(drData, "LastName") && drData["LastName"] != DBNull.Value)
                {
                    objCustomer.LastName = Convert.ToString(drData["LastName"]);
                }

                if (FieldExists(drData, "Address") && drData["Address"] != DBNull.Value)
                {
                    objCustomer.Address = Convert.ToString(drData["Address"]);
                }
                if (FieldExists(drData, "City") && drData["City"] != DBNull.Value)
                {
                    objCustomer.City = Convert.ToString(drData["City"]);
                }
                if (FieldExists(drData, "ZIP") && drData["ZIP"] != DBNull.Value)
                {
                    objCustomer.ZIP = Convert.ToString(drData["ZIP"]);
                }

                if (FieldExists(drData, "Country") && drData["Country"] != DBNull.Value)
                {
                    objCustomer.Country = Convert.ToString(drData["Country"]);
                }

                if (FieldExists(drData, "TeleNumber") && drData["TeleNumber"] != DBNull.Value)
                {
                    objCustomer.TeleNumber = Convert.ToString(drData["TeleNumber"]);
                }

                if (FieldExists(drData, "Email") && drData["Email"] != DBNull.Value)
                {
                    objCustomer.Email = Convert.ToString(drData["Email"]);
                }

               
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return objCustomer;
        }

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

    }
}
