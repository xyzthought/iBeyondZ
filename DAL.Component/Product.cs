using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using BLL.BusinessObject;
using CSWeb.Utility;
using System.Data.Common;
using System.Data;
using System.Web.Mail;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Data.SqlClient;

namespace DAL.Component
{
    public class Product
    {
        public List<BLL.BusinessObject.Product> GetAllProduct(PageInfo vobjPageInfo)
        {

            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("[sprocCS_GetProducts_V2]");
            db.AddInParameter(dbCommand, "SortColumnName", DbType.String, vobjPageInfo.SortColumnName);
            db.AddInParameter(dbCommand, "SortDirection", DbType.String, vobjPageInfo.SortDirection);
            db.AddInParameter(dbCommand, "SearchText", DbType.String, vobjPageInfo.SearchText);

            List<BLL.BusinessObject.Product> list = new List<BLL.BusinessObject.Product>();

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {

                    BLL.BusinessObject.Product obj = new BLL.BusinessObject.Product();
                    list.Add(PopulateProductInfo(dataReader));

                }

            }

            return list;

        }

        private BLL.BusinessObject.Product PopulateProductInfo(IDataReader dataReader)
        {
            BLL.BusinessObject.Product obj = new BLL.BusinessObject.Product();

            if (dataReader["ProductID"] != DBNull.Value) { obj.ProductID = (int)dataReader["ProductID"]; }
            if (dataReader["ProductName"] != DBNull.Value) { obj.ProductName = (string)dataReader["ProductName"]; }
            if (dataReader["Description"] != DBNull.Value) { obj.Description = (string)dataReader["Description"]; }
            if (dataReader["BarCode"] != DBNull.Value) { obj.BarCode = (string)dataReader["BarCode"]; }
            if (dataReader["Brand"] != DBNull.Value) { obj.Brand = (string)dataReader["Brand"]; }
            if (dataReader["BuyingPrice"] != DBNull.Value) { obj.BuyingPrice = (decimal)dataReader["BuyingPrice"]; }
            if (dataReader["SellingPrice"] != DBNull.Value) { obj.SellingPrice = (decimal)dataReader["SellingPrice"]; }
            if (dataReader["Tax"] != DBNull.Value) { obj.Tax = (decimal)dataReader["Tax"]; }
            if (dataReader["Margin"] != DBNull.Value) { obj.Margin = (decimal)dataReader["Margin"]; }
            if (dataReader["BarCode"] != DBNull.Value) { obj.BarCode = (string)dataReader["BarCode"]; }
            if (dataReader["Season"] != DBNull.Value) { obj.Season = (string)dataReader["Season"]; }
            if (dataReader["Quantity"] != DBNull.Value) { obj.Quantities = dataReader["Quantity"].ToString(); }
            if (dataReader["Stock"] != DBNull.Value) { obj.Stock = dataReader["Stock"].ToString(); }
            if (dataReader["QuantityDetails"] != DBNull.Value) { obj.QuantityDetails = dataReader["QuantityDetails"].ToString(); }
            if (dataReader["StockDetails"] != DBNull.Value) { obj.StockDetails = dataReader["StockDetails"].ToString(); }
            return obj;
        }

