using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Component;
using System.Data;

namespace BLL.Component
{
    public class UserBLL
    {
        UserDB objDB;
        public UserBLL()
        {
            objDB = new UserDB();
        }

        public DataTable GetSourceData()
        {
            return objDB.GetSourceData(); 
        }

        public void SaveSourceDataTable(DataTable dtSource)
        {
            objDB.SaveSourceDataTable(dtSource);
        }
    }
}
