using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;
using System.Data;

namespace DAL.Component
{
    public class BrandDB
    {
        public List<BLL.BusinessObject.Brand> GetBrand()
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_GetBrand");
            // db.AddInParameter(dbCommand, "CategoryID", DbType.String, CategoryID);
            List<BLL.BusinessObject.Brand> list = new List<BLL.BusinessObject.Brand>();

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {

                    BLL.BusinessObject.Brand obj = new BLL.BusinessObject.Brand();

                    if (dataReader["BrandID"] != DBNull.Value) { obj.BrandID = (int)dataReader["BrandID"]; }

                    if (dataReader["Brand"] != DBNull.Value) { obj.BrandName = (string)dataReader["Brand"]; }

                    list.Add(obj);
                }


                return list;
            }
        }

        public int AddEditBrand(int BrandID, string BrandName)
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_AddEditBrand");

            db.AddInParameter(dbCommand, "BrandID", DbType.Int32, BrandID);
            db.AddInParameter(dbCommand, "Brand", DbType.String, BrandName);
            db.AddOutParameter(dbCommand, "Return", DbType.Int32, 4);
            db.ExecuteNonQuery(dbCommand);

            int mintReturn = int.Parse(db.GetParameterValue(dbCommand, "@Return").ToString());
            return mintReturn;
        }

        public int DeleteBrand(int BrandID)
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_DeleteBrand ");

            db.AddInParameter(dbCommand, "BrandID", DbType.Int32, BrandID);
            db.AddOutParameter(dbCommand, "Return", DbType.Int32, 4);
            db.ExecuteNonQuery(dbCommand);
            int mintReturn = int.Parse(db.GetParameterValue(dbCommand, "@Return").ToString());
            return mintReturn;
        }
    }
}
