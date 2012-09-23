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

namespace DAL.Component
{
    public class Product
    {
        public List<BLL.BusinessObject.Product> GetAllProduct(PageInfo vobjPageInfo)
        {

            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_GetProducts");
            db.AddInParameter(dbCommand, "SortColumnName", DbType.String, vobjPageInfo.SortColumnName);
            db.AddInParameter(dbCommand, "SortDirection", DbType.String, vobjPageInfo.SortDirection);
            db.AddInParameter(dbCommand, "SearchText", DbType.String, vobjPageInfo.SearchText);

            List<BLL.BusinessObject.Product> list = new List<BLL.BusinessObject.Product>();

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {

                    BLL.BusinessObject.Product obj = new BLL.BusinessObject.Product();

                    if (dataReader["ProductID"] != DBNull.Value) { obj.ProductID = (int)dataReader["ProductID"]; }
                    if (dataReader["ProductName"] != DBNull.Value) { obj.ProductName = (string)dataReader["ProductName"]; }
                    if (dataReader["Description"] != DBNull.Value) { obj.Description = (string)dataReader["Description"]; }
                    if (dataReader["ManufacturerID"] != DBNull.Value) { obj.ManufacturerID = (int)dataReader["ManufacturerID"]; }

                    if (dataReader["Manufacturer"] != DBNull.Value) { obj.Manufacturer = (string)dataReader["Manufacturer"]; }
                    if (dataReader["CategoryName"] != DBNull.Value) { obj.CategoryName = (string)dataReader["CategoryName"]; }
                    //     if (dataReader["SizeName"] != DBNull.Value) { obj.SizeName = (string)dataReader["SizeName"]; }

                    if (dataReader["CategoryID"] != DBNull.Value) { obj.CategoryID = (int)dataReader["CategoryID"]; }
                    //if (dataReader["SizeID"] != DBNull.Value) { obj.SizeID = (string)dataReader["SizeID"]; }
                    if (dataReader["BuyingPrice"] != DBNull.Value) { obj.BuyingPrice = (decimal)dataReader["BuyingPrice"]; }
                    if (dataReader["Tax"] != DBNull.Value) { obj.Tax = (decimal)dataReader["Tax"]; }
                    if (dataReader["Margin"] != DBNull.Value) { obj.Margin = (decimal)dataReader["Margin"]; }
                    if (dataReader["BarCode"] != DBNull.Value) { obj.BarCode = (string)dataReader["BarCode"]; }
                    //if (dataReader["CreatedOn"] != DBNull.Value) { obj.CreatedOn = (DateTime)dataReader["CreatedOn"]; }
                    //if (dataReader["UpdatedOn"] != DBNull.Value) { obj.UpdatedOn = (DateTime)dataReader["UpdatedOn"]; }
                    //if (dataReader["CreatedBy"] != DBNull.Value) { obj.CreatedBy = (int)dataReader["CreatedBy"]; }
                    //if (dataReader["UpdatedBy"] != DBNull.Value) { obj.UpdatedBy = (int)dataReader["UpdatedBy"]; }
                    //if (dataReader["IsActive"] != DBNull.Value) { obj.IsActive = (bool)dataReader["IsActive"]; }
                    //if (dataReader["IsDeleted"] != DBNull.Value) { obj.IsDeleted = (bool)dataReader["IsDeleted"]; }

                    list.Add(obj);

                }

            }

            return list;

        }

        public int InsertProduct(BLL.BusinessObject.Product vobjProduct)
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_InsertProduct");

            //db.AddInParameter(dbCommand, "ProductID", DbType.Int32, vobjProduct.ProductID);
            db.AddInParameter(dbCommand, "ProductName", DbType.String, vobjProduct.ProductName);
            db.AddInParameter(dbCommand, "Description", DbType.String, vobjProduct.Description);
            db.AddInParameter(dbCommand, "ManufacturerID", DbType.Int32, vobjProduct.ManufacturerID);
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
            db.AddInParameter(dbCommand, "ManufacturerID", DbType.Int32, vobjProduct.ManufacturerID);
            db.AddInParameter(dbCommand, "CategoryID", DbType.Int32, vobjProduct.CategoryID);
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
            //
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_GetProductByID");
            db.AddInParameter(dbCommand, "ProductID", DbType.Int32, ProductID);

            BLL.BusinessObject.Product obj = new BLL.BusinessObject.Product();
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {



                    if (dataReader["ProductID"] != DBNull.Value) { obj.ProductID = (int)dataReader["ProductID"]; }
                    if (dataReader["ProductName"] != DBNull.Value) { obj.ProductName = (string)dataReader["ProductName"]; }
                    if (dataReader["Description"] != DBNull.Value) { obj.Description = (string)dataReader["Description"]; }
                    if (dataReader["ManufacturerID"] != DBNull.Value) { obj.ManufacturerID = (int)dataReader["ManufacturerID"]; }

                    if (dataReader["Manufacturer"] != DBNull.Value) { obj.Manufacturer = (string)dataReader["Manufacturer"]; }
                    if (dataReader["CategoryName"] != DBNull.Value) { obj.CategoryName = (string)dataReader["CategoryName"]; }
                    // if (dataReader["SizeName"] != DBNull.Value) { obj.SizeName = (string)dataReader["SizeName"]; }

                    if (dataReader["CategoryID"] != DBNull.Value) { obj.CategoryID = (int)dataReader["CategoryID"]; }
                    if (dataReader["SizeID"] != DBNull.Value) { obj.SizeID = (string)dataReader["SizeID"]; }
                    if (dataReader["BuyingPrice"] != DBNull.Value) { obj.BuyingPrice = (decimal)dataReader["BuyingPrice"]; }
                    if (dataReader["SellingPrice"] != DBNull.Value) { obj.SellingPrice = (decimal)dataReader["SellingPrice"]; }
                    if (dataReader["Tax"] != DBNull.Value) { obj.Tax = (decimal)dataReader["Tax"]; }
                    if (dataReader["Margin"] != DBNull.Value) { obj.Margin = (decimal)dataReader["Margin"]; }
                    if (dataReader["BarCode"] != DBNull.Value) { obj.BarCode = (string)dataReader["BarCode"]; }
                    //if (dataReader["CreatedOn"] != DBNull.Value) { obj.CreatedOn = (DateTime)dataReader["CreatedOn"]; }
                    //if (dataReader["UpdatedOn"] != DBNull.Value) { obj.UpdatedOn = (DateTime)dataReader["UpdatedOn"]; }
                    //if (dataReader["CreatedBy"] != DBNull.Value) { obj.CreatedBy = (int)dataReader["CreatedBy"]; }
                    //if (dataReader["UpdatedBy"] != DBNull.Value) { obj.UpdatedBy = (int)dataReader["UpdatedBy"]; }
                    //if (dataReader["IsActive"] != DBNull.Value) { obj.IsActive = (bool)dataReader["IsActive"]; }
                    //if (dataReader["IsDeleted"] != DBNull.Value) { obj.IsDeleted = (bool)dataReader["IsDeleted"]; }



                }

            }
            return obj;
        }

        public bool DeleteProduct(int ProductID)
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_DeleteProduct");

            db.AddInParameter(dbCommand, "ProductID", DbType.Int32, ProductID);
            return (db.ExecuteNonQuery(dbCommand) == 1);
        }
    }
}
