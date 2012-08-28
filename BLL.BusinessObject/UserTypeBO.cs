using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public class UserTypeBO
    {

        private int mintUsertypeID;
        private string mstrUserType;

        public int UsertypeID
        {
            get { return mintUsertypeID; }
            set { mintUsertypeID = value; }
        }
        public string UserType
        {
            get { return mstrUserType; }
            set { mstrUserType = value; }
        }

    }
}
