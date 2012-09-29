using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Threading;

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

    protected override void InitializeCulture()
    {
        var culture = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
        culture.NumberFormat.CurrencySymbol = string.Empty;
        culture.NumberFormat.NumberDecimalDigits = 2;
        culture.NumberFormat.NumberDecimalSeparator = ".";
        culture.NumberFormat.NumberGroupSeparator = ",";
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        base.InitializeCulture();
    }

}