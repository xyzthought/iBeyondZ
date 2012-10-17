using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public class ProductPurchase : Message
    {
        public int ProductPurchaseID {get;set;}
        public int ProductID {get;set;}
        public int ManufacturerID {get;set;}
        public int BrandID {get;set;}
        public int CategoryID {get; set;}
        public int SeasonID {get; set;}
        public DateTime PurchaseDate {get; set;}
        public int Quantity {get; set;}
        public decimal BuyingPrice {get; set;}
        public decimal Tax {get; set;}
        public decimal Margin { get; set; }
        public decimal SellingPrice { get; set; }
        public string BarCode { get; set; }
        public int UpdatedBy { get; set; }
        public string ManufacturerName { get; set; }
        public string SizeIDs { get; set; }
        public string ProductName { get; set; }
        public string Sizes { get; set; }            
        
        /*
        private int mintPurchaseID;
        private int mintManufacturerID;
        private string mstrManufacturerName;
        private DateTime mdtPurchaseDate;
        private int mintProductID;
        private string mstrProductName;
        private int mintSizeID;
        private string mstrSizeName;
        private int mintQuantity;
        private decimal mdcmPrice;

        public int PurchaseID
        {
            get { return mintPurchaseID; }
            set { mintPurchaseID = value; }
        }
        public int ManufacturerID
        {
            get { return mintManufacturerID; }
            set { mintManufacturerID = value; }
        }
        public string ManufacturerName
        {
            get { return mstrManufacturerName; }
            set { mstrManufacturerName = value; }
        }
        public DateTime PurchaseDate
        {
            get { return mdtPurchaseDate; }
            set { mdtPurchaseDate = value; }
        }
        public int ProductID
        {
            get { return mintProductID; }
            set { mintProductID = value; }
        }
        public string ProductName
        {
            get { return mstrProductName; }
            set { mstrProductName = value; }
        }
        public int SizeID
        {
            get { return mintSizeID; }
            set { mintSizeID = value; }
        }
        public string SizeName
        {
            get { return mstrSizeName; }
            set { mstrSizeName = value; }
        }
        public int Quantity
        {
            get { return mintQuantity; }
            set { mintQuantity = value; }
        }
        public decimal Price
        {
            get { return mdcmPrice; }
            set { mdcmPrice = value; }
        }
         */

    }
}
