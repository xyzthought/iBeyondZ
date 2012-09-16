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


public partial class Modules_Sale : PageBase
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
            ViewState[Constants.SORTDERECTION] = Constants.DESC;

            datepicker.Value = string.Format("{0:MM/dd/yyyy}", DateTime.Today);
            PopulateGrid();
        }
    }

    #region Populate Grid
    private void PopulateGrid()
    {
        try
        {
            if (!string.IsNullOrEmpty(datepicker.Value))
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
                objData = objSaleBLL.GetAllSaleDataByDate(objData, objPI, datepicker.Value);

                gvGrid.DataSource = objData;
                gvGrid.ExportTemplate = "export_template_4Column.xlsx";
                gvGrid.ExportCaption = "";
                gvGrid.ExcelColumn = "";
                gvGrid.DataBind();
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

    protected void gvGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                /*param = Constants.MODE + "=" + Constants.MODE_EDIT + "&" + Constants.ID + "=" + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UserID"));
                param = Common.GenerateBASE64WithObfuscateApp(param);
                vstrLink = "AddEditUser?q=" + param;
                HtmlControl aEdit = (HtmlControl)e.Row.FindControl("aEdit");
                aEdit.Attributes.Add("on", vstrLink);*/


                LinkButton lnkDelete = new LinkButton();
                lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.OnClientClick = "return confirm('User :" + BLL.BusinessObject.Constants.DeleteConf + "');";

                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UserID")) == ((User)Session["UserData"]).UserID)
                    lnkDelete.Visible = false;


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
                int intUserID = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["intUserID"] = intUserID;                
            }

            if (e.CommandName == "Delete")
            {

                Message vobjMsg = new Message();
                int intUserID = Convert.ToInt32(e.CommandArgument.ToString());
                User objUser = new User();
                UserBLL objUBLL = new UserBLL();
                objUser.UserID = intUserID;
                vobjMsg = objUBLL.DeletePlatformUser(objUser);

                if (vobjMsg.ReturnValue > 0)
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
                    lblMsg.Text = vobjMsg.ReturnMessage;
                }
            }



        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    protected void gvGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PopulateGrid();

    }

    protected void gvGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gvGrid.EditIndex = e.NewEditIndex;
    }

    #endregion
   
    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        PopulateGrid();
    }
}