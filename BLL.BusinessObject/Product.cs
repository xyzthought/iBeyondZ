﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public class Product
    {
        private int mintProductID;
        public int ProductID { get { return mintProductID; } set { mintProductID = value; } }
        private string mstrProductName;
        public string ProductName { get { return mstrProductName; } set { mstrProductName = value; } }
        private string mstrDescription;
        public string Description { get { return mstrDescription; } set { mstrDescription = value; } }
        private int mintManufacturerID;
        public int ManufacturerID { get { return mintManufacturerID; } set { mintManufacturerID = value; } }
        private int mintCategoryID;
        public int CategoryID { get { return mintCategoryID; } set { mintCategoryID = value; } }
        private int mintSizeID;
        public int SizeID { get { return mintSizeID; } set { mintSizeID = value; } }
        private decimal mdcmBuyingPrice;
        public decimal BuyingPrice { get { return mdcmBuyingPrice; } set { mdcmBuyingPrice = value; } }
        private decimal mdcmTax;
        public decimal Tax { get { return mdcmTax; } set { mdcmTax = value; } }
        private decimal mdcmMargin;
        public decimal Margin { get { return mdcmMargin; } set {  mdcmMargin=value; } }
        private string mstrBarCode;
        public string BarCode { get { return mstrBarCode; } set { mstrBarCode = value; } }
        private DateTime mdtmCreatedOn;
        public DateTime CreatedOn { get { return mdtmCreatedOn; } set { mdtmCreatedOn = value; } }
        private DateTime mdtmUpdatedOn;
        public DateTime UpdatedOn { get { return mdtmUpdatedOn; } set { mdtmUpdatedOn = value; } }
        private int mintCreatedBy;
        public int CreatedBy { get { return mintCreatedBy; } set { mintCreatedBy = value; } }
        private int mintUpdatedBy;
        public int UpdatedBy { get { return mintUpdatedBy; } set { mintUpdatedBy = value; } }
        private bool mblnIsActive;
        public bool IsActive { get { return mblnIsActive; } set { mblnIsActive = value; } }
        private bool mblnIsDeleted;
        public bool IsDeleted { get { return mblnIsDeleted; } set { mblnIsDeleted = value; } }


    }
}