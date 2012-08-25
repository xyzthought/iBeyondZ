using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
   public class Message
    {
        private int mintReturnStatus = 1;
        private long mintReturnValue = -1;
        private string mstrReturnMessage;

        public int ReturnStatus
        {
            get { return mintReturnStatus; }
            set { mintReturnStatus = value; }
        }

        public long ReturnValue
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