        public List<BLL.BusinessObject.Product> GetAllActiveProduct()
        {
            List<BLL.BusinessObject.Product> lstProduct = new List<BLL.BusinessObject.Product>();
            try
            {
                object[] mParams = { };

                Database dBase = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");

                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetAllActiveProduct", mParams))
                {
                    while (reader.Read())
                    {
                        BLL.BusinessObject.Product objProduct = new BLL.BusinessObject.Product();

                        if (reader["ProductID"] != DBNull.Value)
                            objProduct.ProductID = Convert.ToInt32(reader["ProductID"]);

                        if (reader["ProductName"] != DBNull.Value)
                            objProduct.ProductName = Convert.ToString(reader["ProductName"]);

                        lstProduct.Add(objProduct);
                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return lstProduct;
        }

        public int InsertProduct(BLL.BusinessObject.Product vobjProduct)
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_InsertProduct");

            //db.AddInParameter(dbCommand, "ProductID", DbType.Int32, vobjProduct.ProductID);
            db.AddInParameter(dbCommand, "ProductName", DbType.String, vobjProduct.ProductName);
            db.AddInParameter(dbCommand, "Description", DbType.String, vobjProduct.Description);
            // db.AddInParameter(dbCommand, "ManufacturerID", DbType.Int32, vobjProduct.ManufacturerID);
            db.AddInParameter(dbCommand, "BrandID", DbType.Int32, vobjProduct.BrandID);
            db.AddInParameter(dbCommand, "SeasonID", DbType.Int32, vobjProduct.SeasonID);
            db.AddInParameter(dbCommand, "CategoryID", DbType.Int32, vobjProduct.CategoryID);
            db.AddInParameter(dbCommand, "SizeID", DbType.String, vobjProduct.SizeID);
            db.AddInParameter(dbCommand, "BuyingPrice", DbType.Decimal, vobjProduct.BuyingPrice);
            db.AddInParameter(dbCommand, "Tax", DbType.Decimal, vobjProduct.Tax);
            db.AddInParameter(dbCommand, "Margin", DbType.Decimal, vobjProduct.Margin);
            db.AddInParameter(dbCommand, "BarCode", DbType.String, vobjProduct.BarCode);
            db.AddInParameter(dbCommand, "SellingPrice", DbType.Decimal, vobjProduct.SellingPrice);
            //db.AddInParameter(dbCommand, "CreatedOn", DbType.DateTime, vobjProduct.CreatedOn);
            //db.AddInParameter(dbCommand, "UpdatedOn", DbType.DateTime, vobjProduct.UpdatedOn);
            db.AddInParameter(dbCommand, "CreatedBy", DbType.Int32, vobjProduct.CreatedBy);
            db.AddInParameter(dbCommand, "UpdatedBy", DbType.Int32, vobjProduct.UpdatedBy);
            //db.AddInParameter(dbCommand, "IsActive", DbType.Boolean, vobjProduct.IsActive);
            //db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, vobjProduct.IsDeleted);
            db.AddOutParameter(dbCommand, "Return", DbType.Int32, 4);
            db.ExecuteNonQuery(dbCommand);
            int mintReturn = int.Parse(db.GetParameterValue(dbCommand, "@Return").ToString());
            return mintReturn;
            //(db.ExecuteNonQuery(dbCommand) == 1);
        }

        public int UpdateProduct(BLL.BusinessObject.Product vobjProduct)
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_UpdateProduct");

            db.AddInParameter(dbCommand, "ProductID", DbType.Int32, vobjProduct.ProductID);
            db.AddInParameter(dbCommand, "ProductName", DbType.String, vobjProduct.ProductName);
            db.AddInParameter(dbCommand, "Description", DbType.String, vobjProduct.Description);
            //db.AddInParameter(dbCommand, "ManufacturerID", DbType.Int32, vobjProduct.ManufacturerID);
            db.AddInParameter(dbCommand, "BrandID", DbType.Int32, vobjProduct.BrandID);
            db.AddInParameter(dbCommand, "CategoryID", DbType.Int32, vobjProduct.CategoryID);
            db.AddInParameter(dbCommand, "SeasonID", DbType.Int32, vobjProduct.SeasonID);
            db.AddInParameter(dbCommand, "SizeID", DbType.String, vobjProduct.SizeID);
            db.AddInParameter(dbCommand, "BuyingPrice", DbType.Decimal, vobjProduct.BuyingPrice);
            db.AddInParameter(dbCommand, "Tax", DbType.Decimal, vobjProduct.Tax);
            db.AddInParameter(dbCommand, "Margin", DbType.Decimal, vobjProduct.Margin);
            db.AddInParameter(dbCommand, "BarCode", DbType.String, vobjProduct.BarCode);
            db.AddInParameter(dbCommand, "SellingPrice", DbType.Decimal, vobjProduct.SellingPrice);
            db.AddInParameter(dbCommand, "UpdatedBy", DbType.Int32, vobjProduct.UpdatedBy);
            db.AddOutParameter(dbCommand, "Return", DbType.Int32, 4);
            db.ExecuteNonQuery(dbCommand);
            int mintReturn = int.Parse(db.GetParameterValue(dbCommand, "@Return").ToString());
            return mintReturn;
        }

        public BLL.BusinessObject.Product GetProductByID(int ProductID)
        {
            BLL.BusinessObject.Product objData = new BLL.BusinessObject.Product();
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_GetProductByID");
            db.AddInParameter(dbCommand, "ProductID", DbType.Int32, ProductID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {
                    objData = PopulateData(dataReader);
                }

            }
            return objData;
        }

        private BLL.BusinessObject.Product PopulateData(IDataReader dataReader)
        {
            BLL.BusinessObject.Product objData = new BLL.BusinessObject.Product();
            if (dataReader["ProductID"] != DBNull.Value) { objData.ProductID = (int)dataReader["ProductID"]; }
            if (dataReader["ProductName"] != DBNull.Value) { objData.ProductName = (string)dataReader["ProductName"]; }
            if (dataReader["Description"] != DBNull.Value) { objData.Description = (string)dataReader["Description"]; }
            if (dataReader["BrandID"] != DBNull.Value) { objData.BrandID = (int)dataReader["BrandID"]; }
            if (dataReader["SeasonID"] != DBNull.Value) { objData.SeasonID = (int)dataReader["SeasonID"]; }
            if (dataReader["CategoryID"] != DBNull.Value) { objData.CategoryID = (int)dataReader["CategoryID"]; }
            if (dataReader["SizeID"] != DBNull.Value) { objData.SizeID = (string)dataReader["SizeID"]; }
            if (dataReader["BuyingPrice"] != DBNull.Value) { objData.BuyingPrice = (decimal)dataReader["BuyingPrice"]; }
            if (dataReader["SellingPrice"] != DBNull.Value) { objData.SellingPrice = (decimal)dataReader["SellingPrice"]; }
            if (dataReader["Tax"] != DBNull.Value) { objData.Tax = (decimal)dataReader["Tax"]; }
            if (dataReader["Margin"] != DBNull.Value) { objData.Margin = (decimal)dataReader["Margin"]; }
            if (dataReader["BarCode"] != DBNull.Value) { objData.BarCode = (string)dataReader["BarCode"]; }

            return objData;
        }

        public bool DeleteProduct(int ProductID)
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_DeleteProduct");

            db.AddInParameter(dbCommand, "ProductID", DbType.Int32, ProductID);
            return (db.ExecuteNonQuery(dbCommand) == 1);
        }

