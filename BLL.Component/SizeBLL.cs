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
    }
}
