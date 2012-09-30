using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public class Sale
    {
        private long mlngSaleID;
        private string mstrSaleOrder;
        private string mstrFirstName;
        private string mstrLastName;
        private DateTime mdtSaleDate;
        private string mstrPaymentMode;
        private decimal mdblPrice;
        private decimal mdblUnitPrice;
        private decimal mdblTax;
        private decimal mdblStandardRebate;
        private decimal mdblDiscount;
       
        private int mintCustomerID;
        private int mintPaymentModeID;
        private decimal mdblCCAmount;
        private decimal mdblBankAmount;
        private decimal mdblCash;
        private int mintSaleMadeBy;
        private string mstrSaleMadeByName;

        private int mintProductID;
        private int mintSizeID;
        private string mstrBarCode;
        private string mstrProductName;
        private string mstrSizeName;
        private decimal mdblQuantity;

        private string mstrCFirstName;
        private string mstrCLastName;
        private string mstrAddress;
        private string mstrCity;
        private string mstrZIP;
        private string mstrCountry;
        private string mstrTeleNumber;
        private string mstrEmail;
        private DateTime mdtCreatedOn;
        private int mintCreatedBy;

       
        public string CFirstName
        {
            get { return mstrCFirstName; }
            set { mstrCFirstName = value; }
        }
        public string CLastName
        {
            get { return mstrCLastName; }
            set { mstrCLastName = value; }
        }
        public string Address
        {
            get { return mstrAddress; }
            set { mstrAddress = value; }
        }
        public string City
        {
            get { return mstrCity; }
            set { mstrCity = value; }
        }
        public string ZIP
        {
            get { return mstrZIP; }
            set { mstrZIP = value; }
        }
        public string Country
        {
            get { return mstrCountry; }
            set { mstrCountry = value; }
        }
        public string TeleNumber
        {
            get { return mstrTeleNumber; }
            set { mstrTeleNumber = value; }
        }
        public string Email
        {
            get { return mstrEmail; }
            set { mstrEmail = value; }
        }
        public DateTime CreatedOn
        {
            get { return mdtCreatedOn; }
            set { mdtCreatedOn = value; }
        }
        public int CreatedBy
        {
            get { return mintCreatedBy; }
            set { mintCreatedBy = value; }
        }


        public long SaleID
        {
            get { return mlngSaleID; }
            set { mlngSaleID = value; }
        }
        public int CustomerID
        {
            get { return mintCustomerID; }
            set { mintCustomerID = value; }
        }
        public int PaymentModeID
        {
            get { return mintPaymentModeID; }
            set { mintPaymentModeID = value; }
        }
        public decimal CCAmount
        {
            get { return mdblCCAmount; }
            set { mdblCCAmount = value; }
        }
        public decimal BankAmount
        {
            get { return mdblBankAmount; }
            set { mdblBankAmount = value; }
        }
        public decimal Cash
        {
            get { return mdblCash; }
            set { mdblCash = value; }
        }

        public decimal Tax
        {
            get { return mdblTax; }
            set { mdblTax = value; }
        }
       
        public int SaleMadeBy
        {
            get { return mintSaleMadeBy; }
            set { mintSaleMadeBy = value; }
        }
        public string SaleMadeByName
        {
            get { return mstrSaleMadeByName; }
            set { mstrSaleMadeByName = value; }
        }


        public int SizeID
        {
            get { return mintSizeID; }
            set { mintSizeID = value; }
        }
       
        public string SaleOrder
        {
            get { return mstrSaleOrder; }
            set { mstrSaleOrder = value; }
        }
        public string FirstName
        {
            get { return mstrFirstName; }
            set { mstrFirstName = value; }
        }
        public string LastName
        {
            get { return mstrLastName; }
            set { mstrLastName = value; }
        }
        public DateTime SaleDate
        {
            get { return mdtSaleDate; }
            set { mdtSaleDate = value; }
        }
        public string PaymentMode
        {
            get { return mstrPaymentMode; }
            set { mstrPaymentMode = value; }
        }
        public decimal Price
        {
            get { return mdblPrice; }
            set { mdblPrice = value; }
        }

        public decimal UnitPrice
        {
            get { return mdblUnitPrice; }
            set { mdblUnitPrice = value; }
        }
        public decimal StandardRebate
        {
            get { return mdblStandardRebate; }
            set { mdblStandardRebate = value; }
        }
        public decimal Discount
        {
            get { return mdblDiscount; }
            set { mdblDiscount = value; }
        }
        

      

        

        public int ProductID
        {
            get { return mintProductID; }
            set { mintProductID = value; }
        }

        public string BarCode
        {
            get { return mstrBarCode; }
            set { mstrBarCode = value; }
        }
        public string ProductName
        {
            get { return mstrProductName; }
            set { mstrProductName = value; }
        }
        public string SizeName
        {
            get { return mstrSizeName; }
            set { mstrSizeName = value; }
        }

        public decimal Quantity
        {
            get { return mdblQuantity; }
            set { mdblQuantity = value; }
        }
    }
}
