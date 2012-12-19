using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Component;
using System.Data;
using BLL.BusinessObject;

namespace BLL.Component
{
    public class SaleBLL
    {
        SaleDB objDB;
        public SaleBLL()
        {
            objDB = new SaleDB();
        }

        public List<Sale> GetAllSaleDataByDate(List<Sale> objData, PageInfo objPI, string strFromDate, string strToDate)
        {
            return objDB.GetAllSaleDataByDate(objData, objPI, strFromDate, strToDate);
        }

        public List<Sale> GetProductDetailByBarCode(string vstrProductBarcode)
        {
            return objDB.GetProductDetailByBarCode(vstrProductBarcode);
        }

        public List<Sale> GetProductInfoByBarCode(string vstrProductBarcode)
        {
            return objDB.GetProductInfoByBarCode(vstrProductBarcode);
        }

        public DataTable GetAllCustomerNameForAutoComplete()
        {
            return objDB.GetAllCustomerNameForAutoComplete();
        }

        public DataTable PopulateAutoCompleteProductInformation()
        {
            return objDB.PopulateAutoCompleteProductInformation();
        }

        public Message InsertUpdateSaleMaster(Sale objSale)
        {
            return objDB.InsertUpdateSaleMaster(objSale);
        }

        public Message InsertUpdateSaleDetail(Sale objSale)
        {
            return objDB.InsertUpdateSaleDetail(objSale);
        }

        public Message DeleteSale(int intSaleID, int intUserID)
        {
            return objDB.DeleteSale(intSaleID, intUserID);
        }

        public List<Sale> GetSaleDetailBySaleID(Sale objSale)
        {
            return objDB.GetSaleDetailBySaleID(objSale);
        }

        public Sale GetFinalCheckOutDeatils(ref Sale objSale)
        {
            return objDB.GetFinalCheckOutDeatils(ref objSale);
        }

        public Message DeleteExistingSalesDetails(Sale objSale)
        {
            return objDB.DeleteExistingSalesDetails(objSale);
        }

        public List<Sale> GetAllProductBarCode(PageInfo objPI, int PurchaseID)
        {
            return objDB.GetAllProductBarCode(objPI, PurchaseID);
        }

        public List<Sale> GetProductBarCode(string thisBarcode, int Quantity)
        {
            return objDB.GetProductBarCode(thisBarcode, Quantity);
        }

        public DataTable SearchProductAutoCompleteForPurchase()
        {
            return objDB.SearchProductAutoCompleteForPurchase();
        }
    }
}
