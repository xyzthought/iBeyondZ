using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BLL.BusinessObject
{
    public class User
    {
        private int mintUserID;
        private string mstrLoginID;
        private string mstrPassword;
       

        public int UserID
        {
            get { return mintUserID; }
            set { mintUserID = value; }
        }
       
    }
}
