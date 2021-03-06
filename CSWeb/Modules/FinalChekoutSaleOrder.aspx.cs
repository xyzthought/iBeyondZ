﻿using System;
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
using System.Activities.Statements;


public partial class Modules_FinalChekoutSaleOrder : PageBase
{
    protected string serversideEvent = string.Empty;
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

    protected string SaleDate
    {
        get
        {
            if (ViewState["SaleDate"] != null)
                return Convert.ToString(ViewState["SaleDate"]);
            else
                return string.Empty;
        }
        set
        {
            ViewState["SaleDate"] = value;
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
        try
        {
            if (!Page.IsPostBack)
            {
                serversideEvent = Page.ClientScript.GetPostBackEventReference(btnRegresh, string.Empty);
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
                        SaleDate = objQuery["sd"].ToString();
                        if (SelectedMode == Constants.MODE_EDIT)
                        {
                            PopulateFinalSaleDetail(SaleID);
                        }
                        lblHeader.Text = "Final Checkout | Sale Order";
                        PopulateProductDetail();
                        if (!string.IsNullOrEmpty(Session["Discount"].ToString()))
                        {
                            txtDiscount.Text = string.Format("{0:0.00}", Convert.ToDecimal(Session["Discount"].ToString()));
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
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    private void PopulateFinalSaleDetail(int SaleID)
    {
        Sale objSale = new Sale();
        objSale.SaleID = SaleID;
        SaleBLL objSBLL = new SaleBLL();
        objSale = objSBLL.GetFinalCheckOutDeatils(ref objSale);

        Customer.Text = objSale.CFirstName;
        txtAddress.Text = objSale.Address;
        txtZIP.Text = objSale.ZIP;
        txtCity.Text = objSale.City;
        txtCountry.Text = objSale.Country;
        txtPhone.Text = objSale.TeleNumber;
        txtEmailID.Text = objSale.Email;
        txtAmountPaid.Text = objSale.CCAmount.ToString();
        txtBCash.Text = objSale.BankAmount.ToString();
        txtCash.Text = objSale.Cash.ToString();
        txtDiscount.Text = txtDiscount.Text = string.Format("{0:0.00}", objSale.Discount);
        txtSaleNote.Text = objSale.SaleNote;

        ddlFdiscount.SelectedValue = objSale.FinalDiscountType.ToString();
        txtFDiscount.Text = objSale.FinalDiscount.ToString();

        txtFinalAmount.Text = objSale.FinalPayableAmount.ToString();
    }

    private void PopulateCustomer()
    {
        try
        {
            SaleBLL objSaleBLL = new SaleBLL();
            DataTable dtData = objSaleBLL.GetAllCustomerNameForAutoComplete();
            if (null != dtData && dtData.Rows.Count > 0)
            {
                hdnCustData.Value = "";
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    hdnCustData.Value += dtData.Rows[i]["CustomerID"].ToString() + "##" + dtData.Rows[i]["CustomerName"].ToString() + "##" + dtData.Rows[i]["Address"].ToString() + "@@";
                }
                if (hdnCustData.Value.Length > 0)
                {
                    hdnCustData.Value = hdnCustData.Value.Substring(0, hdnCustData.Value.Length - 2);
                }
            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
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
        try
        {
            decimal dblTotalPrice = 0;
            decimal dblDiscounted = 0;
            if (null != dtProductDetail)
            {
                for (int i = 0; i < dtProductDetail.Rows.Count; i++)
                {
                    decimal UQ = (Convert.ToDecimal(dtProductDetail.Rows[i]["Unit"].ToString()) * Convert.ToDecimal(dtProductDetail.Rows[i]["Quantity"].ToString()));
                    UQ = UQ + (UQ * +Convert.ToDecimal(dtProductDetail.Rows[i]["Tax"].ToString()) / 100);
                    dblTotalPrice += UQ;
                }
                if (!string.IsNullOrEmpty(txtDiscount.Text.Trim()))
                {
                    dblDiscounted = dblTotalPrice - Convert.ToDecimal(txtDiscount.Text.Trim());
                }

                lblTotalAmount.Text = string.Format("{0:0.00}", dblTotalPrice);// String.Format("{0:C}", dblTotalPrice);
                lblTotalPay.Text = string.Format("{0:0.00}", dblDiscounted);// String.Format("{0:C}", dblDiscounted);
                lblFinalAmount.Text = string.Format("{0:0.00}", dblDiscounted);// String.Format("{0:C}", dblDiscounted);


                decimal dblAmountAfterDiscount = Convert.ToDecimal(lblTotalPay.Text);
                if (Convert.ToDecimal(txtFDiscount.Text) > 0)
                {
                    if (ddlFdiscount.SelectedValue == "%")
                    {
                        dblAmountAfterDiscount = dblAmountAfterDiscount - (dblAmountAfterDiscount * Convert.ToDecimal(txtFDiscount.Text) / 100);
                    }
                    else
                    {
                        dblAmountAfterDiscount = dblAmountAfterDiscount - Convert.ToDecimal(txtFDiscount.Text);
                    }
                }
                lblFinalAmount.Text = string.Format("{0:0.00}", dblAmountAfterDiscount);
                txtFinalAmount.Text = lblFinalAmount.Text;
            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
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
        param += "&tsd=" + SaleDate;
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateCustomer", "Populate()", true);
    }

    private void PopulateFormFields(List<BLL.BusinessObject.Customer> objLCust)
    {
        txtAddress.Text = objLCust[0].Address;
        txtZIP.Text = objLCust[0].ZIP;
        txtCity.Text = objLCust[0].City;
        txtCountry.Text = objLCust[0].Country;
        txtEmailID.Text = objLCust[0].Email;
        txtPhone.Text = objLCust[0].TeleNumber;
        txtSaleNote.Text = objLCust[0].Notes;
    }
    protected void lnkFinalCheckout_Click(object sender, EventArgs e)
    {
        try
        {
            double dbAmounttoBePaid = Math.Round(Convert.ToDouble(txtFinalAmount.Text.Trim()), 2);
            double dblCalculatedAmount = 0;
            double dblCredit = (string.IsNullOrEmpty(txtAmountPaid.Text.Trim()) ? 0 : Math.Round(Convert.ToDouble(txtAmountPaid.Text.Trim()), 2));
            double dblBank = (string.IsNullOrEmpty(txtBCash.Text.Trim()) ? 0 : Math.Round(Convert.ToDouble(txtBCash.Text.Trim()), 2));
            double dblCash = (string.IsNullOrEmpty(txtCash.Text.Trim()) ? 0 : Math.Round(Convert.ToDouble(txtCash.Text.Trim()), 2));

            dblCalculatedAmount = dblCredit + dblBank + dblCash;
            if (dblCalculatedAmount < dbAmounttoBePaid)
            {
                spErrorPay.InnerHtml = "Deficit Amount ( " + Math.Round((dbAmounttoBePaid - dblCalculatedAmount), 2).ToString() + " )";
            }
            else if (dblCalculatedAmount > dbAmounttoBePaid)
            {
                spErrorPay.InnerHtml = "Surplus Amount ( " + Math.Round((dblCalculatedAmount - dbAmounttoBePaid), 2).ToString() + " )";
            }
            else
            {
                spErrorPay.InnerHtml = "Ready for save data";
                SaveData();
                Session["dtProductDetail"] = null;
                Session["Discount"] = null;
                Response.Redirect("AddEditSaleOrder.aspx", false);

            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }

    }

    protected string ShowValue(string strUnit, string Qnty, string Tax)
    {
        string strCalTax = string.Empty;
        strCalTax = string.Format("{0:0.00}", (Convert.ToDecimal(strUnit) * Convert.ToDecimal(Qnty)) * Convert.ToDecimal(Tax) / 100);

        return strCalTax;
    }

    private void SaveData()
    {

        try
        {

            Sale objSale = new Sale();
            SaleBLL objSBLL = new SaleBLL();
            objSale.SaleID = SaleID;
            objSale.CustomerID = (string.IsNullOrEmpty(Customerid.Value.Trim()) ? 0 : Convert.ToInt32(Customerid.Value.Trim()));
            objSale.CFirstName = Customer.Text.Trim();
            objSale.Address = txtAddress.Text.Trim();
            objSale.City = txtCity.Text.Trim();
            objSale.ZIP = txtZIP.Text.Trim();
            objSale.Country = txtCountry.Text.Trim();
            objSale.Email = txtEmailID.Text.Trim();
            objSale.SaleNote = txtSaleNote.Text.Trim();
            objSale.TeleNumber = txtPhone.Text.Trim();
            objSale.CCAmount = (string.IsNullOrEmpty(txtAmountPaid.Text.Trim()) ? 0 : Convert.ToDecimal(txtAmountPaid.Text.Trim()));
            objSale.BankAmount = (string.IsNullOrEmpty(txtBCash.Text.Trim()) ? 0 : Convert.ToDecimal(txtBCash.Text.Trim()));
            objSale.Cash = (string.IsNullOrEmpty(txtCash.Text.Trim()) ? 0 : Convert.ToDecimal(txtCash.Text.Trim()));
            objSale.Discount = (string.IsNullOrEmpty(txtDiscount.Text.Trim()) ? 0 : Convert.ToDecimal(txtDiscount.Text.Trim()));
            objSale.FinalDiscountType = ddlFdiscount.SelectedValue.ToString();
            objSale.FinalDiscount = Convert.ToDecimal(txtFDiscount.Text.Trim());
            objSale.FinalPayableAmount = Convert.ToDecimal(txtFinalAmount.Text.Trim());
            objSale.SaleMadeBy = ((User)Session["UserData"]).UserID;
            objSale.ThisSaleDate = SaleDate;

            Message objMessage = objSBLL.InsertUpdateSaleMaster(objSale);
            if (objMessage.ReturnValue > 0)
            {
                objSale.SaleID = objMessage.ReturnValue;
                objMessage = objSBLL.DeleteExistingSalesDetails(objSale);
                for (int i = 0; i < dtProductDetail.Rows.Count; i++)
                {

                    objSale.ProductID = Convert.ToInt32(dtProductDetail.Rows[i]["ProductID"].ToString());
                    objSale.SizeID = Convert.ToInt32(dtProductDetail.Rows[i]["SizeID"].ToString());
                    objSale.Quantity = Convert.ToDecimal(dtProductDetail.Rows[i]["Quantity"].ToString());
                    objSale.Discount = Convert.ToDecimal(dtProductDetail.Rows[i]["PDiscount"].ToString());
                    objSale.DiscountType = dtProductDetail.Rows[i]["DiscountType"].ToString();
                    objSale.Price = Convert.ToDecimal(dtProductDetail.Rows[i]["Unit"].ToString());

                    objMessage = objSBLL.InsertUpdateSaleDetail(objSale);
                }

            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
}