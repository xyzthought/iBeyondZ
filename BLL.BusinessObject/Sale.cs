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
        private decimal mdblStandardRebate;
        private decimal mdblDiscount;
        private string mstrSaleMadeBy;

        public long SaleID
        {
            get { return mlngSaleID; }
            set { mlngSaleID = value; }
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
        public string SaleMadeBy
        {
            get { return mstrSaleMadeBy; }
            set { mstrSaleMadeBy = value; }
        }

    }
}
