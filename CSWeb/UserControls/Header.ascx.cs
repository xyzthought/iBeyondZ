using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BusinessObject;

public partial class UserControls_Header : System.Web.UI.UserControl
{
    PageBase objPage = new PageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        objPage.CheckSession();
        LoadUserSpecificData();
    }

    private void LoadUserSpecificData()
    {
        try
        {
            User objUser = new BLL.BusinessObject.User();
            objUser = (User)Session["UserData"];
            lblUserName.InnerHtml = objUser.FirstName;
            spnUserType.InnerHtml = " , " + objUser.UserType + " - Last login : "+string.Format("{0:dd-MMM @ HH:mm}",objUser.LastLoggedIn)+" ";
        }
        catch (Exception ex)
        {
           
        }
        
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("~/Default.aspx",false);
    }
}