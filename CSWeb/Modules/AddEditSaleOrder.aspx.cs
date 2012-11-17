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

    protected DataTable dtAutoComplete
    {
        get
        {
            if (ViewState["dtAutoComplete"] != null)
                return (DataTable)ViewState["dtAutoComplete"];
            else
                return null;
        }
        set
        {
            ViewState["dtAutoComplete"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                serversideEvent = Page.ClientScript.GetPostBackEventReference(lnkAddMore, string.Empty);
                PopulateAutoCompleteDataTable();
                // Auto
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

    private void PopulateAutoCompleteDataTable()
    {
        SaleBLL objSaleBLL = new SaleBLL();
        dtAutoComplete = objSaleBLL.PopulateAutoCompleteProductInformation();
        PopulateAutoCompleteProductInformation();
    }
    private void PopulateAutoCompleteProductInformation()
    {

        if (null != dtAutoComplete && dtAutoComplete.Rows.Count > 0)
        {
            hdnByBarCode.Value = "";
            hdnByProductName.Value = "";
            for (int i = 0; i < dtAutoComplete.Rows.Count; i++)
            {
                hdnByBarCode.Value += dtAutoComplete.Rows[i]["ProductID"].ToString() + "##" + dtAutoComplete.Rows[i]["BarCode"].ToString() + "##" + dtAutoComplete.Rows[i]["ProductName"].ToString() + "@@";
                hdnByProductName.Value += dtAutoComplete.Rows[i]["ProductID"].ToString() + "##" + dtAutoComplete.Rows[i]["ProductName"].ToString() + "##" + dtAutoComplete.Rows[i]["BarCode"].ToString() + "@@";
            }
            if (hdnByBarCode.Value.Length > 0)
            {
                hdnByBarCode.Value = hdnByBarCode.Value.Substring(0, hdnByBarCode.Value.Length - 2);
            }

            if (hdnByProductName.Value.Length > 0)
            {
                hdnByProductName.Value = hdnByProductName.Value.Substring(0, hdnByProductName.Value.Length - 2);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('2')", true);
        }
    }

    private DataTable CreateTableStructure()
    {
        DataTable dtData = new DataTable();

        dtData.Columns.Add("ProductID", typeof(int));
        dtData.Columns.Add("BarCode", typeof(string));
        dtData.Columns.Add("PBarCodeWithSize", typeof(string));
        dtData.Columns.Add("ProductName", typeof(string));
        dtData.Columns.Add("SizeID", typeof(int));
        dtData.Columns.Add("SizeBarcodeID", typeof(string));
        dtData.Columns.Add("SizeBarcode", typeof(string));
        dtData.Columns.Add("SizeName", typeof(string));
        dtData.Columns.Add("Sizes", typeof(string));
        dtData.Columns.Add("Quantity", typeof(decimal));
        dtData.Columns.Add("Unit", typeof(decimal));
        dtData.Columns.Add("PDiscount", typeof(decimal));
        dtData.Columns.Add("DiscountType", typeof(string));
        dtData.Columns.Add("Tax", typeof(decimal));
        dtData.Columns.Add("Price", typeof(decimal));
        dtData.Columns.Add("TPrice", typeof(decimal));

        DataColumn[] planfeaturesKeyColumns = new DataColumn[2];

        planfeaturesKeyColumns[0] = dtData.Columns["ProductID"];
        planfeaturesKeyColumns[1] = dtData.Columns["SizeID"];
        dtData.PrimaryKey = planfeaturesKeyColumns;
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('2')", true);

    }
    #endregion
    protected void lnkAddMore_Click(object sender, EventArgs e)
    {
        ViewState["DiscountType"] = "%";
        PopulateInformation();

    }

    private void PopulateInformation()
    {
        AddDataToDataTable();
        PopulateProductDetail();
        CalculateTotalPrice();

        txtProductBarCode.Text = "";
        txtQuantity.Text = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('2')", true);
    }

    private void CalculateTotalPrice()
    {
        decimal dblTotalPrice = 0;
        decimal dblDiscounted = 0;
        decimal dblTotalDiscount = 0;
        string selectedVal = string.Empty;
        if (null != dtProductDetail)
        {
            for (int i = 0; i < dtProductDetail.Rows.Count; i++)
            {
                decimal UQ = (Convert.ToDecimal(dtProductDetail.Rows[i]["Unit"].ToString()) * Convert.ToDecimal(dtProductDetail.Rows[i]["Quantity"].ToString()));
                UQ = UQ + (UQ * +Convert.ToDecimal(dtProductDetail.Rows[i]["Tax"].ToString()) / 100);

                decimal decDiscount = UQ;

                selectedVal = dtProductDetail.Rows[i]["DiscountType"].ToString();
                if (selectedVal == "%")
                {
                    decDiscount = (decDiscount * (Convert.ToDecimal(dtProductDetail.Rows[i]["PDiscount"].ToString()) / 100));
                }
                else
                {
                    decDiscount = Convert.ToDecimal(dtProductDetail.Rows[i]["PDiscount"].ToString());
                }

                dblTotalDiscount += decDiscount;


                dblTotalPrice += UQ;
            }



            lblTotalAmount.Text = Math.Round(dblTotalPrice, 2).ToString();// String.Format("{0:C}", dblTotalPrice);
            txtDiscount.Text = string.Format("{0:0.00}", dblTotalDiscount);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('2')", true);

                lblError.InnerHtml = "Product Bar Code not found";
            }
        }
        catch (ConstraintException ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('2')", true);
            lblError.InnerHtml = "Product allready added or select diffrent size";
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
        dtRow["PBarCodeWithSize"] = objSale.BarCode + "-" + objSale.SizeBarCode;
        dtRow["ProductName"] = objSale.ProductName;
        dtRow["SizeID"] = objSale.SizeID;
        dtRow["SizeBarcodeID"] = objSale.SizeID + "||" + objSale.SizeBarCode;
        dtRow["SizeBarcode"] = objSale.SizeBarCode;
        dtRow["SizeName"] = objSale.SizeName;
        dtRow["Sizes"] = objSale.Sizes;
        dtRow["Quantity"] = (blnIsSingleEntry == true ? Convert.ToDecimal(txtQuantity.Text.Trim()) : objSale.Quantity);
        dtRow["Unit"] = objSale.UnitPrice;
        dtRow["PDiscount"] = (blnIsSingleEntry == true ? 0 : objSale.Discount);
        if (!string.IsNullOrEmpty(objSale.DiscountType))
        {
            dtRow["DiscountType"] = objSale.DiscountType;
        }
        else
        {
            dtRow["DiscountType"] = ViewState["DiscountType"].ToString();
        }

        dtRow["Tax"] = objSale.Tax; ;
        dtRow["Price"] = objSale.Price;
        dtRow["TPrice"] = objSale.Price * (blnIsSingleEntry == true ? Convert.ToDecimal(txtQuantity.Text.Trim()) : objSale.Quantity);
        dtProductDetail.Rows.Add(dtRow);
    }

    private void DeleteRowFromTempTable(Sale objSale)
    {
        var rows = dtProductDetail.Select("ProductID =" + objSale.ProductID);
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
        /*if (gvGrid.Rows.Count > 0)
        {
            for (int i = 0; i < gvGrid.Rows.Count; i++)
            {
                TextBox txtPDiscount = (TextBox)gvGrid.Rows[i].Cells[6].FindControl("txtPDiscount");
                dtProductDetail.Rows[i]["PDiscount"] = (string.IsNullOrEmpty(txtPDiscount.Text.Trim()) ? 0 : Convert.ToDecimal(txtPDiscount.Text.Trim()));

                decimal dblPrice = Convert.ToDecimal(dtProductDetail.Rows[i]["Unit"].ToString()) * Convert.ToDecimal(dtProductDetail.Rows[i]["Quantity"].ToString());
                dblPrice = dblPrice + dblPrice * (Convert.ToDecimal(dtProductDetail.Rows[i]["Tax"].ToString()) / 100);

                if (!string.IsNullOrEmpty(txtPDiscount.Text))
                {
                    dtProductDetail.Rows[i]["TPrice"] = dblPrice - (dblPrice * Convert.ToDecimal(txtPDiscount.Text.Trim()) / 100);
                }
                else
                {
                    dtProductDetail.Rows[i]["TPrice"] = (Convert.ToDecimal(gvGrid.Rows[i].Cells[5].Text) * Convert.ToDecimal(gvGrid.Rows[i].Cells[4].Text));
                }
                dtProductDetail.AcceptChanges();
            }
        }*/
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
                int ProductID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ProductID"));
                DropDownList ddlPSize = (DropDownList)e.Row.FindControl("ddlPSize");
                PopulateSizeDropDown(ddlPSize, ProductID);
                ddlPSize.Items.FindByValue((e.Row.FindControl("lblSizeBarcodeID") as Label).Text).Selected = true;

                DropDownList ddlDType = (DropDownList)e.Row.FindControl("ddlDType");
                ddlDType.Items.FindByValue((e.Row.FindControl("lblDiscType") as Label).Text).Selected = true;

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

    private void PopulateSizeDropDown(DropDownList ddlList, int vintProductID)
    {
        try
        {
            List<Sale> objLSize = new List<Sale>();

            string sizes = string.Empty;

            if (null != dtProductDetail && dtProductDetail.Rows.Count > 0)
            {
                for (int i = 0; i < dtProductDetail.Rows.Count; i++)
                {
                    if (dtProductDetail.Rows[i]["ProductID"].ToString() == vintProductID.ToString())
                    {
                        sizes = dtProductDetail.Rows[i]["Sizes"].ToString();
                        objLSize = PopulateSizeListObject(sizes);
                        CSWeb.Utility.Common.BindControl(ddlList, objLSize, "SizeName", "StringSizeID", Constants.ControlType.DropDownList, true);
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    private List<Sale> PopulateSizeListObject(string sizes)
    {
        Sale objSize = new Sale();
        List<Sale> objLSize = new List<Sale>();
        string[] strRecordSeparator = sizes.Split(new string[] { "##" }, StringSplitOptions.None);
        if (strRecordSeparator.Length > 0)
        {
            for (int ii = 0; ii < strRecordSeparator.Length; ii++)
            {
                string[] strDataSeparator = strRecordSeparator[ii].Split(new string[] { "@@" }, StringSplitOptions.None);
                if (strDataSeparator.Length > 1)
                {
                    objSize = new Sale();

                    objSize.StringSizeID = strDataSeparator[0];
                    objSize.SizeName = strDataSeparator[1];
                    objLSize.Add(objSize);
                }
            }
        }
        return objLSize;
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

                LinkButton gv = (LinkButton)e.CommandSource;
                GridViewRow gvr = (GridViewRow)gv.Parent.Parent;
                int rowIndex = gvr.RowIndex;
                Label lblSizeID = gvGrid.Rows[rowIndex].Cells[0].Controls[0].FindControl("lblSizeID") as Label;

                int intProductID = Convert.ToInt32(e.CommandArgument.ToString());
                for (int i = 0; i < dtProductDetail.Rows.Count; i++)
                {
                    if (dtProductDetail.Rows[i]["ProductID"].ToString() == intProductID.ToString() && dtProductDetail.Rows[i]["SizeID"].ToString() == lblSizeID.Text.Trim())
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
        PopulateAutoCompleteProductInformation();
    }

    protected void gvGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gvGrid.EditIndex = e.NewEditIndex;
    }

    #endregion

    protected void lnkFinalChekout_Click(object sender, EventArgs e)
    {
        bool isReadyForFinalCheckout = true;
        if (null != dtProductDetail && dtProductDetail.Rows.Count > 0)
        {
            for (int i = 0; i < dtProductDetail.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(dtProductDetail.Rows[i]["SizeBarcode"].ToString()) || string.IsNullOrEmpty(dtProductDetail.Rows[i]["Quantity"].ToString()))
                {
                    isReadyForFinalCheckout = false;
                    break;
                }
            }

            if (isReadyForFinalCheckout)
            {
                RepopulateDataTableWithDiscountPrice();
                CalculateTotalPrice();
                Session["dtProductDetail"] = dtProductDetail;
                param = Constants.MODE + "=" + SelectedMode + "&" + Constants.ID + "=" + SaleID;
                param = Common.GenerateBASE64WithObfuscateApp(param);
                vstrLink = "FinalChekoutSaleOrder.aspx?q=" + param;
                Response.Redirect(vstrLink, false);
            }
            else
            {
                lblError.InnerHtml = "Opeartion failed. Please check product list";
            }
        }
    }



    protected void lblQuantity_TextChanged(object sender, EventArgs e)
    {
        TextBox txtQuantity = (TextBox)sender;
        int rowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
        Label lblProductID = gvGrid.Rows[rowIndex].Cells[0].Controls[0].FindControl("lblProductID") as Label;

        decimal PQuantity = Convert.ToDecimal(txtQuantity.Text.Trim());
        int PID = Convert.ToInt32(lblProductID.Text.Trim());

        for (int i = 0; i < dtProductDetail.Rows.Count; i++)
        {
            if (dtProductDetail.Rows[i]["ProductID"].ToString() == PID.ToString())
            {
                dtProductDetail.Rows[i]["Quantity"] = PQuantity;
                dtProductDetail.Rows[i]["TPrice"] = Convert.ToDecimal(dtProductDetail.Rows[i]["Price"]) * PQuantity;
                break;
            }
        }
        dtProductDetail.AcceptChanges();
        PopulateProductDetail();
        CalculateTotalPrice();
        PopulateAutoCompleteProductInformation();
    }

    protected void txtPDiscount_TextChanged(object sender, EventArgs e)
    {
        TextBox txtPDiscount = (TextBox)sender;
        int rowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
        Label lblSizeID = gvGrid.Rows[rowIndex].Cells[0].Controls[0].FindControl("lblSizeID") as Label;
        Label lblProductID = gvGrid.Rows[rowIndex].Cells[0].Controls[0].FindControl("lblProductID") as Label;
        TextBox txtQuantity = gvGrid.Rows[rowIndex].Cells[0].Controls[0].FindControl("lblQuantity") as TextBox;
        DropDownList ddlDType = gvGrid.Rows[rowIndex].Cells[0].Controls[0].FindControl("ddlDType") as DropDownList;
        string selectedVal = ddlDType.SelectedItem.Text;
        decimal PQuantity = Convert.ToDecimal(txtQuantity.Text.Trim());
        int PID = Convert.ToInt32(lblProductID.Text.Trim());

        for (int i = 0; i < dtProductDetail.Rows.Count; i++)
        {
            if (dtProductDetail.Rows[i]["ProductID"].ToString() == PID.ToString() && dtProductDetail.Rows[i]["SizeID"].ToString() == lblSizeID.Text.ToString())
            {
                decimal dblPrice = Convert.ToDecimal(dtProductDetail.Rows[i]["Unit"].ToString()) * Convert.ToDecimal(PQuantity);
                dblPrice = dblPrice + dblPrice * (Convert.ToDecimal(dtProductDetail.Rows[i]["Tax"].ToString()) / 100);

                if (!string.IsNullOrEmpty(txtPDiscount.Text))
                {
                    if (selectedVal == "%")
                    {
                        dtProductDetail.Rows[i]["TPrice"] = dblPrice - (dblPrice * Convert.ToDecimal(txtPDiscount.Text.Trim()) / 100);
                        dtProductDetail.Rows[i]["PDiscount"] = Convert.ToDecimal(txtPDiscount.Text.Trim());
                    }
                    else
                    {
                        dtProductDetail.Rows[i]["TPrice"] = dblPrice - Convert.ToDecimal(txtPDiscount.Text.Trim());
                        dtProductDetail.Rows[i]["PDiscount"] = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(txtPDiscount.Text.Trim())));
                    }

                    dtProductDetail.Rows[i]["DiscountType"] = selectedVal;
                }
                else
                {
                    dtProductDetail.Rows[i]["TPrice"] = dblPrice;
                }
                break;
            }
        }
        dtProductDetail.AcceptChanges();
        PopulateProductDetail();
        CalculateTotalPrice();
        PopulateAutoCompleteProductInformation();
    }

    protected void ddlDType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlDType = (DropDownList)sender;
        int rowIndex = ((GridViewRow)((DropDownList)sender).NamingContainer).RowIndex;
        TextBox txtPDiscount = gvGrid.Rows[rowIndex].Cells[0].Controls[0].FindControl("txtPDiscount") as TextBox;
        txtPDiscount.Text = "";
        string selectedVal = ddlDType.SelectedItem.Text;
        if (selectedVal == "%")
        {
            txtPDiscount.MaxLength = 2;
        }
        else
        {
            txtPDiscount.MaxLength = 8;
        }
        PopulateAutoCompleteProductInformation();
    }

    protected void ddlPSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlPSize = (DropDownList)sender;
            int rowIndex = ((GridViewRow)((DropDownList)sender).NamingContainer).RowIndex;
            Label lblSizeID = gvGrid.Rows[rowIndex].Cells[0].Controls[0].FindControl("lblSizeID") as Label;
            Label lblBarCode = gvGrid.Rows[rowIndex].Cells[0].Controls[0].FindControl("lblBarCode") as Label;
            Label lblProductID = gvGrid.Rows[rowIndex].Cells[0].Controls[0].FindControl("lblProductID") as Label;
            Label lblPBarCodeWithSize = gvGrid.Rows[rowIndex].Cells[0].Controls[0].FindControl("lblPBarCodeWithSize") as Label;


            for (int i = 0; i < dtProductDetail.Rows.Count; i++)
            {
                if (dtProductDetail.Rows[i]["ProductID"].ToString() == lblProductID.Text.ToString() && dtProductDetail.Rows[i]["SizeID"].ToString() == lblSizeID.Text.ToString())
                {
                    if (ddlPSize.SelectedIndex > 0)
                    {
                        string Barcode = dtProductDetail.Rows[i]["BarCode"].ToString();
                        string[] SizeID = ddlPSize.SelectedValue.ToString().Split(new string[] { "||" }, StringSplitOptions.None);
                        if (SizeID.Length > 0)
                        {
                            lblSizeID.Text = SizeID[0];
                            lblPBarCodeWithSize.Text = lblBarCode.Text + "-" + SizeID[1];
                        }
                        dtProductDetail.Rows[i]["PBarCodeWithSize"] = lblBarCode.Text + "-" + SizeID[1];
                        dtProductDetail.Rows[i]["SizeID"] = SizeID[0];
                        dtProductDetail.Rows[i]["SizeName"] = ddlPSize.SelectedItem.Text.ToString();
                        dtProductDetail.Rows[i]["SizeBarcode"] = SizeID[1];
                        dtProductDetail.Rows[i]["SizeBarcodeID"] = ddlPSize.SelectedValue.ToString();
                    }
                    else
                    {
                        dtProductDetail.Rows[i]["PBarCodeWithSize"] = "";
                        dtProductDetail.Rows[i]["SizeID"] = 0;
                        dtProductDetail.Rows[i]["SizeName"] = "";
                        dtProductDetail.Rows[i]["SizeBarcode"] = "";
                        dtProductDetail.Rows[i]["SizeBarcodeID"] = "";
                    }
                }
                dtProductDetail.AcceptChanges();
                PopulateProductDetail();
                CalculateTotalPrice();
                PopulateAutoCompleteProductInformation();


            }
        }
        catch (ConstraintException ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('2')", true);
            lblError.InnerHtml = "Product all ready added with same size";
        }
    }
}