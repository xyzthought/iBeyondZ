using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Component;
using BLL.BusinessObject;

namespace BLL.Component
{
    public class ProductPurchaseBLL
    {
        public List<ProductPurchase> GetAll(String purchaseStartDate, String purchaseEndDate, String ManufacturerName, PageInfo vobjPageInfo)
        {
            return new ProductPurchaseDB().GetAll(purchaseStartDate, purchaseEndDate, ManufacturerName, vobjPageInfo);
        }
        public void GetByID(ref ProductPurchase vobjProductPurchase)
        {
            new ProductPurchaseDB().GetByID(ref vobjProductPurchase);
        }
        public void AddEditPurchase(ref ProductPurchase vobjProductPurchase)
        {
            new ProductPurchaseDB().AddEditPurchase(ref vobjProductPurchase);
        }
        /*public void Add(ref ProductPurchase vobjProductPurchase)
        {
            new ProductPurchaseDB().Add(ref vobjProductPurchase);
        }
        public void Update(ref ProductPurchase vobjProductPurchase)
        {
            new ProductPurchaseDB().Update(ref vobjProductPurchase);
        }*/
        public void Delete(ref ProductPurchase vobjProductPurchase)
        {
            new ProductPurchaseDB().Delete(ref vobjProductPurchase);
        }
    }
}
