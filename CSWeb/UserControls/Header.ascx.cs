using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BusinessObject;
using BLL.Component;

public partial class UserControls_Header : System.Web.UI.UserControl
{
    PageBase objPage = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        objPage.CheckSession();
        LoadUserSpecificData();
        Page.Title = "S O F I S M";
    }

    private void LoadUserSpecificData()
    {
        try
        {
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