        public List<BLL.BusinessObject.Product> GetStockByDate(PageInfo vobjPageInfo, string strDate)
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");
            List<BLL.BusinessObject.Product> lstobjData = new List<BLL.BusinessObject.Product>();
            try
            {

                object[] mParams = {
                                        new SqlParameter("@SortColumnName", SqlDbType.NVarChar),                                              
                                        new SqlParameter("@SortDirection", SqlDbType.NVarChar),
                                        new SqlParameter("@SearchText", SqlDbType.NVarChar),
                                        new SqlParameter("@StockDate", SqlDbType.NVarChar)
                                };

                mParams[0] = vobjPageInfo.SortColumnName;
                mParams[1] = vobjPageInfo.SortDirection;
                mParams[2] = vobjPageInfo.SearchText;
                mParams[3] = strDate;


                using (IDataReader reader = db.ExecuteReader("sprocCS_GetStockByDate", mParams))
                {
                    while (reader.Read())
                    {
                        lstobjData.Add(PopulateStock(reader));
                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return lstobjData;
        }


        private BLL.BusinessObject.Product PopulateStock(IDataReader dataReader)
        {
            BLL.BusinessObject.Product obj = new BLL.BusinessObject.Product();

            if (dataReader["ProductID"] != DBNull.Value) { obj.ProductID = (int)dataReader["ProductID"]; }
            if (dataReader["ProductName"] != DBNull.Value) { obj.ProductName = (string)dataReader["ProductName"]; }
            if (dataReader["Description"] != DBNull.Value) { obj.Description = (string)dataReader["Description"]; }
            if (dataReader["BarCode"] != DBNull.Value) { obj.BarCode = (string)dataReader["BarCode"]; }
            if (dataReader["Brand"] != DBNull.Value) { obj.Brand = (string)dataReader["Brand"]; }
            if (dataReader["BuyingPrice"] != DBNull.Value) { obj.BuyingPrice = (decimal)dataReader["BuyingPrice"]; }
            if (dataReader["SellingPrice"] != DBNull.Value) { obj.SellingPrice = (decimal)dataReader["SellingPrice"]; }
            if (dataReader["Tax"] != DBNull.Value) { obj.Tax = (decimal)dataReader["Tax"]; }
            if (dataReader["Margin"] != DBNull.Value) { obj.Margin = (decimal)dataReader["Margin"]; }
            if (dataReader["BarCode"] != DBNull.Value) { obj.BarCode = (string)dataReader["BarCode"]; }
            if (dataReader["Season"] != DBNull.Value) { obj.Season = (string)dataReader["Season"]; }
            if (dataReader["Stock"] != DBNull.Value) { obj.Stock = dataReader["Stock"].ToString(); }
            if (dataReader["StockDetails"] != DBNull.Value) { obj.StockDetails = dataReader["StockDetails"].ToString(); }
            return obj;
        }
    }
}