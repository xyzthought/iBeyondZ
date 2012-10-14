using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public class ProductPurchase : Message
    {
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

    }
}
