using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public class Size
    {
        private int mintSizeID;
        public int SizeID
        {
            get { return mintSizeID; }
            set { mintSizeID = value; }
        }

        private string mstrSizeName;
        public string SizeName
        {
            get { return mstrSizeName; }
            set { mstrSizeName = value; }
        }
    }
}
