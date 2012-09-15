using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BusinessObject;
using BLL.Component;
using CSWeb.Utility;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using CSWeb.Utility;

public partial class Modules_LandingPage : PageBase
{

    PageInfo objPI = new PageInfo();
    string vstrLink = string.Empty;
    string param = string.Empty;
    protected bool iFlag = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            PopulateTopSellingProduct();
            PopulateL7DaysTop10SellingProduct();
        }
    }

    private void PopulateL7DaysTop10SellingProduct()
    {
        try
        {

            if (objPI.SortDirection == null && objPI.SortColumnName == null)
            {
                objPI.SortDirection = Convert.ToString(ViewState[Constants.SORTDERECTION]);
                objPI.SortColumnName = Convert.ToString(ViewState[Constants.SORTCOLUMNNAME]);
                objPI.SearchText = string.Empty;
            }
            ReportBLL objReportBLL = new ReportBLL();

            List<Report> objData = new List<Report>();
            objData = objReportBLL.GetL7DaysTop10SellingProduct(objData, objPI);

            L7DaysTop10.DataSource = objData;
            L7DaysTop10.ExportTemplate = "export_template_4Column.xlsx";
            L7DaysTop10.ExportCaption = "";
            L7DaysTop10.ExcelColumn = "";
            L7DaysTop10.DataBind();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    #region Populate Top Selling Product
    private void PopulateTopSellingProduct()
    {
       try
        {

            if (objPI.SortDirection == null && objPI.SortColumnName == null)
            {
                objPI.SortDirection = Convert.ToString(ViewState[Constants.SORTDERECTION]);
                objPI.SortColumnName = Convert.ToString(ViewState[Constants.SORTCOLUMNNAME]);
                objPI.SearchText = string.Empty;
            }
            ReportBLL objReportBLL = new ReportBLL();

            List<Report> objData = new List<Report>();
            objData = objReportBLL.GetTopSellingProduct(objData, objPI);

            gvGridTopSellingProduct.DataSource = objData;
            gvGridTopSellingProduct.ExportTemplate = "export_template_4Column.xlsx";
            gvGridTopSellingProduct.ExportCaption = "";
            gvGridTopSellingProduct.ExcelColumn = "";
            gvGridTopSellingProduct.DataBind();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    } 
    #endregion

    #region GRID VIEW EVENTS
    protected void gvGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            divMess.Visible = false;
            lblMsg.Text = "search";
            if (Convert.ToString(e.SortDirection) == Convert.ToString(SortDirection.Ascending))
            {
                e.SortDirection = SortDirection.Descending;
                objPI.SortDirection = Constants.DESC;
                ViewState[Constants.SORTDERECTION] = Constants.DESC;
            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                objPI.SortDirection = Constants.ASC;
                ViewState[Constants.SORTDERECTION] = Constants.ASC;
            }
            //objPI.SortDirection = e.SortDirection.ToString();
            objPI.SortColumnName = e.SortExpression;
            ViewState[Constants.SORTCOLUMNNAME] = e.SortExpression;
            PopulateTopSellingProduct();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
    #endregion
}