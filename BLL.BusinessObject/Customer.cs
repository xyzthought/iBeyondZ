using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public class Customer
    {
        private int mintCustomerID;
        private string mstrFirstName;
        private string mstrLastName;
        private string mstrAddress;
        private string mstrCity;
        private string mstrZIP;
        private string mstrCountry;
        private string mstrTeleNumber;
        private string mstrEmail;
        private DateTime mdtCreatedOn;
        private int mintCreatedBy;

        public int CustomerID
        {
            get { return mintCustomerID; }
            set { mintCustomerID = value; }
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



        public DateTime UpdatedOn { get; set; }

        public string Notes { get; set; }
    }
}
