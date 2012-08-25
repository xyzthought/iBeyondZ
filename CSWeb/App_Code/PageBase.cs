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
        try
        {
            if (null != System.Web.HttpContext.Current.Session["UserData"])
            {
            }
            else
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }
        catch (Exception ex)
        {

        }
        
	}
}