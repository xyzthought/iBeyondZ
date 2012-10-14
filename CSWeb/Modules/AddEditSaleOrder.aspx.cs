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
        try
        {
            if (!Page.IsPostBack)
            {
                PopulateAutoCompleteProductInformation();
                if (null == Session["dtProductDetail"])
                {
                    dtProductDetail = CreateTableStructure();
                }
                else
                {
                    dtProductDetail = (DataTable)Session["dtProductDetail"];
                    PopulateProductDetail();
                    txtDiscount.Text = Session["Discount"].ToString();
                    CalculateTotalPrice();
                }
                string strQuery = Request.QueryString["q"];
                if (!string.IsNullOrEmpty(strQuery))
                {
                    Dictionary<String, String> objQuery = Common.PopulateDictionaryFromQueryString(strQuery);
                    SelectedMode = objQuery["MODE"].ToString();
                    SaleID = Convert.ToInt32(objQuery["ID"].ToString());

                    if (SelectedMode == Constants.MODE_EDIT)
                    {
                        Session["dtProductDetail"] = null;
                        dtProductDetail.Clear();
                        PopulateSaleDetail(SaleID);
                        lblHeader.Text = "EDIT | Sale Order";
                    }
                    else
                    {
                        lblHeader.Text = "ADD | Sale Order";
                    }
                }
                else
                {
                    Response.Redirect("Sale.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    private void PopulateAutoCompleteProductInformation()
    {
        SaleBLL objSaleBLL = new SaleBLL();
        DataTable dtData = objSaleBLL.PopulateAutoCompleteProductInformation();
        if (null != dtData && dtData.Rows.Count > 0)
        {
            hdnByBarCode.Value = "";
            hdnByProductName.Value = "";
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                hdnByBarCode.Value += dtData.Rows[i]["ProductID"].ToString() + "##" + dtData.Rows[i]["BarCode"].ToString() + "##" + dtData.Rows[i]["ProductName"].ToString() + "@@";
                hdnByProductName.Value += dtData.Rows[i]["ProductID"].ToString() + "##" + dtData.Rows[i]["ProductName"].ToString() + "##" + dtData.Rows[i]["BarCode"].ToString() + "@@";
            }
            if (hdnByBarCode.Value.Length > 0)
            {
                hdnByBarCode.Value = hdnByBarCode.Value.Substring(0, hdnByBarCode.Value.Length - 2);
            }

            if (hdnByProductName.Value.Length > 0)
            {
                hdnByProductName.Value = hdnByProductName.Value.Substring(0, hdnByProductName.Value.Length - 2);
            }
        }
    }

    private DataTable CreateTableStructure()
    {
        DataTable dtData = new DataTable();

        dtData.Columns.Add("ProductID", typeof(int));
        dtData.Columns.Add("BarCode", typeof(string));
        dtData.Columns.Add("ProductName", typeof(string));
        dtData.Columns.Add("SizeID", typeof(int));
        dtData.Columns.Add("SizeName", typeof(string));
        dtData.Columns.Add("Quantity", typeof(decimal));
        dtData.Columns.Add("Unit", typeof(decimal));
        dtData.Columns.Add("PDiscount", typeof(decimal));
        dtData.Columns.Add("Tax", typeof(decimal));
        dtData.Columns.Add("Price", typeof(decimal));
        dtData.PrimaryKey = new DataColumn[] { dtData.Columns["ProductID"] };
        return dtData;
    }



    #region Populate Sale Detail
    private void PopulateSaleDetail(int intSaleID)
    {
        Sale objSale = new Sale();
        objSale.SaleID = intSaleID;
        SaleBLL objSBLL = new SaleBLL();
        List<Sale> objLSale = new List<Sale>();
        objLSale = objSBLL.GetSaleDetailBySaleID(objSale);
        if (null != objLSale)
        {
            for (int i = 0; i < objLSale.Count; i++)
            {
                InsertDataintoTempDataTable(objLSale[i], false);
            }
        }
        PopulateProductDetail();
        CalculateTotalPrice();
        txtProductBarCode.Text = "";
        txtQuantity.Text = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('1')", true);

    }
    #endregion
    protected void lnkAddMore_Click(object sender, EventArgs e)
    {

        PopulateInformation();
    }

    private void PopulateInformation()
    {
        AddDataToDataTable();
        PopulateProductDetail();
        CalculateTotalPrice();
        txtProductBarCode.Text = "";
        txtQuantity.Text = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('1')", true);
    }

    private void CalculateTotalPrice()
    {
        decimal dblTotalPrice = 0;
        decimal dblDiscounted = 0;
        decimal dblTotalDiscount = 0;
        if (null != dtProductDetail)
        {
            for (int i = 0; i < dtProductDetail.Rows.Count; i++)
            {
                decimal decDiscount = Convert.ToDecimal(dtProductDetail.Rows[i]["Unit"].ToString()) * Convert.ToDecimal(dtProductDetail.Rows[i]["Quantity"].ToString());
                decDiscount = (decDiscount * (Convert.ToDecimal(dtProductDetail.Rows[i]["PDiscount"].ToString()) / 100));
                dblTotalDiscount += decDiscount;

                dblTotalPrice += Convert.ToDecimal(dtProductDetail.Rows[i]["Price"].ToString());
            }

            dblDiscounted = dblTotalPrice;
            if (!string.IsNullOrEmpty(txtDiscount.Text.Trim()))
            {
                dblDiscounted = dblTotalPrice - Convert.ToDecimal(txtDiscount.Text.Trim());
            }

            lblTotalAmount.Text = Math.Round(dblTotalPrice, 2).ToString();// String.Format("{0:C}", dblTotalPrice);
            txtDiscount.Text = Math.Round(dblTotalDiscount, 2).ToString();
            lblTotalPay.Text = Math.Round((dblTotalPrice - dblTotalDiscount), 2).ToString();// String.Format("{0:C}", dblDiscounted);
            Session["Discount"] = Math.Round(dblTotalDiscount, 2).ToString();
        }
    }

    private void AddDataToDataTable()
    {
        try
        {
            SaleBLL objSBLL = new SaleBLL();
            List<Sale> objSale = objSBLL.GetProductDetailByBarCode(Productid.Value.Trim());
            RepopulateDataTableWithDiscountPrice();
            if (null != objSale && objSale.Count > 0)
            {
                InsertDataintoTempDataTable(objSale[0], true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('1')", true);

                lblError.InnerHtml = "Product Bar Code not found";
            }
        }
        catch (ConstraintException ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('1')", true);
            lblError.InnerHtml = "Product all ready added";
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    private void InsertDataintoTempDataTable(Sale objSale, bool blnIsSingleEntry)
    {
        if (lnkAddMore.Text.ToLower() == "update")
        {
            DeleteRowFromTempTable(objSale);
        }

        DataRow dtRow = dtProductDetail.NewRow();
        dtRow["ProductID"] = objSale.ProductID;
        dtRow["BarCode"] = objSale.BarCode;
        dtRow["ProductName"] = objSale.ProductName;
        dtRow["SizeID"] = objSale.SizeID;
        dtRow["SizeName"] = objSale.SizeName;
        dtRow["Quantity"] = (blnIsSingleEntry == true ? Convert.ToDecimal(txtQuantity.Text.Trim()) : objSale.Quantity);
        dtRow["Unit"] = objSale.UnitPrice;
        dtRow["PDiscount"] = (blnIsSingleEntry == true ? 0 : objSale.Discount);
        dtRow["Tax"] = objSale.Tax; ;
        dtRow["Price"] = objSale.Price * (blnIsSingleEntry == true ? Convert.ToDecimal(txtQuantity.Text.Trim()) : objSale.Quantity);
        dtProductDetail.Rows.Add(dtRow);
    }

    private void DeleteRowFromTempTable(Sale objSale)
    {
        var rows = dtProductDetail.Select("ProductID ="+objSale.ProductID);
        foreach (var row in rows)
            row.Delete();
        dtProductDetail.AcceptChanges();
        txtProductBarCode.Enabled = true;
        lnkAddMore.Text = "Add";
        //dtProductDetail.AsEnumerable().Where(a => Convert.ToInt32(a["ProductID"]) == objSale.ProductID).CopyToDataTable();
    }



    protected string ShowValue(string strUnit, string Qnty, string Tax)
    {
        string strCalTax = string.Empty;
        strCalTax = string.Format("{0:0.00}", (Convert.ToDecimal(strUnit) * Convert.ToDecimal(Qnty)) * Convert.ToDecimal(Tax) / 100);

        return strCalTax;
    }

    private void RepopulateDataTableWithDiscountPrice()
    {
        if (gvGrid.Rows.Count > 0)
        {
            for (int i = 0; i < gvGrid.Rows.Count; i++)
            {
                TextBox txtPDiscount = (TextBox)gvGrid.Rows[i].Cells[6].FindControl("txtPDiscount");
                dtProductDetail.Rows[i]["PDiscount"] = (string.IsNullOrEmpty(txtPDiscount.Text.Trim()) ? 0 : Convert.ToDecimal(txtPDiscount.Text.Trim()));

                decimal dblPrice = Convert.ToDecimal(dtProductDetail.Rows[i]["Unit"].ToString()) * Convert.ToDecimal(dtProductDetail.Rows[i]["Quantity"].ToString());
                dblPrice = dblPrice + dblPrice * (Convert.ToDecimal(dtProductDetail.Rows[i]["Tax"].ToString()) / 100);

                if (!string.IsNullOrEmpty(txtPDiscount.Text))
                {
                    dtProductDetail.Rows[i]["Price"] = dblPrice - (dblPrice * Convert.ToDecimal(txtPDiscount.Text.Trim()) / 100);
                }
                else
                {
                    dtProductDetail.Rows[i]["Price"] = (Convert.ToDecimal(gvGrid.Rows[i].Cells[5].Text) * Convert.ToDecimal(gvGrid.Rows[i].Cells[4].Text));
                }
                dtProductDetail.AcceptChanges();
            }
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
                int intProductID = Convert.ToInt32(e.CommandArgument.ToString());
                PopulateForm(intProductID);
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

    private void PopulateForm(int vintProductID)
    {
        for (int i = 0; i < dtProductDetail.Rows.Count; i++)
        {
            if (dtProductDetail.Rows[i]["ProductID"].ToString() == vintProductID.ToString())
            {
                txtProductBarCode.Text = dtProductDetail.Rows[i]["BarCode"].ToString();
                txtQuantity.Text = dtProductDetail.Rows[i]["Quantity"].ToString();
                //dtProductDetail.Rows[i].Delete();
                lnkAddMore.Text = "Update";
                txtProductBarCode.Enabled = false;
                break;
            }
        }
        //dtProductDetail.AcceptChanges();
        PopulateProductDetail();
        CalculateTotalPrice();

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

    protected void lnkFinalChekout_Click(object sender, EventArgs e)
    {
        if (null != dtProductDetail && dtProductDetail.Rows.Count > 0)
        {
            RepopulateDataTableWithDiscountPrice();
            CalculateTotalPrice();
            Session["dtProductDetail"] = dtProductDetail;
            param = Constants.MODE + "=" + SelectedMode + "&" + Constants.ID + "=" + SaleID;
            param = Common.GenerateBASE64WithObfuscateApp(param);
            vstrLink = "FinalChekoutSaleOrder.aspx?q=" + param;
            Response.Redirect(vstrLink, false);
        }
    }



}