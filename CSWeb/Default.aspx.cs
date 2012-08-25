using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSWeb.Utility;
using BLL.BusinessObject;
using BLL.Component;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClearPage();

    }

    private void ClearPage()
    {
        errMsg.InnerHtml = "";
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (AuthenticateUser())
        {
            Response.Redirect("modules/LandingPage.aspx", false);
        }
        else
        {
            errMsg.InnerHtml = "Invalid login credentials";
        }
    }

    private bool AuthenticateUser()
    {
        bool blnIsAuthenticated = true;
        User objUser = new User();
        UserBLL objUserBll = new UserBLL();
        try
        {
            string strLogin = txtUserName.Text.Trim();
            string strPassword = txtPassword.Text.Trim();
            if (!(string.IsNullOrEmpty(strLogin) && string.IsNullOrEmpty(strPassword)))
            {
                objUser.LoginID = strLogin;
                objUser.LoginPassword = strPassword;
                objUserBll.AuthenticationValidation(ref objUser);
                if (string.IsNullOrEmpty(objUser.FirstName))
                {
                    blnIsAuthenticated = false;
                }
                else
                {
                    StoreValuesToSession(objUser);                
                }
            }
            else
            {
                errMsg.InnerHtml = "Invalid login credentials";
            }

        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > "+ (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name,ex.ToString());
        }


        return blnIsAuthenticated;
    }

    private void StoreValuesToSession(User objUser)
    {
        Session["UserData"] = objUser;
    }
}