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
using CSWeb.Utility;
using System.Diagnostics;

namespace DAL.Component
{
    public class ProductPurchaseDB
    {
        Database dBase = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");

        public List<ProductPurchase> GetAll(DateTime purchaseStartDate, DateTime purchaseEndDate, PageInfo vobjPageInfo)
        {
            List<ProductPurchase> lstProductPurshase = new List<ProductPurchase>();
            try
            {
                object[] mParams = {
                                    new SqlParameter("@PurchaseStartDate", SqlDbType.DateTime),
                                    new SqlParameter("@PurchaseEndDate", SqlDbType.DateTime),
                                    new SqlParameter("@SortColumnName", SqlDbType.NVarChar),                                              
                                    new SqlParameter("@SortDirection", SqlDbType.NVarChar),
                                    new SqlParameter("@SearchText", SqlDbType.NVarChar)
                                };

                mParams[0] = purchaseStartDate;
                mParams[1] = purchaseEndDate;
                mParams[2] = vobjPageInfo.SortColumnName;
                mParams[3] = vobjPageInfo.SortDirection;
                mParams[4] = vobjPageInfo.SearchText;


                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetAllProductPurchase", mParams))
                {
                    while (reader.Read())
                    {
                        ProductPurchase objProductPurchase = new ProductPurchase();

                        if (reader["PurchaseID"] != DBNull.Value)
                            objProductPurchase.PurchaseID = Convert.ToInt32(reader["PurchaseID"]);
                        if (reader["PurchaseDate"] != DBNull.Value)
                            objProductPurchase.PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]);
                        if (reader["CompanyName"] != DBNull.Value)
                            objProductPurchase.ManufacturerName = Convert.ToString(reader["CompanyName"]);
                        if (reader["ProductName"] != DBNull.Value)
                            objProductPurchase.ProductName = Convert.ToString(reader["ProductName"]);
                        if (reader["SizeName"] != DBNull.Value)
                            objProductPurchase.SizeName = Convert.ToString(reader["SizeName"]);
                        if (reader["Quantity"] != DBNull.Value)
                            objProductPurchase.Quantity = Convert.ToInt32(reader["Quantity"]);
                        if (reader["Price"] != DBNull.Value)
                            objProductPurchase.Price = Convert.ToDecimal(reader["Price"]);

                        lstProductPurshase.Add(objProductPurchase);
                    }
                }

            }
            catch (Exception ex)
            {
                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return lstProductPurshase;
        }

