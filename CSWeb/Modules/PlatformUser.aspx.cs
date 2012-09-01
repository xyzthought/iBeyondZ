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
            PopulateUserType();
            PopulateGrid();
        }
    }

   

    #region Populate User Type
    private void PopulateUserType()
    {
        UserTypeBLL objUTBLL = new UserTypeBLL();
        List<UserTypeBO> objUTBO = objUTBLL.GetAllUserType();
        Common.BindControl(ddlUserType, objUTBO, "UserType", "UserTypeID", Constants.ControlType.DropDownList, true);
    } 
    #endregion

    #region Populate Grid
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
            updPanel.Update();
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
                lnkDelete.OnClientClick = "return confirm(':" + BLL.BusinessObject.Constants.DeleteConf + "');";

                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UserID")) == ((User)Session["UserData"]).UserID)
                    lnkDelete.Visible = false;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "CallLoader();MyModalClose();", true);

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
                LoadData(intUserID);
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
                    lblMsg.Text = e.CommandArgument.ToString() + "' " + Constants.Deleted;
                    divMess.Attributes.Add("class", "Deleted");
                    lblMsg.Style.Add("color", "Black");
                    PopulateGrid();
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


    

    private void LoadData(int vintUserID)
    {
        try
        {
            User objUser = new User();
            UserBLL objUBLL = new UserBLL();
            objUser.UserID = vintUserID;
            List<User> objList = objUBLL.GetPlatformUserByUserID(ref objUser);

            if (null != objList)
            {
                ddlUserType.SelectedValue = objList[0].UserTypeID.ToString();
                txtFirstName.Text = objList[0].FirstName;
                txtLastName.Text = objList[0].LastName;
                txtLoginID.Text = objList[0].LoginID;
                txtPassword.Text = objList[0].LoginPassword;
                txtEmailID.Text = objList[0].CommunicationEmailID;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AddEditUser", "ShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);
               
            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
       
    }

    protected void gvGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gvGrid.EditIndex = e.NewEditIndex;
    }
    
    #endregion

   

    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {
        PopulateGrid();
    }

    protected void lnkBtnSaveDS_Click(object sender, EventArgs e)
    {
        if (ddlUserType.SelectedIndex > 0)
        {
            if (null != ViewState["intUserID"] && Convert.ToInt32(ViewState["intUserID"].ToString())>0)
            {
                SaveData(Constants.MODE_EDIT, Convert.ToInt32(ViewState["intUserID"].ToString()));
            }
            else
            {
                SaveData(Constants.MODE_ADD, 0);
            }
        }
        else
        {
            lblMsg.Text = Resources.Resource.MandatoryFieldMissing;
        }
    }


    #region Save Data
    private void SaveData(string vstrMode,int vintUserID )
    {
        User objUser = new User();
        UserBLL objUBLL=new UserBLL();
        objUser.UserID=vintUserID;
        objUser.UserTypeID =Convert.ToInt32(ddlUserType.SelectedItem.Value.ToString());
        objUser.FirstName = txtFirstName.Text.Trim();
        objUser.LastName = txtLastName.Text.Trim();
        objUser.LoginID = txtLoginID.Text.Trim();
        objUser.LoginPassword = txtPassword.Text.Trim();
        objUser.CommunicationEmailID = txtEmailID.Text.Trim();

        Message objMsg = objUBLL.InsertUpdatePlatformUser(objUser);

        lblError.InnerHtml = objMsg.ReturnMessage;
        if (objMsg.ReturnValue > 0)
        {
            lblMsg.Text = objMsg.ReturnMessage;
            ViewState["intUserID"] = null;
            PopulateGrid();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "AddEditUser", "ShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);
        }
    } 
    #endregion
    
}