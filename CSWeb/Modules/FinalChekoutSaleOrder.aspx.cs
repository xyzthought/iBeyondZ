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


public partial class Modules_FinalChekoutSaleOrder : PageBase
{
    string vstrLink = string.Empty;
    string param = string.Empty;

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
            if (null != Session["dtProductDetail"])
            {
                PopulateCustomer();
                dtProductDetail = (DataTable)Session["dtProductDetail"];
                string strQuery = Request.QueryString["q"];
                if (!string.IsNullOrEmpty(strQuery))
                {
                    Dictionary<String, String> objQuery = Common.PopulateDictionaryFromQueryString(strQuery);
                    SelectedMode = objQuery["MODE"].ToString();
                    SaleID = Convert.ToInt32(objQuery["ID"].ToString());
                    lblHeader.Text = "Final Checkout | Sale Order";
                    PopulateProductDetail();
                    if (!string.IsNullOrEmpty(Session["Discount"].ToString()))
                    {
                        txtDiscount.Text = Session["Discount"].ToString();
                    }
                    else
                    {
                        txtDiscount.Text = "0.00";
                    }
                    
                    CalculateTotalPrice();
                }
                else
                {
                    Response.Redirect("Sale.aspx", false);
                }
            }
        }
    }

    private void PopulateCustomer()
    {
        SaleBLL objSaleBLL = new SaleBLL();
        DataTable dtData = objSaleBLL.GetAllCustomerNameForAutoComplete();
        if (null != dtData && dtData.Rows.Count > 0)
        {
            hdnCustData.Value="";
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                hdnCustData.Value+=dtData.Rows[i]["CustomerID"].ToString()+"##"+dtData.Rows[i]["CustomerName"].ToString()+"##"+dtData.Rows[i]["Address"].ToString()+"@@";
            }
            if (hdnCustData.Value.Length > 0)
            {
                hdnCustData.Value = hdnCustData.Value.Substring(0, hdnCustData.Value.Length - 2);
            }
        }
    }

   

    #region Populate Sale Detail
    private void PopulateSaleDetail()
    {
        throw new NotImplementedException();
    } 
    #endregion
    protected void lnkAddMore_Click(object sender, EventArgs e)
    {
        PopulateProductDetail();
        CalculateTotalPrice();
    }

    private void CalculateTotalPrice()
    {
        decimal dblTotalPrice = 0;
        decimal dblDiscounted = 0;
        if (null != dtProductDetail)
        {
            for (int i = 0; i < dtProductDetail.Rows.Count; i++)
            {
                dblTotalPrice += Convert.ToDecimal(dtProductDetail.Rows[i]["Price"].ToString());
            }

            dblDiscounted = dblTotalPrice;
            if(!string.IsNullOrEmpty(txtDiscount.Text.Trim()))
            {
                dblDiscounted = dblTotalPrice - Convert.ToDecimal(txtDiscount.Text.Trim());
            }

            lblTotalAmount.Text = string.Format("{0:0.00}", dblTotalPrice);// String.Format("{0:C}", dblTotalPrice);
            lblTotalPay.Text = string.Format("{0:0.00}", dblDiscounted);// String.Format("{0:C}", dblDiscounted);
        }
    }

    

    private void PopulateProductDetail()
    {
        if (null != dtProductDetail)
        {
            gvGrid.DataSource = dtProductDetail;
            gvGrid.DataBind();
        }
    }

    #region GRID VIEW EVENTS
   

    
    protected void gvGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                /*param = Constants.MODE + "=" + Constants.MODE_EDIT + "&" + Constants.ID + "=" + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UserID"));
                param = Common.GenerateBASE64WithObfuscateApp(param);
                vstrLink = "AddEditUser?q=" + param;
                HtmlControl aEdit = (HtmlControl)e.Row.FindControl("aEdit");
                aEdit.Attributes.Add("on", vstrLink);*/


                LinkButton lnkDelete = new LinkButton();
                lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.OnClientClick = "return confirm('Product :" + BLL.BusinessObject.Constants.DeleteConf + "');";

            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    protected void gvGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit")
            {
                int intUserID = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["intUserID"] = intUserID;
            }

            if (e.CommandName == "Delete")
            {

                int intProductID = Convert.ToInt32(e.CommandArgument.ToString());
                for (int i = 0; i < dtProductDetail.Rows.Count; i++)
                {
                    if (dtProductDetail.Rows[i]["ProductID"].ToString() == intProductID.ToString())
                    {
                        dtProductDetail.Rows[i].Delete();
                        break;
                    }
                }
                dtProductDetail.AcceptChanges();
            }



        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

   

    protected void gvGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        PopulateProductDetail();
        CalculateTotalPrice();
    }

    protected void gvGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gvGrid.EditIndex = e.NewEditIndex;
    }

    #endregion
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Session["dtProductDetail"] = dtProductDetail;
        param = Constants.MODE + "=" + SelectedMode + "&" + Constants.ID + "=" + SaleID;
        param = Common.GenerateBASE64WithObfuscateApp(param);
        vstrLink = "AddEditSaleOrder.aspx?q=" + param;
        Response.Redirect(vstrLink, false);
    }
    protected void btnRegresh_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(Customerid.Value))
        {
            int CustID = Convert.ToInt32(Customerid.Value);
            Customer objCust = new Customer();
            CustomerBLL objCBLL = new CustomerBLL();
            objCust.CustomerID = CustID;
            List<Customer> objLCust = objCBLL.GetCustomerDetailByCustID(ref objCust);
            if (null != objLCust)
            {
                PopulateFormFields(objLCust);
            }
        }
    }

    private void PopulateFormFields(List<BLL.BusinessObject.Customer> objLCust)
    {
        txtAddress.Text = objLCust[0].Address;
        txtZIP.Text = objLCust[0].ZIP;
        txtCity.Text = objLCust[0].City;
        txtCountry.Text = objLCust[0].Country;
        txtEmailID.Text = objLCust[0].Email;
        txtPhone.Text = objLCust[0].TeleNumber;
    }
    protected void lnkFinalCheckout_Click(object sender, EventArgs e)
    {
        double dbAmounttoBePaid = Math.Round(Convert.ToDouble(lblTotalPay.Text.Trim()), 2);
        double dblCalculatedAmount = 0;
        double dblCredit = (string.IsNullOrEmpty(txtAmountPaid.Text.Trim()) ? 0 : Math.Round(Convert.ToDouble(txtAmountPaid.Text.Trim()), 2));
        double dblBank = (string.IsNullOrEmpty(txtBCash.Text.Trim()) ? 0 : Math.Round(Convert.ToDouble(txtBCash.Text.Trim()), 2));
        double dblCash = (string.IsNullOrEmpty(txtCash.Text.Trim()) ? 0 : Math.Round(Convert.ToDouble(txtCash.Text.Trim()), 2));

         dblCalculatedAmount = dblCredit + dblBank + dblCash;
        if (dblCalculatedAmount < dbAmounttoBePaid)
        {
            spErrorPay.InnerHtml = "Deficit Amount ( "+ Math.Round((dbAmounttoBePaid-dblCalculatedAmount),2).ToString()+" )";
        }
        else if (dblCalculatedAmount > dbAmounttoBePaid)
        {
            spErrorPay.InnerHtml = "Surplus Amount ( " + Math.Round((dblCalculatedAmount - dbAmounttoBePaid), 2).ToString() + " )";
        }
        else
        {
            spErrorPay.InnerHtml = "Ready for save data";
            SaveData();
        }

    }

    private void SaveData()
    {
        /*
         * SaleID
CustomerID
PaymentModeID
CCAmount
BankAmount
Cash
SaleDate
StandardRebate
Discount
SaleMadeBy
         */
        Sale objSale = new Sale();
        objSale.SaleID = SaleID;
        objSale.CustomerID =Convert.ToInt32(Customerid.Value.ToString());
           
    }
}