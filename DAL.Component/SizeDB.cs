using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.BusinessObject;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data;
using System.Data.Common;

namespace DAL.Component
{
    public class SizeDB
    {
        public List<Size> GetSize()
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_GetSize");
            // db.AddInParameter(dbCommand, "CategoryID", DbType.String, CategoryID);
            List<BLL.BusinessObject.Size> list = new List<BLL.BusinessObject.Size>();

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {

                    BLL.BusinessObject.Size obj = new BLL.BusinessObject.Size();

                    if (dataReader["SizeID"] != DBNull.Value) { obj.SizeID = (int)dataReader["SizeID"]; }

                    if (dataReader["SizeName"] != DBNull.Value) { obj.SizeName = (string)dataReader["SizeName"]; }

                    list.Add(obj);
                }


                return list;
            }
        }
    }
}
