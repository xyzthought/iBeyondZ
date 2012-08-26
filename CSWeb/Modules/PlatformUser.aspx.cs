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

public partial class Modules_PlatformUser : PageBase
{

    PageInfo objPI = new PageInfo();
    string vstrLink = string.Empty;
    string param = string.Empty;
    public const string DEFAULTCOLUMNNAME = "CreatedOn";
    protected bool iFlag = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objPI.SortColumnName = DEFAULTCOLUMNNAME;
            objPI.SortDirection = Constants.DESC;
            ViewState[Constants.SORTCOLUMNNAME] = DEFAULTCOLUMNNAME;
            ViewState[Constants.SORTDERECTION] = Constants.DESC;
            PopulateGrid();
        }
    }

    private void PopulateGrid()
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
            UserBLL objUserBLL = new UserBLL();
          
            List<User> objData = new List<User>();
            objData = objUserBLL.GetAllUser(objData, objPI);

            gvGrid.DataSource = objData;
            gvGrid.ExportTemplate = "export_template_4Column.xlsx";
            gvGrid.ExportCaption = "";
            gvGrid.ExcelColumn = "";
            gvGrid.DataBind();

            if (objData != null)
            {
                if (!iFlag && lblMsg.Text == "")
                {
                    int mintMode = Common.GetQueryStringIntValue("Mode");
                    if (mintMode > 0)
                    {
                        if (mintMode == 1)
                        {
                            divMess.Attributes.Add("class", "success");
                            divMess.Visible = true;
                            lblMsg.Text = "-'" + objData[0].FirstName + "' " + Constants.Added;

                        }
                        else if (mintMode == 2)
                        {

                        }

                    }
                    else
                    {
                        divMess.Visible = false;
                        lblMsg.Text = "";
                    }
                }
                else
                {
                    if (lblMsg.Text == "")
                    {
                        divMess.Visible = false;
                        lblMsg.Text = "";
                    }
                }

            }
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

                param = Constants.MODE + "=" + Constants.MODE_EDIT + "&" + Constants.ID + "=" + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UserID"));
                param = Common.GenerateBASE64WithObfuscateApp(param);
                vstrLink = "AddEditUser?q=" + param;
                HtmlControl aEdit = (HtmlControl)e.Row.FindControl("aEdit");
                aEdit.Attributes.Add("href", vstrLink);


                Label lblinfo = (Label)e.Row.FindControl("lblinfo");
                lblinfo.Text = Common.cutTextToSpecifiedSize(lblinfo.Text.Trim(), 50);

                ImageButton imbtnStatus = new ImageButton();

                imbtnStatus = (ImageButton)e.Row.FindControl("imbtnStatus");
                if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive")))
                {
                    imbtnStatus.ImageUrl = "../../Images/icon_tick.png";
                    imbtnStatus.ToolTip = Resources.Resource.InactiveMsg;
                    imbtnStatus.OnClientClick = "return confirm(':" + BLL.BusinessObject.Constants.InActiveConf + " ');";
                    // imbtnStatus.Attributes.Add("OnClientClick", 
                }
                else
                {
                    imbtnStatus.ImageUrl = "../../Images/icon_cross.png";
                    imbtnStatus.ToolTip = Resources.Resource.ActiveMsg;
                    imbtnStatus.OnClientClick = "return confirm(':" + BLL.BusinessObject.Constants.ActiveConf + "');";
                    //imbtnStatus.OnClientClick = "return confirm('<b>Ad Exchange:</b>'" + CloudMob.BO.Constants.DeleteConf + ");";
                }


                LinkButton lnkDelete = new LinkButton();
                lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.OnClientClick = "return confirm(':" + BLL.BusinessObject.Constants.DeleteConf + "');";

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
            if (e.CommandName == "Delete")
            {
                //  divMess.Visible = false;
                Message vobjMsg = new Message();
                int dd = Convert.ToInt32(e.CommandArgument.ToString().Split('|')[0]);
                //    iFlag = true;
                vobjMsg = null;//.Delete();
                if (vobjMsg.ReturnValue > 0)
                {
                    divMess.Visible = true;
                    lblMsg.Text = e.CommandArgument.ToString().Split('|')[1].ToString() + "' " + Constants.Deleted;
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
            else if (e.CommandName == "Status")
            {
                Message vobjMsg = new Message();

            }

            PopulateGrid();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    protected void gvGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


   

    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {
        PopulateGrid();
    }
    protected void lnkAddNew_Click(object sender, EventArgs e)
    {

    }
}