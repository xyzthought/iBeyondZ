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
    public class CategoryDB
    {
        public List<Category> GetCategory()
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_GetCategory");
            // db.AddInParameter(dbCommand, "CategoryID", DbType.String, CategoryID);
            List<BLL.BusinessObject.Category> list = new List<BLL.BusinessObject.Category>();

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {

                    BLL.BusinessObject.Category obj = new BLL.BusinessObject.Category();

                    if (dataReader["CategoryID"] != DBNull.Value) { obj.CategoryID = (int)dataReader["CategoryID"]; }

                    if (dataReader["CategoryName"] != DBNull.Value) { obj.CategoryName = (string)dataReader["CategoryName"]; }

                    list.Add(obj);
                }


                return list;
            }
        }
    }
}