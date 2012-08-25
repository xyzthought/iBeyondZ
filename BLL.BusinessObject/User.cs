using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BLL.BusinessObject
{
    public class User
    {
        private int mintUserID;
        private int mintUserTypeID;
        private string mstrFirstName;
        private string mstrLastName;
        private string mstrLoginID;
        private string mstrLoginPassword;
        private string mstrCommunicationEmailID;
        private DateTime mdtLastLoggedIn;
        private DateTime mdtCreatedOn;
        private DateTime mdtUpdatedOn;
        private bool mblnIsActive;
        private bool mblnIsDeleted;

        public int UserID
        {
            get { return mintUserID; }
            set { mintUserID = value; }
        }
        public int UserTypeID
        {
            get { return mintUserTypeID; }
            set { mintUserTypeID = value; }
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
        public string LoginID
        {
            get { return mstrLoginID; }
            set { mstrLoginID = value; }
        }
        public string LoginPassword
        {
            get { return mstrLoginPassword; }
            set { mstrLoginPassword = value; }
        }
        public string CommunicationEmailID
        {
            get { return mstrCommunicationEmailID; }
            set { mstrCommunicationEmailID = value; }
        }
        public DateTime LastLoggedIn
        {
            get { return mdtLastLoggedIn; }
            set { mdtLastLoggedIn = value; }
        }
        public DateTime CreatedOn
        {
            get { return mdtCreatedOn; }
            set { mdtCreatedOn = value; }
        }
        public DateTime UpdatedOn
        {
            get { return mdtUpdatedOn; }
            set { mdtUpdatedOn = value; }
        }
        public bool IsActive
        {
            get { return mblnIsActive; }
            set { mblnIsActive = value; }
        }
        public bool IsDeleted
        {
            get { return mblnIsDeleted; }
            set { mblnIsDeleted = value; }
        }

       
       
    }
}
