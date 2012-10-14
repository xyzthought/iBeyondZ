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
    public class SeasonDB
    {
        public List<BLL.BusinessObject.Season> GetSeason()
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_GetSeason");
            // db.AddInParameter(dbCommand, "CategoryID", DbType.String, CategoryID);
            List<BLL.BusinessObject.Season> list = new List<BLL.BusinessObject.Season>();

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {

                    BLL.BusinessObject.Season obj = new BLL.BusinessObject.Season();

                    if (dataReader["SeasonID"] != DBNull.Value) { obj.SeasonID = (int)dataReader["SeasonID"]; }

                    if (dataReader["Season"] != DBNull.Value) { obj.SeasonName = (string)dataReader["Season"]; }

                    list.Add(obj);
                }


                return list;
            }
        }

        public int AddEditSeason(int SeasonID, string SeasonName)
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_AddEditSeason");

            db.AddInParameter(dbCommand, "SeasonID", DbType.Int32, SeasonID);
            db.AddInParameter(dbCommand, "Season", DbType.String, SeasonName);
            db.AddOutParameter(dbCommand, "Return", DbType.Int32, 4);
            db.ExecuteNonQuery(dbCommand);

            int mintReturn = int.Parse(db.GetParameterValue(dbCommand, "@Return").ToString());
            return mintReturn;
        }

        public bool DeleteSeason(int SeasonID)
        {
            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");//DatabaseFactory.CreateDatabase(Config);
            DbCommand dbCommand = db.GetStoredProcCommand("sprocCS_DeleteSeason ");

            db.AddInParameter(dbCommand, "SeasonID", DbType.Int32, SeasonID);

            return (db.ExecuteNonQuery(dbCommand) == 1);
        }
    }
}
