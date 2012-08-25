using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public class Message
    {
        private int mintReturnValue;
        private string mstrReturnMessage;
       

        public int ReturnValue
        {
            get { return mintReturnValue; }
            set { mintReturnValue = value; }
        }

        public string ReturnMessage
        {
            get { return mstrReturnMessage; }
            set { mstrReturnMessage = value; }
        }

       
    }
}
