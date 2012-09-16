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


public partial class Modules_AddEditSaleOrder : PageBase
{

    #region Protected Properties
    protected string SelectedMode
    {
        get
        {
            if (ViewState["SelectedMode"] != null)
                return Convert.ToString(ViewState["SelectedMode"]);
            else
                return string.Empty;
        }
        set
        {
            ViewState["SelectedMode"] = value;
        }
    }
    protected Int32 SaleID
    {
        get
        {
            if (ViewState["SaleID"] != null)
                return Convert.ToInt32(ViewState["SaleID"]);
            else
                return 0;
        }
        set
        {
            ViewState["SaleID"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string strQuery = Request.QueryString["q"];
            if(!string.IsNullOrEmpty(strQuery))
            {
                Dictionary<String, String> objQuery = Common.PopulateDictionaryFromQueryString(strQuery);
                SelectedMode = objQuery["MODE"].ToString();
                SaleID = Convert.ToInt32(objQuery["ID"].ToString());
                if (Constants.MODE == Constants.MODE_EDIT)
                {
                    PopulateSaleDetail();
                    lblHeader.Text = "EDIT | Sale Order";
                }
                else
                {
                    lblHeader.Text = "ADD | Sale Order";
                }
            }
            else
            {
                Response.Redirect("Sale.aspx",false);
            }
        }
    }

    #region Populate Sale Detail
    private void PopulateSaleDetail()
    {
        throw new NotImplementedException();
    } 
    #endregion
}