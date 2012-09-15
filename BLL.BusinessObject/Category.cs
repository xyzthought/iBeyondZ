using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public class Category
    {
        private int mintCategoryID;
        public int CategoryID { get { return mintCategoryID; } set { mintCategoryID = value; } }
        private string mstrCategoryName;
        public string CategoryName { get { return mstrCategoryName; } set { mstrCategoryName = value; } }
    }
}
