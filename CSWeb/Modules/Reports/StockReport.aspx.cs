using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.Component;
using CSWeb.Utility;
using BLL.BusinessObject;
using System.Diagnostics;
using System.Web.UI.HtmlControls;

public partial class Modules_Reports_StockReport : System.Web.UI.Page
{
    PageInfo objPI = new PageInfo();
    public const string DEFAULTCOLUMNNAME = "ProductName";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {

                objPI.SortColumnName = DEFAULTCOLUMNNAME;
                objPI.SortDirection = Constants.ASC;
                ViewState[Constants.SORTCOLUMNNAME] = DEFAULTCOLUMNNAME;
                ViewState[Constants.SORTDERECTION] = Constants.DESC;
                toDate.Value = string.Format("{0:dd/MM/yyyy}", DateTime.Today);

                BindProduct();

            }
            catch (Exception ex)
            {
                SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }

        }
    }

    private void BindProduct()
    {
        try
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
            //List<Manufacturer> objData = new ManufacturerBLL().GetAll(objPI);

            BLL.Component.ProductBLL objprod = new BLL.Component.ProductBLL();
            gvGrid.DataSource = objprod.GetStockByDate(objPI, toDate.Value);
            gvGrid.DataBind();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    protected void gvGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lbl = (Label)e.Row.FindControl("lblQuantity");
            HtmlGenericControl divQty = (HtmlGenericControl)e.Row.FindControl("dvQtyDetails");
            Label lblStock = (Label)e.Row.FindControl("lblStock");
            HtmlGenericControl divStock = (HtmlGenericControl)e.Row.FindControl("dvStockDetails");

            e.Row.Cells[5].ToolTip = divQty.InnerHtml.Trim();
            e.Row.Cells[6].ToolTip = divStock.InnerHtml.Trim();
        }
    }
    protected void gvGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvGrid.PageIndex = e.NewPageIndex;
            BindProduct();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
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
            BindProduct();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {
        BindProduct();
    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        BindProduct();
    }
}