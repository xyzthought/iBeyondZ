using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PageBase
/// </summary>
public class PageBase : System.Web.UI.Page
{
	public PageBase()
	{
        CheckSession();
	}

    public void CheckSession()
    {
        try
        {
            if (null != System.Web.HttpContext.Current.Session["UserData"])
            {
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Default.aspx", false);
            }
        }
        catch (Exception ex)
        {

        }

    }
}