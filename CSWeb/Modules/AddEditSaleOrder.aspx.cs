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
using System.Data;


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

    protected DataTable dtProductDetail
    {
        get
        {
            if (ViewState["dtProductDetail"] != null)
                return (DataTable)ViewState["dtProductDetail"];
            else
                return null;
        }
        set
        {
            ViewState["dtProductDetail"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            dtProductDetail=CreateTableStructure();
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

    private DataTable CreateTableStructure()
    {
        DataTable dtData = new DataTable();
       
        dtData.Columns.Add("ProductID", typeof(int));
        dtData.Columns.Add("BarCode", typeof(string));
        dtData.Columns.Add("ProductName", typeof(string));
        dtData.Columns.Add("SizeName", typeof(string));
        dtData.Columns.Add("Quantity", typeof(decimal));
        dtData.Columns.Add("Unit", typeof(decimal));
        dtData.Columns.Add("Price", typeof(decimal));
        dtData.PrimaryKey = new DataColumn[] { dtData.Columns["ProductID"] };
        return dtData;
    }

   

    #region Populate Sale Detail
    private void PopulateSaleDetail()
    {
        throw new NotImplementedException();
    } 
    #endregion
    protected void lnkAddMore_Click(object sender, EventArgs e)
    {
        AddDataToDataTable();
        PopulateProductDetail();
        CalculateTotalPrice();
    }

    private void CalculateTotalPrice()
    {
        decimal dblTotalPrice = 0;
        if (null != dtProductDetail && dtProductDetail.Rows.Count > 0)
        {
            for (int i = 0; i < dtProductDetail.Rows.Count; i++)
            {
                dblTotalPrice += Convert.ToDecimal(dtProductDetail.Rows[i]["Price"].ToString());
            }

            lblTotalAmount.Text = String.Format("{0:C}", dblTotalPrice);
            lblTotalPay.Text = String.Format("{0:C}", dblTotalPrice);
        }
    }

    private void AddDataToDataTable()
    {
        try
        {
            SaleBLL objSBLL = new SaleBLL();
            List<Sale> objSale = objSBLL.GetProductDetailByBarCode(txtProductBarCode.Text.Trim());
            if (null != objSale && objSale.Count>0)
            {
                DataRow dtRow = dtProductDetail.NewRow();
                dtRow["ProductID"] = objSale[0].ProductID;
                dtRow["BarCode"] = objSale[0].BarCode;
                dtRow["ProductName"] = objSale[0].ProductName;
                dtRow["SizeName"] = objSale[0].SizeName;
                dtRow["Quantity"] =Convert.ToDecimal(txtQuantity.Text.Trim());
                dtRow["Unit"] = objSale[0].Price;
                dtRow["Price"] = objSale[0].Price * Convert.ToDecimal(txtQuantity.Text.Trim());
                dtProductDetail.Rows.Add(dtRow);
            }
            else
            {
                lblError.InnerHtml = "Product Bar Code not found";
            }
        }
        catch (ConstraintException ex)
        {
            lblError.InnerHtml = "Product all ready added";
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    private void PopulateProductDetail()
    {
        if (null != dtProductDetail && dtProductDetail.Rows.Count > 0)
        {
            gvGrid.DataSource = dtProductDetail;
            gvGrid.DataBind();
        }
    }
}