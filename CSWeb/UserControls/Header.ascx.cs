using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BusinessObject;

public partial class UserControls_Header : System.Web.UI.UserControl
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadUserSpecificData();
    }

    private void LoadUserSpecificData()
    {
        User objUser = new BLL.BusinessObject.User();
        objUser = (User)Session["UserData"];
        lblUserName.InnerHtml = objUser.FirstName;
        spnUserType.InnerHtml = "Super Admin";
    }
}