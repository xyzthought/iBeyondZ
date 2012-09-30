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

        public List<Sale> GetAllSaleDataByDate(List<Sale> objData, PageInfo objPI, string strDate)
        {
            return objDB.GetAllSaleDataByDate(objData, objPI, strDate);
        }

        public List<Sale> GetProductDetailByBarCode(string vstrProductBarcode)
        {
            return objDB.GetProductDetailByBarCode(vstrProductBarcode);
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
    }
}
