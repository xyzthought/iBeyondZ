using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Component
{
    public class SeasonBLL
    {
        public List<BusinessObject.Season> GetSeason()
        {
            return new DAL.Component.SeasonDB().GetSeason();
        }

        public int AddEditSeason(int SeasonID, string SeasonName)
        {
            return new DAL.Component.SeasonDB().AddEditSeason(SeasonID, SeasonName);
        }

        public bool DeleteSeason(int SeasonID)
        {
            return new DAL.Component.SeasonDB().DeleteSeason(SeasonID);
        }
    }
}
