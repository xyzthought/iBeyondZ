using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Component
{
    public class BrandBLL
    {
        public List<BusinessObject.Brand> GetBrand()
        {
            return new DAL.Component.BrandDB().GetBrand();
        }

        public int AddEditBrand(int BrandID, string BrandName)
        {
            return new DAL.Component.BrandDB().AddEditBrand(BrandID, BrandName);
        }

        public int DeleteBrand(int BrandID)
        {
            return new DAL.Component.BrandDB().DeleteBrand(BrandID);
        }
    }
}