        public void GetByID(ref ProductPurchase vobjProductPurchase)
        {
            try
            {
                object[] mParams = {
                                    new SqlParameter("@PurchaseID", SqlDbType.Int)
                                };

                mParams[0] = vobjProductPurchase.PurchaseID;

                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetProductPurchaseByID", mParams))
                {
                    while (reader.Read())
                    {
                        if (reader["PurchaseID"] != DBNull.Value)
                            vobjProductPurchase.PurchaseID = Convert.ToInt32(reader["PurchaseID"]);
                        if (reader["PurchaseDate"] != DBNull.Value)
                            vobjProductPurchase.PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]);
                        if (reader["ManufacturerID"] != DBNull.Value)
                            vobjProductPurchase.ManufacturerID = Convert.ToInt32(reader["ManufacturerID"]);
                        if (reader["CompanyName"] != DBNull.Value)
                            vobjProductPurchase.ManufacturerName = Convert.ToString(reader["CompanyName"]);
                        if (reader["ProductID"] != DBNull.Value)
                            vobjProductPurchase.ProductID = Convert.ToInt32(reader["ProductID"]);
                        if (reader["ProductName"] != DBNull.Value)
                            vobjProductPurchase.ProductName = Convert.ToString(reader["ProductName"]);
                        if (reader["SizeID"] != DBNull.Value)
                            vobjProductPurchase.SizeID = Convert.ToInt32(reader["SizeID"]);
                        if (reader["SizeName"] != DBNull.Value)
                            vobjProductPurchase.SizeName = Convert.ToString(reader["SizeName"]);
                        if (reader["Quantity"] != DBNull.Value)
                            vobjProductPurchase.Quantity = Convert.ToInt32(reader["Quantity"]);
                        if (reader["Price"] != DBNull.Value)
                            vobjProductPurchase.Price = Convert.ToDecimal(reader["Price"]);

                    }
                }

            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
        }

        public void Add(ref ProductPurchase vobjProductPurchase)
        {
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_InsertProductPurchase");
                dBase.AddInParameter(objCmd, "@ManufacturerID", DbType.Int32, vobjProductPurchase.ManufacturerID);
                dBase.AddInParameter(objCmd, "@PurchaseDate", DbType.DateTime, vobjProductPurchase.PurchaseDate);
                dBase.AddInParameter(objCmd, "@ProductID", DbType.Int32, vobjProductPurchase.ProductID);
                dBase.AddInParameter(objCmd, "@SizeID", DbType.Int32, vobjProductPurchase.SizeID);
                dBase.AddInParameter(objCmd, "@Quantity", DbType.Int32, vobjProductPurchase.Quantity);
                dBase.AddInParameter(objCmd, "@Price", DbType.Decimal, vobjProductPurchase.Price);
                dBase.AddOutParameter(objCmd, "@MessageID", DbType.Int32, 4);
                dBase.AddOutParameter(objCmd, "@Message", DbType.String, 255);
                dBase.ExecuteNonQuery(objCmd);

                vobjProductPurchase.ReturnValue = (int)dBase.GetParameterValue(objCmd, "@MessageID");
                vobjProductPurchase.ReturnMessage = (string)dBase.GetParameterValue(objCmd, "@Message");
            }
            catch (Exception ex)
            {
                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }

        }

        public void Update(ref ProductPurchase vobjProductPurchase)
        {
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_UpdateProductPurchase");
                dBase.AddInParameter(objCmd, "@PurchaseID", DbType.Int32, vobjProductPurchase.ManufacturerID);
                dBase.AddInParameter(objCmd, "@ManufacturerID", DbType.Int32, vobjProductPurchase.ManufacturerID);
                dBase.AddInParameter(objCmd, "@PurchaseDate", DbType.DateTime, vobjProductPurchase.PurchaseDate);
                dBase.AddInParameter(objCmd, "@ProductID", DbType.Int32, vobjProductPurchase.ProductID);
                dBase.AddInParameter(objCmd, "@SizeID", DbType.Int32, vobjProductPurchase.SizeID);
                dBase.AddInParameter(objCmd, "@Quantity", DbType.Int32, vobjProductPurchase.Quantity);
                dBase.AddInParameter(objCmd, "@Price", DbType.Decimal, vobjProductPurchase.Price);
                dBase.AddOutParameter(objCmd, "@MessageID", DbType.Int32, 4);
                dBase.AddOutParameter(objCmd, "@Message", DbType.String, 255);
                dBase.ExecuteNonQuery(objCmd);

                vobjProductPurchase.ReturnValue = (int)dBase.GetParameterValue(objCmd, "@MessageID");
                vobjProductPurchase.ReturnMessage = (string)dBase.GetParameterValue(objCmd, "@Message");
            }
            catch (Exception ex)
            {
                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
        }

        public void Delete(ref ProductPurchase vobjProductPurchase)
        {
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_DeleteProductPurchase");
                dBase.AddInParameter(objCmd, "@PurchaseID", DbType.Int32, vobjProductPurchase.PurchaseID);
                dBase.AddOutParameter(objCmd, "@MessageID", DbType.Int32, 4);
                dBase.AddOutParameter(objCmd, "@Message", DbType.String, 255);
                dBase.ExecuteNonQuery(objCmd);

                vobjProductPurchase.ReturnValue = (int)dBase.GetParameterValue(objCmd, "@MessageID");
                vobjProductPurchase.ReturnMessage = (string)dBase.GetParameterValue(objCmd, "@Message");
            }
            catch (Exception ex)
            {
                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }

        }

    }
}
