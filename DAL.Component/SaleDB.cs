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
                if (FieldExists(drData, "PaymentMode") && drData["PaymentMode"] != DBNull.Value)
                {
                    objData.PaymentMode = Convert.ToString(drData["PaymentMode"]);
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
                if (FieldExists(drData, "SaleMadeBy") && drData["SaleMadeBy"] != DBNull.Value)
                {
                    objData.SaleMadeBy = Convert.ToString(drData["SaleMadeBy"]);

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

        public List<Sale> GetProductDetailByBarCode(string vstrProductBarcode)
        {
            List<Sale> lstobjData = new List<Sale>();
            try
            {

                object[] mParams = {
                                        new SqlParameter("@ProductBarcode", SqlDbType.NVarChar),                                              
                                };

                mParams[0] = vstrProductBarcode;

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
                if (FieldExists(drData, "SizeName") && drData["SizeName"] != DBNull.Value)
                {
                    objData.SizeName = Convert.ToString(drData["SizeName"]);
                }
                if (FieldExists(drData, "Quantity") && drData["Quantity"] != DBNull.Value)
                {
                    objData.Quantity = Convert.ToDecimal(drData["Quantity"]);
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
    }
}
