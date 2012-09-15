using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mail;
using System.ComponentModel;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using BLL.BusinessObject;
using CSWeb.Utility;
using System.Diagnostics;

namespace DAL.Component
{
    public class ReportDB
    {
        Database dBase = EnterpriseLibraryContainer.Current.GetInstance<Database>("CSWebDSN");

        public List<Report> GetTopSellingProduct(List<Report> objData, PageInfo vobjPageInfo)
        {
            List<Report> lstobjReport = new List<Report>();
            try
            {

                object[] mParams = {
                                        new SqlParameter("@SortColumnName", SqlDbType.NVarChar),                                              
                                        new SqlParameter("@SortDirection", SqlDbType.NVarChar),
                                        new SqlParameter("@SearchText", SqlDbType.NVarChar)
                                };

                mParams[0] = vobjPageInfo.SortColumnName;
                mParams[1] = vobjPageInfo.SortDirection;
                mParams[2] = vobjPageInfo.SearchText;

                using (IDataReader reader = dBase.ExecuteReader("sprocCS_GetTop10SellingProduct", mParams))
                {
                    while (reader.Read())
                    {
                        lstobjReport.Add(PopulateReport(reader));
                    }
                }
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return lstobjReport;
        }

        #region Get All Report
        private Report PopulateReport(IDataReader drData)
        {
            Report objReport = new Report();
            try
            {

                if (FieldExists(drData, "ProductID") && drData["ProductID"] != DBNull.Value)
                {
                    objReport.ProductID = Convert.ToInt32(drData["ProductID"]);
                }
                if (FieldExists(drData, "ProductName") && drData["ProductName"] != DBNull.Value)
                {
                    objReport.ProductName = Convert.ToString(drData["ProductName"]);
                }

                if (FieldExists(drData, "SizeName") && drData["SizeName"] != DBNull.Value)
                {
                    objReport.SizeName = Convert.ToString(drData["SizeName"]);
                }

                if (FieldExists(drData, "Quantity") && drData["Quantity"] != DBNull.Value)
                {
                    objReport.Quantity = Convert.ToInt32(drData["Quantity"]);
                }

                if (FieldExists(drData, "Price") && drData["Price"] != DBNull.Value)
                {
                    objReport.Price = Convert.ToDecimal(drData["Price"]);
                } 

            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return objReport;
        } 
        #endregion

        public bool FieldExists(IDataReader reader, string fieldName)
        {
            try
            {
                reader.GetSchemaTable().DefaultView.RowFilter = string.Format("ColumnName= '{0}'", fieldName);
            }
            catch (Exception ex)
            {

                Common.LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
            return (reader.GetSchemaTable().DefaultView.Count > 0);
        }
    }
}
