using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Component
{
    public class CategoryBLL
    {
        public List<BusinessObject.Category> GetCategory(int CategoryID)
        {
            return new DAL.Component.CategoryDB().GetCategory();
        }
    }
}
