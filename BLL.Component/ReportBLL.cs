using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Component;
using System.Data;
using BLL.BusinessObject;

namespace BLL.Component
{
    public class ReportBLL
    {
        ReportDB objDB;
        public ReportBLL()
        {
            objDB = new ReportDB();
        }

       

        public List<Report> GetTopSellingProduct(List<Report> objData, PageInfo objPI)
        {
            return objDB.GetTopSellingProduct(objData, objPI);
        }
    }
}
