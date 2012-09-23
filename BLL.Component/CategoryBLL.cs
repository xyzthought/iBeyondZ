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

        public int AddEditCategory(int CategoryID, string CategoryName)
        {
            return new DAL.Component.CategoryDB().AddEditCategory(CategoryID, CategoryName);
        }

        public bool DeleteCategory(int CategoryID)
        {
            return new DAL.Component.CategoryDB().DeleteCategory(CategoryID);
        }
    }
}
