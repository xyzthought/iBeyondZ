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

    public class SaleDB
    {
        Database dBase = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");

        public List<Sale> GetAllSaleDataByDate(List<Sale> objData, PageInfo vobjPageInfo, string strDate)
        {
            List<Sale> lstobjData = new List<Sale>();
            try
            {

                object[] mParams = {
                                        new SqlParameter("@SortColumnName", SqlDbType.NVarChar),                                              
                                        new SqlParameter("@SortDirection", SqlDbType.NVarChar),
                                        new SqlParameter("@SearchText", SqlDbType.NVarChar),
                                        new SqlParameter("@SaleDate", SqlDbType.NVarChar),
                                };

                mParams[0] = vobjPageInfo.SortColumnName;
                mParams[1] = vobjPageInfo.SortDirection;
                mParams[2] = vobjPageInfo.SearchText;
                mParams[3] = strDate;

                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetAllSaleDataByDate", mParams))
                {
                    while (reader.Read())
                    {
                        lstobjData.Add(PopulateData(reader));
                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return lstobjData;
        }

        private Sale PopulateData(IDataReader drData)
        {
            Sale objData = new Sale();
            try
            {

                if (FieldExists(drData, "SaleID") && drData["SaleID"] != DBNull.Value)
                {
                    objData.SaleID = Convert.ToInt32(drData["SaleID"]);
                }
                if (FieldExists(drData, "SaleOrder") && drData["SaleOrder"] != DBNull.Value)
                {
                    objData.SaleOrder = Convert.ToString(drData["SaleOrder"]);
                }
                if (FieldExists(drData, "FirstName") && drData["FirstName"] != DBNull.Value)
                {
                    objData.FirstName = Convert.ToString(drData["FirstName"]);
                }
                if (FieldExists(drData, "LastName") && drData["LastName"] != DBNull.Value)
                {
                    objData.LastName = Convert.ToString(drData["LastName"]);
                }

                if (FieldExists(drData, "SaleDate") && drData["SaleDate"] != DBNull.Value)
                {
                    objData.SaleDate = Convert.ToDateTime(drData["SaleDate"]);
                }


                if (FieldExists(drData, "Price") && drData["Price"] != DBNull.Value)
                {
                    objData.Price = Convert.ToDecimal(drData["Price"]);
                }
                if (FieldExists(drData, "StandardRebate") && drData["StandardRebate"] != DBNull.Value)
                {
                    objData.StandardRebate = Convert.ToDecimal(drData["StandardRebate"]);

                }
                if (FieldExists(drData, "Discount") && drData["Discount"] != DBNull.Value)
                {
                    objData.Discount = Convert.ToDecimal(drData["Discount"]);

                }
                if (FieldExists(drData, "SaleMadeByName") && drData["SaleMadeByName"] != DBNull.Value)
                {
                    objData.SaleMadeByName = Convert.ToString(drData["SaleMadeByName"]);

                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return objData;
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

        public List<Sale> GetProductDetailByBarCode(string vstrProductID)
        {
            List<Sale> lstobjData = new List<Sale>();
            try
            {

                object[] mParams = {
                                        new SqlParameter("@ProductID", SqlDbType.Int),                                              
                                };

                mParams[0] = Convert.ToInt32(vstrProductID);

                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetProductDetailByBarCode", mParams))
                {
                    while (reader.Read())
                    {
                        lstobjData.Add(PopulateProductData(reader));
                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return lstobjData;
        }

        private Sale PopulateProductData(IDataReader drData)
        {
            Sale objData = new Sale();
            try
            {

                if (FieldExists(drData, "ProductID") && drData["ProductID"] != DBNull.Value)
                {
                    objData.ProductID = Convert.ToInt32(drData["ProductID"]);
                }
                if (FieldExists(drData, "BarCode") && drData["BarCode"] != DBNull.Value)
                {
                    objData.BarCode = Convert.ToString(drData["BarCode"]);
                }
                if (FieldExists(drData, "ProductName") && drData["ProductName"] != DBNull.Value)
                {
                    objData.ProductName = Convert.ToString(drData["ProductName"]);
                }
                if (FieldExists(drData, "SizeID") && drData["SizeID"] != DBNull.Value)
                {
                    objData.SizeID = Convert.ToInt32(drData["SizeID"]);
                }
                if (FieldExists(drData, "SizeName") && drData["SizeName"] != DBNull.Value)
                {
                    objData.SizeName = Convert.ToString(drData["SizeName"]);
                }
                if (FieldExists(drData, "Quantity") && drData["Quantity"] != DBNull.Value)
                {
                    objData.Quantity = Convert.ToDecimal(drData["Quantity"]);
                }
                if (FieldExists(drData, "UnitPrice") && drData["UnitPrice"] != DBNull.Value)
                {
                    objData.UnitPrice = Convert.ToDecimal(drData["UnitPrice"]);
                }
                if (FieldExists(drData, "Discount") && drData["Discount"] != DBNull.Value)
                {
                    objData.Discount = Convert.ToDecimal(drData["Discount"]);
                }
                if (FieldExists(drData, "Tax") && drData["Tax"] != DBNull.Value)
                {
                    objData.Tax = Convert.ToDecimal(drData["Tax"]);
                }
                if (FieldExists(drData, "Price") && drData["Price"] != DBNull.Value)
                {
                    objData.Price = Convert.ToDecimal(drData["Price"]);
                }

            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return objData;
        }

        public DataTable GetAllCustomerNameForAutoComplete()
        {
            DataTable dtData = new DataTable();
            try
            {
                using (DbConnection connection = dBase.CreateConnection())
                {
                    connection.Open();
                    DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_GetAllCustomerNameForAutoComplete");
                    DataSet dsData = dBase.ExecuteDataSet(objCmd);
                    dtData = dsData.Tables[0];
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }

            return dtData;
        }

        public DataTable PopulateAutoCompleteProductInformation()
        {
            DataTable dtData = new DataTable();
            try
            {
                using (DbConnection connection = dBase.CreateConnection())
                {
                    connection.Open();
                    DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_SearchProductAutoComplete");
                    DataSet dsData = dBase.ExecuteDataSet(objCmd);
                    dtData = dsData.Tables[0];
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }

            return dtData;
        }

        public Message InsertUpdateSaleMaster(Sale objSale)
        {
            Message objMessage = new Message();
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_InsertUpdateSaleMaster");
                dBase.AddInParameter(objCmd, "@SaleID", DbType.Int32, objSale.SaleID);
                dBase.AddInParameter(objCmd, "@CustomerID", DbType.Int32, objSale.CustomerID);
                dBase.AddInParameter(objCmd, "@CFirstName", DbType.String, objSale.CFirstName);
                dBase.AddInParameter(objCmd, "@Address", DbType.String, objSale.Address);
                dBase.AddInParameter(objCmd, "@City", DbType.String, objSale.City);
                dBase.AddInParameter(objCmd, "@ZIP", DbType.String, objSale.ZIP);
                dBase.AddInParameter(objCmd, "@Country", DbType.String, objSale.Country);
                dBase.AddInParameter(objCmd, "@Email", DbType.String, objSale.Email);
                dBase.AddInParameter(objCmd, "@TeleNumber", DbType.String, objSale.TeleNumber);

                dBase.AddInParameter(objCmd, "@CCAmount", DbType.Decimal, objSale.CCAmount);
                dBase.AddInParameter(objCmd, "@BankAmount", DbType.Decimal, objSale.BankAmount);
                dBase.AddInParameter(objCmd, "@Cash", DbType.Decimal, objSale.Cash);
                dBase.AddInParameter(objCmd, "@Discount", DbType.Decimal, objSale.Discount);
                dBase.AddInParameter(objCmd, "@SaleMadeBy", DbType.Int32, objSale.SaleMadeBy);
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

        public Message InsertUpdateSaleDetail(Sale objSale)
        {
            Message objMessage = new Message();
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_InsertUpdateSaleDetail");
                dBase.AddInParameter(objCmd, "@SaleID", DbType.Int32, objSale.SaleID);
                dBase.AddInParameter(objCmd, "@ProductID", DbType.Int32, objSale.ProductID);
                dBase.AddInParameter(objCmd, "@SizeID", DbType.Int32, objSale.SizeID);
                dBase.AddInParameter(objCmd, "@Quantity", DbType.Decimal, objSale.Quantity);
                dBase.AddInParameter(objCmd, "@Discount", DbType.Decimal, objSale.Discount);
                dBase.AddInParameter(objCmd, "@Price", DbType.Decimal, objSale.Price);

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

        public Message DeleteSale(int intSaleID, int intUserID)
        {
            Message objMessage = new Message();
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_DeleteSale");
                dBase.AddInParameter(objCmd, "@SaleID", DbType.Int32, intSaleID);
                dBase.AddInParameter(objCmd, "@UserID", DbType.Int32, intUserID);
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

        public List<Sale> GetSaleDetailBySaleID(Sale objSale)
        {
            List<Sale> lstobjData = new List<Sale>();
            try
            {

                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_GetSaleDetailBySaleID");
                dBase.AddInParameter(objCmd, "@SaleID", DbType.Int32, objSale.SaleID);

                using (IDataReader reader = dBase.ExecuteReader(objCmd))
                {
                    while (reader.Read())
                    {
                        lstobjData.Add(PopulateProductData(reader));
                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return lstobjData;
        }

        public Sale GetFinalCheckOutDeatils(ref Sale objSale)
        {
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_GetFinalCheckOutDeatils");
                dBase.AddInParameter(objCmd, "@SaleID", DbType.Int32, objSale.SaleID);

                using (IDataReader reader = dBase.ExecuteReader(objCmd))
                {
                    while (reader.Read())
                    {
                        objSale=PopulateFinalCheckOutData(reader);
                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return objSale;
        }

        private Sale PopulateFinalCheckOutData(IDataReader drData)
        {
            Sale objData = new Sale();
            try
            {

                if (FieldExists(drData, "CustomerID") && drData["CustomerID"] != DBNull.Value)
                {
                    objData.CustomerID = Convert.ToInt32(drData["CustomerID"]);
                }
                if (FieldExists(drData, "CFirstName") && drData["CFirstName"] != DBNull.Value)
                {
                    objData.CFirstName = Convert.ToString(drData["CFirstName"]);
                }
                if (FieldExists(drData, "Address") && drData["Address"] != DBNull.Value)
                {
                    objData.Address = Convert.ToString(drData["Address"]);
                }
                if (FieldExists(drData, "ZIP") && drData["ZIP"] != DBNull.Value)
                {
                    objData.ZIP = Convert.ToString(drData["ZIP"]);
                }
                if (FieldExists(drData, "City") && drData["City"] != DBNull.Value)
                {
                    objData.City = Convert.ToString(drData["City"]);
                }
                if (FieldExists(drData, "TeleNumber") && drData["TeleNumber"] != DBNull.Value)
                {
                    objData.TeleNumber = Convert.ToString(drData["TeleNumber"]);
                }
                if (FieldExists(drData, "Country") && drData["Country"] != DBNull.Value)
                {
                    objData.Country = Convert.ToString(drData["Country"]);
                }
                if (FieldExists(drData, "Email") && drData["Email"] != DBNull.Value)
                {
                    objData.Email = Convert.ToString(drData["Email"]);
                }
                if (FieldExists(drData, "Discount") && drData["Discount"] != DBNull.Value)
                {
                    objData.Discount = Convert.ToDecimal(drData["Discount"]);
                }
                if (FieldExists(drData, "CCAmount") && drData["CCAmount"] != DBNull.Value)
                {
                    objData.CCAmount = Convert.ToDecimal(drData["CCAmount"]);
                }
                if (FieldExists(drData, "BankAmount") && drData["BankAmount"] != DBNull.Value)
                {
                    objData.BankAmount = Convert.ToDecimal(drData["BankAmount"]);
                }
                if (FieldExists(drData, "Cash") && drData["Cash"] != DBNull.Value)
                {
                    objData.Cash = Convert.ToDecimal(drData["Cash"]);
                }
                

            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return objData;
        }

        public Message DeleteExistingSalesDetails(Sale objSale)
        {
            Message objMessage = new Message();
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_DeleteExistingSalesDetails");
                dBase.AddInParameter(objCmd, "@SaleID", DbType.Int32, objSale.SaleID);
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
    }
}
