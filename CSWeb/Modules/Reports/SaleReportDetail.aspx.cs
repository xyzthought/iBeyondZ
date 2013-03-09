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


public partial class Modules_SaleReportDetail : PageBase
{

    PageInfo objPI = new PageInfo();
    string vstrLink = string.Empty;
    string param = string.Empty;
    public const string DEFAULTCOLUMNNAME = "FirstName";
    protected bool iFlag = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {


            objPI.SortColumnName = DEFAULTCOLUMNNAME;
            objPI.SortDirection = Constants.DESC;
            ViewState[Constants.SORTCOLUMNNAME] = DEFAULTCOLUMNNAME;
            ViewState[Constants.SORTDERECTION] = Constants.ASC;

            //datepicker.Value = string.Format("{0:dd/MM/yyyy}", DateTime.Today);
            fromDate.Value = string.Format("{0:dd/MM/yyyy}", DateTime.Today);
            toDate.Value = string.Format("{0:dd/MM/yyyy}", DateTime.Today);

            PopulateGrid();
        }
    }


    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {
        PopulateGrid();
    }

   

    #region Populate Grid
    private void PopulateGrid()
    {
        try
        {
            if (!(string.IsNullOrEmpty(fromDate.Value) && string.IsNullOrEmpty(toDate.Value)))
            {
                if (txtSearch.Text.Trim() != "Search")
                {
                    objPI.SearchText = txtSearch.Text.Trim();
                }
                else
                {
                    objPI.SearchText = "";
                }



                if (objPI.SortDirection == null && objPI.SortColumnName == null)
                {
                    objPI.SortDirection = Convert.ToString(ViewState[Constants.SORTDERECTION]);
                    objPI.SortColumnName = Convert.ToString(ViewState[Constants.SORTCOLUMNNAME]);
                }
                SaleBLL objSaleBLL = new SaleBLL();

                List<Sale> objData = new List<Sale>();
                objData = objSaleBLL.GetAllSaleDetailsDataByDate(objData, objPI, fromDate.Value, toDate.Value);

                gvGrid.DataSource = objData;
                gvGrid.ExportTemplate = "export_template_4Column.xlsx";
                gvGrid.ExportCaption = "";
                gvGrid.ExcelColumn = "";
                gvGrid.DataBind();

                if (null != objData)
                {
                    decimal sumBanAmount = (from od in objData
                                            select od.BankAmount).Sum();
                    spBanContact.InnerHtml = String.Format("{0:0,0.0}", sumBanAmount);

                    decimal sumCCAmount = (from od in objData
                                            select od.CCAmount).Sum();
                    spVisa.InnerHtml = String.Format("{0:0,0.0}", sumCCAmount);

                    decimal sumCashAmount = (from od in objData
                                            select od.Cash).Sum();
                    spCash.InnerHtml = String.Format("{0:0,0.0}", sumCashAmount);
                }
            }
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
            PopulateGrid();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    protected void gvGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            divMess.Visible = false;
            lblMsg.Text = "search";
            gvGrid.PageIndex = e.NewPageIndex;
            PopulateGrid();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

   

    #endregion

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        PopulateGrid();
    }
}