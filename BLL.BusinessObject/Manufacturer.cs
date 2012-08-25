using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public class Manufacturer : Message
    {

        private int mintManufacturerID;
        private string mstrCompanyName;
        private string mstrContactFirstName;
        private string mstrContactLastName;
        private string mstrAddress;
        private string mstrZIP;
        private string mstrCity;
        private string mstrCountry;
        private string mstrPhone;
        private string mstrEmail;
        private int mintCreatedBy;
        private int mintUpdatedBy;
        private bool mblnIsActive;

        public int ManufacturerID
        {
            get { return mintManufacturerID; }
            set { mintManufacturerID = value; }
        }
        public string CompanyName
        {
            get { return mstrCompanyName; }
            set { mstrCompanyName = value; }
        }
        public string ContactFirstName
        {
            get { return mstrContactFirstName; }
            set { mstrContactFirstName = value; }
        }
        public string ContactLastName
        {
            get { return mstrContactLastName; }
            set { mstrContactLastName = value; }
        }
        public string Address
        {
            get { return mstrAddress; }
            set { mstrAddress = value; }
        }
        public string ZIP
        {
            get { return mstrZIP; }
            set { mstrZIP = value; }
        }
        public string City
        {
            get { return mstrCity; }
            set { mstrCity = value; }
        }
        public string Country
        {
            get { return mstrCountry; }
            set { mstrCountry = value; }
        }
        public string Phone
        {
            get { return mstrPhone; }
            set { mstrPhone = value; }
        }
        public string Email
        {
            get { return mstrEmail; }
            set { mstrEmail = value; }
        }
        public int CreatedBy
        {
            get { return mintCreatedBy; }
            set { mintCreatedBy = value; }
        }
        public int UpdatedBy
        {
            get { return mintUpdatedBy; }
            set { mintUpdatedBy = value; }
        }

             public bool IsActive
        {
            get { return mblnIsActive; }
            set { mblnIsActive = value; }
        }
    }
}



