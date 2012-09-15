using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public class Report
    {
        private int mintProductID;
        private string mstrProductName;
        private string mstrSizeName;
        private decimal mdblQuantity;
        private decimal mdblPrice;

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
        public decimal Price
        {
            get { return mdblPrice; }
            set { mdblPrice = value; }
        }

    }
}
