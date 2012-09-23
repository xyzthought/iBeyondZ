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

public partial class Modules_Product : System.Web.UI.Page
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
                objPI.SortDirection = Constants.DESC;
                ViewState[Constants.SORTCOLUMNNAME] = DEFAULTCOLUMNNAME;
                ViewState[Constants.SORTDERECTION] = Constants.DESC;

                BindGrid();
            }
            catch (Exception ex)
            {
                SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }

        }
    }

    private void BindGrid()
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
            gvGrid.DataSource = objprod.GetAllProducts(objPI);
            gvGrid.DataBind();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
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
            BindGrid();
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
            BindGrid();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
    protected void gvGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton lnkDelete = new LinkButton();
                lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.OnClientClick = "return confirm('Product :" + BLL.BusinessObject.Constants.DeleteConf + "');";
                //OnClientClick="return ClearFormFields();ShowModalDiv('ModalWindow1','dvInnerWindow',0)"

            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
    protected void gvGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit")
            {
                int intProductID = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["intProductID"] = intProductID;
                //LoadData(intProductID);
                Response.Redirect("AddEditProduct.aspx?ProductID=" + intProductID);
            }

            if (e.CommandName == "Delete")
            {
                int intProductID = Convert.ToInt32(e.CommandArgument.ToString());
                bool blnReturn = new ProductBLL().DeleteProduct(intProductID);

                if (blnReturn)
                {
                    divMess.Visible = true;
                    lblMsg.Text = Constants.Deleted;
                    divMess.Attributes.Add("class", "Deleted");
                    lblMsg.Style.Add("color", "Black");
                }
                else
                {
                    divMess.Visible = true;
                    lblMsg.Style.Add("color", "Red");
                    divMess.Attributes.Add("class", "error");
                    lblMsg.Text = "Delete failed";
                }
            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    protected void gvGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void lnkAddNew2_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditProduct.aspx");
    }
}