using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Component
{
    public class SizeBLL
    {
        public List<BusinessObject.Size> GetSize(int SizeID)
        {
            return new DAL.Component.SizeDB().GetSize();
        }


        public int InsertSize(string SizeName)
        {
            return new DAL.Component.SizeDB().InsertSize(SizeName);
        }

        public int UpdateSize(int SizeID, string SizeName)
        {
            return new DAL.Component.SizeDB().UpdateSize(SizeID, SizeName);
        }

        public int DeleteSize(int SizeID)
        {
            return new DAL.Component.SizeDB().DeleteSize(SizeID);
        }
    }
}
