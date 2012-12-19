using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BusinessObject;
using BLL.Component;
using System.Web.UI.HtmlControls;

public partial class UserControls_Header : System.Web.UI.UserControl
{
    PageBase objPage = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objPage.CheckSession();
            LoadUserSpecificData();
            SetPagePrivilege();
            Page.Title = "S O F I S M";
        }
    }

    private void SetPagePrivilege()
    {
        objPage.CheckSession();
        string[] menuArray = new string[] { "mnuli1", "mnuli2", "mnuli3", "mnuli4", "mnuli5", "mnuli6", "mnuli7", "mnuli8", "mnuli9", "mnuli10"};
        string[] SellingDesk = new string[] { "mnuli6", "mnuli9" };  //SALE & CUSTOMER & MY ACCOUNT
        string[] StockManager = new string[] { "mnuli6", "mnuli4", "mnuli8", "mnuli5" }; //: MANUFACTURER, PRODUCT, BARCODE, PURCHASE
        User objUser = new BLL.BusinessObject.User();
        objUser = (User)Session["UserData"];
        if (objUser.UserTypeID == (int)Constants.UserType.SellingDesk)
        {
            for (int i = 0; i < menuArray.Length; i++)
            {
                for (int ii = 0; ii < SellingDesk.Length; ii++)
                {
                    if (SellingDesk[ii] == menuArray[i])
                    {
                        HtmlGenericControl ThisLI = (HtmlGenericControl)FindControl(SellingDesk[ii]);
                        ThisLI.ID = SellingDesk[ii];
                        ThisLI.Style.Add("display", "");
                    }
                }
            }
        }

        else if (objUser.UserTypeID == (int)Constants.UserType.StockManager)
        {
            for (int i = 0; i < menuArray.Length; i++)
            {
                for (int ii = 0; ii < StockManager.Length; ii++)
                {
                    if (StockManager[ii] == menuArray[i])
                    {
                        HtmlGenericControl ThisLI = (HtmlGenericControl)FindControl(StockManager[ii]);
                        ThisLI.ID = StockManager[ii];
                        ThisLI.Style.Add("display", "");
                    }
                }
            }
        }

        else if (objUser.UserTypeID == (int)Constants.UserType.Admin)
        {
            for (int i = 0; i < menuArray.Length; i++)
            {
                HtmlGenericControl ThisLI = (HtmlGenericControl)FindControl(menuArray[i]);
                ThisLI.ID = menuArray[i];
                ThisLI.Style.Add("display", "");
            }
        }

    }

    private void LoadUserSpecificData()
    {
        try
        {
            objPage.CheckSession();
            User objUser = new BLL.BusinessObject.User();
            objUser = (User)Session["UserData"];
            lblUserName.InnerHtml = objUser.FirstName;
            spnUserType.InnerHtml = " , " + objUser.UserType + " - Last login : " + string.Format("{0:dd-MMM @ HH:mm}", objUser.LastLoggedIn) + " ";
        }
        catch (Exception ex)
        {

        }

    }
   
    

    protected void lnkBtnSaveDS_Click(object sender, EventArgs e)
    {
        User objUser = new User();
        objUser.UserID = ((User)Session["UserData"]).UserID;
        objUser.LoginPassword = txtOldPassword.Text.Trim();
        Message objMessage = new UserBLL().ChangePassword(objUser, txtNewPassword.Text.Trim());
        if (objMessage.ReturnValue > 0)
        {
            lblError.InnerHtml = "Password changed successfully";
        }
        else
        {
            lblError.InnerHtml = objMessage.ReturnMessage;

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowError", "ShowModalDiv('divChangePassword','dvInnerWindow',0)", true);
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("~/Default.aspx", false);
    }

    protected void lnkMyAccount_Click(object sender, EventArgs e)
    {
        PopulateMyAccountData();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMyAccount", "ShowModalDiv('divMyAccount','Div6',0)", true);
    }

    private void PopulateMyAccountData()
    {
        txtFirstName.Text = ((User)Session["UserData"]).FirstName;
        txtLastName.Text = ((User)Session["UserData"]).LastName;
        txtEmailID.Text = ((User)Session["UserData"]).CommunicationEmailID;
    }

    protected void lnkSaveMyAccount_Click(object sender, EventArgs e)
    {
        User objUser = new User();
        objUser.UserID = ((User)Session["UserData"]).UserID;
        objUser.FirstName = txtFirstName.Text.Trim();
        objUser.LastName = txtLastName.Text.Trim();
        objUser.CommunicationEmailID = txtEmailID.Text.Trim();
        objUser = new UserBLL().ChangeAccountInformation(ref objUser);
        if (!string.IsNullOrEmpty(objUser.FirstName))
        {
            MyAccountError.InnerHtml = "Data updated successfully";
            Session["UserData"] = objUser;
        }
        else
        {
            MyAccountError.InnerHtml = "Data not updated. Email-ID already in use";
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowError", "ShowModalDiv('divMyAccount','Div6',0)", true);
    }
}