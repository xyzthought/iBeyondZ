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


        public int InsertSize(string SizeName,string SizeBarCode)
        {
            return new DAL.Component.SizeDB().InsertSize(SizeName, SizeBarCode);
        }

        public int UpdateSize(int SizeID, string SizeName, string SizeBarCode)
        {
            return new DAL.Component.SizeDB().UpdateSize(SizeID, SizeName, SizeBarCode);
        }

        public int DeleteSize(int SizeID)
        {
            return new DAL.Component.SizeDB().DeleteSize(SizeID);
        }
    }
}
