﻿using System;
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

        public List<ProductPurchase> GetAll(String purchaseStartDate, String purchaseEndDate, String ManufacturerName, PageInfo vobjPageInfo)
        {
            List<ProductPurchase> lstProductPurshase = new List<ProductPurchase>();
            try
            {
                object[] mParams = {
                                    new SqlParameter("@PurchaseStartDate", SqlDbType.VarChar),
                                    new SqlParameter("@PurchaseEndDate", SqlDbType.VarChar),
                                    new SqlParameter("@CompanyName", SqlDbType.NVarChar),
                                    new SqlParameter("@SortColumnName", SqlDbType.NVarChar),                                              
                                    new SqlParameter("@SortDirection", SqlDbType.NVarChar),
                                    new SqlParameter("@SearchText", SqlDbType.NVarChar)
                                };

                mParams[0] = purchaseStartDate;
                mParams[1] = purchaseEndDate;
                mParams[2] = ManufacturerName;
                mParams[3] = vobjPageInfo.SortColumnName;
                mParams[4] = vobjPageInfo.SortDirection;
                mParams[5] = vobjPageInfo.SearchText;


                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetAllProductPurchase", mParams))
                {
                    while (reader.Read())
                    {
                        ProductPurchase objProductPurchase = new ProductPurchase();

                        if (reader["PurchaseID"] != DBNull.Value)
                            objProductPurchase.ProductPurchaseID = Convert.ToInt32(reader["PurchaseID"]);
                        if (reader["ProductPurchaseID"] != DBNull.Value)
                            objProductPurchase.ProductPurchaseDetailID = Convert.ToInt32(reader["ProductPurchaseID"]);
                        if (reader["PurchaseDate"] != DBNull.Value)
                            objProductPurchase.PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]).ToShortDateString();
                        if (reader["PurchaseDate"] != DBNull.Value)
                            objProductPurchase.ProductPurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]);
                        if (reader["CompanyName"] != DBNull.Value)
                            objProductPurchase.ManufacturerName = Convert.ToString(reader["CompanyName"]);
                        if (reader["ProductName"] != DBNull.Value)
                            objProductPurchase.ProductName = Convert.ToString(reader["ProductName"]);
                        if (reader["Sizes"] != DBNull.Value)
                            objProductPurchase.Sizes = Convert.ToString(reader["Sizes"]);
                        if (reader["Quantity"] != DBNull.Value)
                            objProductPurchase.Quantity = Convert.ToInt32(reader["Quantity"]);
                        if (reader["BuyingPrice"] != DBNull.Value)
                            objProductPurchase.BuyingPrice = Convert.ToDecimal(reader["BuyingPrice"]);
                        if (reader["Margin"] != DBNull.Value)
                            objProductPurchase.Margin = Convert.ToDecimal(reader["Margin"]);
                        if (reader["Tax"] != DBNull.Value)
                            objProductPurchase.Tax = Convert.ToDecimal(reader["Tax"]);
                        if (reader["Price"] != DBNull.Value)
                            objProductPurchase.SellingPrice = Convert.ToDecimal(reader["Price"]);
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

        public void GetPurchaseByID(ref PurchaseRecord vobjPurchaseRecord)
        {
            try
            {
                object[] mParams = {
                                    new SqlParameter("@PurchaseID", SqlDbType.Int)
                                };

                mParams[0] = vobjPurchaseRecord.PurchaseID;

                PurchasedProduct objPurchasedProduct;
                ProductSize objProductSize = new ProductSize();
                List<PurchasedProduct> lstPurchasedProduct = new List<PurchasedProduct>();
                List<ProductSize> lstProductSize = new List<ProductSize>();
                int intTotalQty = 0;
                decimal dcmTotalBuyingPrice = 0;

                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetProductPurchaseByID", mParams))
                {
                    while (reader.Read())
                    {
                        objPurchasedProduct = new PurchasedProduct();
                        if (reader["ManufacturerID"] != DBNull.Value)
                            vobjPurchaseRecord.ManufacturerID = Convert.ToInt32(reader["ManufacturerID"]);
                        if (reader["PurchaseDate"] != DBNull.Value)
                            vobjPurchaseRecord.PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]).ToString("dd/MM/yyyy");
                        if (reader["ProductID"] != DBNull.Value)
                            objPurchasedProduct.ProductID = Convert.ToInt32(reader["ProductID"]);
                        if (reader["ProductName"] != DBNull.Value)
                            objPurchasedProduct.ProductName = Convert.ToString(reader["ProductName"]);
                        if (reader["BuyingPrice"] != DBNull.Value)
                            objPurchasedProduct.BuyingPrice = Convert.ToDecimal(reader["BuyingPrice"]);
                        if (reader["Tax"] != DBNull.Value)
                            objPurchasedProduct.Tax = Convert.ToDecimal(reader["Tax"]);
                        if (reader["Margin"] != DBNull.Value)
                            objPurchasedProduct.Margin = Convert.ToDecimal(reader["Margin"]);
                        if (reader["SellingPrice"] != DBNull.Value)
                            objPurchasedProduct.SellingPrice = Convert.ToDecimal(reader["SellingPrice"]);
                        if (reader["BarCode"] != DBNull.Value)
                            objPurchasedProduct.BarCode = Convert.ToString(reader["BarCode"]);
                        string[] sizes = Convert.ToString(reader["Sizes"]).Split(',');

                        lstProductSize = new List<ProductSize>();
                        intTotalQty  = 0;
                        for (int i = 0; i < sizes.Length; i++)
                        {
                            objProductSize = new ProductSize();
                            string[] sizeqty = sizes[i].Split('~');
                            objProductSize.SizeID = Convert.ToInt32(sizeqty[0]);
                            objProductSize.Quantity = Convert.ToInt32(sizeqty[2]);
                            objProductSize.ProductID = objPurchasedProduct.ProductID;
                            lstProductSize.Add(objProductSize);
                            if (i > 0)
                                objPurchasedProduct.SizeQty += "<br/>";
                            objPurchasedProduct.SizeQty += sizeqty[1] + ": " + objProductSize.Quantity;


                            intTotalQty += objProductSize.Quantity;
                        }
                        objPurchasedProduct.PurchasedQty = lstProductSize;

                        vobjPurchaseRecord.TotalQty += intTotalQty;
                        vobjPurchaseRecord.TotalBuyingPrice += objPurchasedProduct.BuyingPrice * intTotalQty;
                        lstPurchasedProduct.Add(objPurchasedProduct);
         
                    }
                    vobjPurchaseRecord.ProductsPurchased = lstPurchasedProduct;
                }
            }
            catch (Exception  ex)
            {
                
                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
        }

        public void GetByID(ref ProductPurchase vobjProductPurchase)
        {
            try
            {
                object[] mParams = {
                                    new SqlParameter("@ProductPurchaseID", SqlDbType.Int)
                                };

                mParams[0] = vobjProductPurchase.ProductPurchaseID;

                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetProductPurchaseByID", mParams))
                {
                    while (reader.Read())
                    {
                        if (reader["ProductPurchaseID"] != DBNull.Value)
                            vobjProductPurchase.ProductPurchaseID = Convert.ToInt32(reader["ProductPurchaseID"]);
                        if (reader["ProductID"] != DBNull.Value)
                            vobjProductPurchase.ProductID = Convert.ToInt32(reader["ProductID"]);
                        if (reader["ManufacturerID"] != DBNull.Value)
                            vobjProductPurchase.ManufacturerID = Convert.ToInt32(reader["ManufacturerID"]);
                        if (reader["BrandID"] != DBNull.Value)
                            vobjProductPurchase.BrandID = Convert.ToInt32(reader["BrandID"]);
                        if (reader["CategoryID"] != DBNull.Value)
                            vobjProductPurchase.CategoryID = Convert.ToInt32(reader["CategoryID"]);
                        if (reader["SeasonID"] != DBNull.Value)
                            vobjProductPurchase.SeasonID = Convert.ToInt32(reader["SeasonID"]);
                        if (reader["PurchaseDate"] != DBNull.Value)
                            vobjProductPurchase.PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]).ToString("dd/MM/yyyy");
                        if (reader["Quantity"] != DBNull.Value)
                            vobjProductPurchase.Quantity = Convert.ToInt32(reader["Quantity"]);
                        if (reader["BuyingPrice"] != DBNull.Value)
                            vobjProductPurchase.BuyingPrice = Convert.ToDecimal(reader["BuyingPrice"]);
                        if (reader["Tax"] != DBNull.Value)
                            vobjProductPurchase.Tax = Convert.ToDecimal(reader["Tax"]);
                        if (reader["Margin"] != DBNull.Value)
                            vobjProductPurchase.Margin = Convert.ToDecimal(reader["Margin"]);
                        if (reader["SellingPrice"] != DBNull.Value)
                            vobjProductPurchase.SellingPrice = Convert.ToDecimal(reader["SellingPrice"]);
                        if (reader["BarCode"] != DBNull.Value)
                            vobjProductPurchase.BarCode = Convert.ToString(reader["BarCode"]);
                        if (reader["SizeIDs"] != DBNull.Value)
                            vobjProductPurchase.SizeIDs = Convert.ToString(reader["SizeIDs"]);
                    }
                }

            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
        }

        public void AddEditProductPurchase(ref PurchaseRecord vobjProductPurchase)
        {
            try
            {
                StringBuilder strProducts = new StringBuilder();
                StringBuilder strSizes = new StringBuilder();
                strProducts.Append("<data>");
                strSizes.Append("<data>");
                for (int i = 0; i < vobjProductPurchase.ProductsPurchased.Count; i++)
                {
                    strProducts.Append("<products>");
                    strProducts.Append("<productId>" + vobjProductPurchase.ProductsPurchased[i].ProductID + "</productId>");
                    strProducts.Append("<buyingPrice>" + vobjProductPurchase.ProductsPurchased[i].BuyingPrice + "</buyingPrice>");
                    strProducts.Append("<tax>" + vobjProductPurchase.ProductsPurchased[i].Tax + "</tax>");
                    strProducts.Append("<margin>" + vobjProductPurchase.ProductsPurchased[i].Margin + "</margin>");
                    strProducts.Append("<sellingPrice>" + vobjProductPurchase.ProductsPurchased[i].SellingPrice + "</sellingPrice>");
                    strProducts.Append("</products>");

                    for (int j = 0; j < vobjProductPurchase.ProductsPurchased[i].PurchasedQty.Count; j++)
                    {
                        if (vobjProductPurchase.ProductsPurchased[i].PurchasedQty[j].Quantity > 0)
                        {
                            strSizes.Append("<sizes>");
                            strSizes.Append("<productId>" + vobjProductPurchase.ProductsPurchased[i].PurchasedQty[j].ProductID + "</productId>");
                            strSizes.Append("<sizeId>" + vobjProductPurchase.ProductsPurchased[i].PurchasedQty[j].SizeID + "</sizeId>");
                            strSizes.Append("<quantity>" + vobjProductPurchase.ProductsPurchased[i].PurchasedQty[j].Quantity + "</quantity>");
                            strSizes.Append("</sizes>");
                        }
                    }
                }
                strProducts.Append("</data>");
                strSizes.Append("</data>");

                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_InsertUpdatePurchase");
                dBase.AddInParameter(objCmd, "@PurchaseID", DbType.Int32, vobjProductPurchase.PurchaseID);
                dBase.AddInParameter(objCmd, "@ManufacturerID", DbType.Int32, vobjProductPurchase.ManufacturerID);
                dBase.AddInParameter(objCmd, "@PurchaseDate", DbType.String, vobjProductPurchase.PurchaseDate.Trim());
                dBase.AddInParameter(objCmd, "@ProductsPurchased", DbType.Xml, strProducts.ToString());
                dBase.AddInParameter(objCmd, "@PurchasedQty", DbType.Xml, strSizes.ToString());
                dBase.AddInParameter(objCmd, "@UpdatedBy", DbType.Int32, 1);
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

        public void UpdateProductPrice(ref ProductPurchase vobjProductPurchase)
        {
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_UpdateProductPrice");
                dBase.AddInParameter(objCmd, "@ProductID", DbType.Int32, vobjProductPurchase.ProductID);
                dBase.AddInParameter(objCmd, "@BuyingPrice", DbType.Decimal, vobjProductPurchase.BuyingPrice);
                dBase.AddInParameter(objCmd, "@Tax", DbType.Decimal, vobjProductPurchase.Tax);
                dBase.AddInParameter(objCmd, "@Margin", DbType.Decimal, vobjProductPurchase.Margin);
                dBase.AddInParameter(objCmd, "@SellingPrice", DbType.Decimal, vobjProductPurchase.SellingPrice);
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
        public void AddEditPurchase(ref ProductPurchase vobjProductPurchase)
        {
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_InsertUpdateProductPurchase");
                dBase.AddInParameter(objCmd, "@ProductPurchaseID", DbType.Int32, vobjProductPurchase.ProductPurchaseID);
                dBase.AddInParameter(objCmd, "@ProductID", DbType.Int32, vobjProductPurchase.ProductID);
                dBase.AddInParameter(objCmd, "@ManufacturerID", DbType.Int32, vobjProductPurchase.ManufacturerID);
                dBase.AddInParameter(objCmd, "@BrandID", DbType.Int32, vobjProductPurchase.BrandID);
                dBase.AddInParameter(objCmd, "@CategoryID", DbType.Int32, vobjProductPurchase.CategoryID);
                dBase.AddInParameter(objCmd, "@SeasonID", DbType.Int32, vobjProductPurchase.SeasonID);
                dBase.AddInParameter(objCmd, "@PurchaseDate", DbType.String, vobjProductPurchase.PurchaseDate);
                dBase.AddInParameter(objCmd, "@Quantity", DbType.Int32, vobjProductPurchase.Quantity);
                dBase.AddInParameter(objCmd, "@BuyingPrice", DbType.Decimal, vobjProductPurchase.BuyingPrice);
                dBase.AddInParameter(objCmd, "@Tax", DbType.Decimal, vobjProductPurchase.Tax);
                dBase.AddInParameter(objCmd, "@Margin", DbType.Decimal, vobjProductPurchase.Margin);
                dBase.AddInParameter(objCmd, "@SellingPrice", DbType.Decimal, vobjProductPurchase.SellingPrice);
                dBase.AddInParameter(objCmd, "@BarCode", DbType.String, vobjProductPurchase.BarCode);
                dBase.AddInParameter(objCmd, "@SizeIDs", DbType.Xml, vobjProductPurchase.SizeIDs);
                dBase.AddInParameter(objCmd, "@UpdatedBy", DbType.Int32, vobjProductPurchase.UpdatedBy);
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


        /*
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
        */

        public void Delete(ref ProductPurchase vobjProductPurchase)
        {
            try
            {
                DbCommand objCmd = dBase.GetStoredProcCommand("sprocCS_DeleteProductPurchase");
                dBase.AddInParameter(objCmd, "@PurchaseID", DbType.Int32, vobjProductPurchase.ProductPurchaseID);
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
