using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.Component;
using System.Data;
using BLL.BusinessObject;
using CSWeb.Utility;
using System.Diagnostics;

public partial class Modules_AddEditProductPurchase : PageBase
{

    string vstrLink = string.Empty;
    string param = string.Empty;
    List<PurchasedProduct> gobjProduct;


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
    protected Int32 PurchaseID
    {
        get
        {
            if (ViewState["PurchaseID"] != null)
                return Convert.ToInt32(ViewState["PurchaseID"]);
            else
                return 0;
        }
        set
        {
            ViewState["PurchaseID"] = value;
        }
    }

    //protected DataTable dtProductDetail
    //{
    //    get
    //    {
    //        if (ViewState["dtProductDetail"] != null)
    //            return (DataTable)ViewState["dtProductDetail"];
    //        else
    //            return null;
    //    }
    //    set
    //    {
    //        ViewState["dtProductDetail"] = value;
    //    }
    //}

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
        if (!Page.IsPostBack)
        {
            PopulateAutoCompleteDataTable();
            PopulateManufacturer();

            gobjProduct = new List<PurchasedProduct>();
            Session["PurchasedProduct"] = gobjProduct;

            string strQuery = Request.QueryString["q"];
            if (!string.IsNullOrEmpty(strQuery))
            {
                Dictionary<String, String> objQuery = Common.PopulateDictionaryFromQueryString(strQuery);
                SelectedMode = objQuery["MODE"].ToString();
                //PurchaseID = Convert.ToInt32(objQuery["ID"].ToString());

                if (SelectedMode == Constants.MODE_EDIT)
                {
                    Session["dtProductDetail"] = null;
                    lblHeader.Text = "EDIT | Product Purchase";
                    PurchaseID = Convert.ToInt32(objQuery["ID"].ToString());
                    PurchaseRecord objPurchaseRecord = new PurchaseRecord();
                    objPurchaseRecord.PurchaseID = PurchaseID;
                    new ProductPurchaseBLL().GetPurchaseByID(ref objPurchaseRecord);
                    gobjProduct = objPurchaseRecord.ProductsPurchased;
                    Session["PurchasedProduct"] = gobjProduct;
                    cmbManufacturer.SelectedValue = objPurchaseRecord.ManufacturerID.ToString();
                    txtDateOfPurchase.Value = objPurchaseRecord.PurchaseDate;
                    lblTotalAmount.Text = Math.Round(objPurchaseRecord.TotalBuyingPrice, 2).ToString();
                    lblTotalQuantity.Text = objPurchaseRecord.TotalQty.ToString();
                    txtPurchaseID.Value = PurchaseID.ToString();
                    PopulateGrid();
                }
                else
                {
                    lblHeader.Text = "ADD | Product Purchase";
                    txtPurchaseID.Value = "0";
                }
            }
            else
            {
                Response.Redirect("BuyingInterface.aspx", false);
            }

            //if (null == Session["PurchasedProduct"])
            //{
            //    gobjProduct = new List<PurchasedProduct>();
            //}
            //else
            //{
            //    gobjProduct = (List<PurchasedProduct>)Session["PurchasedProduct"];
            //}
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

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('1')", true);
        }
    }

    private void PopulateManufacturer()
    {
        List<Manufacturer> lstManufacturer = new ManufacturerBLL().GetAllActive();

        Common.BindControl(cmbManufacturer, lstManufacturer, "CompanyName", "ManufacturerID", Constants.ControlType.DropDownList, true);
    }

    private void PopulateGrid()
    {
        gvGrid.DataSource = gobjProduct;
        gvGrid.DataBind();
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
            //if (e.CommandName == "Edit")
            //{
            //    int intProductID = Convert.ToInt32(e.CommandArgument.ToString());
            //    PopulateForm(intProductID);
            //}

            if (e.CommandName == "Delete")
            {

                int intProductID = Convert.ToInt32(e.CommandArgument.ToString());
                if (Session["PurchasedProduct"] != null)
                {
                    gobjProduct = (List<PurchasedProduct>)Session["PurchasedProduct"];
                    for (int i = 0; i < gobjProduct.Count; i++)
                    {
                        if (gobjProduct[i].ProductID == intProductID)
                        {
                            int intTotalQty = 0;
                            for (int j = 0; j < gobjProduct[i].PurchasedQty.Count; j++)
                            {
                                intTotalQty += gobjProduct[i].PurchasedQty[j].Quantity;
                            }

                            lblTotalQuantity.Text = Convert.ToString(Convert.ToInt32(lblTotalQuantity.Text) - intTotalQty);
                            lblTotalAmount.Text = Math.Round(( Convert.ToDecimal( lblTotalAmount.Text) - (intTotalQty * gobjProduct[i].BuyingPrice)), 2).ToString();

                            gobjProduct.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }


    protected void gvGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        RefreshGrid();
    }

    protected void gvGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gvGrid.EditIndex = e.NewEditIndex;
    }

    #endregion

    protected void lnkAddMore_Click(object sender, EventArgs e)
    {
        PopulateData();

        txtProductBarCode.Text = "";
        Productid.Value = "0";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('2')", true);
    }

    private void RefreshGrid()
    {
        PopulateGrid();

        txtProductBarCode.Text = "";
        Productid.Value = "0";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('2')", true);
    }
    protected void lnkBtnSaveDS_Click(object sender, EventArgs e)
    {
        PurchasedProduct objPurchasedProduct = new PurchasedProduct();
        objPurchasedProduct.ProductID = Convert.ToInt32(htnProductID.Value);
        objPurchasedProduct.ProductName = lblProduct.Text;
        objPurchasedProduct.BarCode = lblBarCode.Text;
        objPurchasedProduct.BuyingPrice = Convert.ToDecimal(txtBuyingPrice.Text);
        objPurchasedProduct.Margin = Convert.ToDecimal(txtMargin.Text);
        objPurchasedProduct.Tax = Convert.ToDecimal(txtTax.Text);
        objPurchasedProduct.SellingPrice = Convert.ToDecimal(txtSellingPrice.Text);
        objPurchasedProduct.SizeQty = "";

        ProductSize objProductSize;
        List<Size> lstSizeIDs = (List<Size>)Session["ProductSizeIDs"];
        List<ProductSize> lstProductSizes = new List<ProductSize>();
        int intTotalQty = 0;
        decimal dblTotalAmount = Convert.ToDecimal(lblTotalAmount.Text);

        for (int i = 0; i < lstSizeIDs.Count; i++)
        {
            if (!String.IsNullOrEmpty(Request.Form["txtSize_" + lstSizeIDs[i].SizeID.ToString()]))
            {
                objProductSize = new ProductSize();
                objProductSize.ProductID = objPurchasedProduct.ProductID;
                objProductSize.SizeID = lstSizeIDs[i].SizeID;
                objProductSize.Quantity = Convert.ToInt32(Request.Form["txtSize_" + lstSizeIDs[i].SizeID.ToString()].ToString());
                lstProductSizes.Add(objProductSize);

                intTotalQty += objProductSize.Quantity;

                if (i > 0)
                    objPurchasedProduct.SizeQty += "<br/>";

                objPurchasedProduct.SizeQty += lstSizeIDs[i].SizeName + ": " + objProductSize.Quantity;
            }
        }
        objPurchasedProduct.PurchasedQty = lstProductSizes;

        if (Session["PurchasedProduct"] != null)
            gobjProduct = (List<PurchasedProduct>)Session["PurchasedProduct"];

        gobjProduct.Add(objPurchasedProduct);
        Session["PurchasedProduct"] = gobjProduct;

        // lblMsg.Text = 
        PopulateGrid();

        lblTotalQuantity.Text = Convert.ToString( intTotalQty + Convert.ToInt32(lblTotalQuantity.Text));
        lblTotalAmount.Text = Math.Round((dblTotalAmount + (intTotalQty * objPurchasedProduct.BuyingPrice)),2).ToString();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('2')", true);

    }

    
        
    protected string ShowValue(string strUnit, string Qnty, string Tax)
    {
        string strCalTax = string.Empty;
        strCalTax = string.Format("{0:0.00}", (Convert.ToDecimal(strUnit) * Convert.ToDecimal(Qnty)) * Convert.ToDecimal(Tax) / 100);

        return strCalTax;
    }

    private void PopulateData()
    {
        int intProductID =0;
        int.TryParse(Productid.Value, out intProductID);
        if (intProductID > 0)
        {
            bool blnIsProductAdded = false;
            if (Session["PurchasedProduct"] != null)
                gobjProduct = (List<PurchasedProduct>)Session["PurchasedProduct"];

            for (int i = 0; i < gobjProduct.Count; i++)
            {
                if (gobjProduct[i].ProductID == intProductID)
                {
                    blnIsProductAdded = true;
                    break;
                }
            }

            if (!blnIsProductAdded)
            {

                Product objProduct = new ProductBLL().GetProductByID(intProductID);
                htnProductID.Value = intProductID.ToString();
                lblProduct.Text = objProduct.ProductName;
                lblBarCode.Text = objProduct.BarCode;
                txtBuyingPrice.Text = objProduct.BuyingPrice.ToString();
                txtTax.Text = objProduct.Tax.ToString();
                txtMargin.Text = objProduct.Margin.ToString();
                txtSellingPrice.Text = objProduct.SellingPrice.ToString();

                List<Size> lstSize = new SizeBLL().GetSize(0);
                string[] arrSizeIDs = objProduct.SizeID.Split(',');
                List<Size> lstSizeIDs = new List<Size>();

                System.Web.UI.HtmlControls.HtmlGenericControl containerDiv;
                System.Web.UI.HtmlControls.HtmlGenericControl textBoxDiv;

                for (int i = 0; i < arrSizeIDs.Length; i++)
                {
                    for (int j = 0; j < lstSize.Count; j++)
                    {
                        if (arrSizeIDs[i] == lstSize[j].SizeID.ToString())
                        {
                            containerDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            containerDiv.Controls.Add(CreateLabel(lstSize[j].SizeID, lstSize[j].SizeName));
                            plhQty.Controls.Add(containerDiv);
                            textBoxDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            textBoxDiv.Controls.Add(CreateTextBox(lstSize[j].SizeID, 0));
                            plhQty.Controls.Add(textBoxDiv);
                            lstSizeIDs.Add(lstSize[j]);
                            break;
                        }
                    }
                }
                Session["ProductSizeIDs"] = lstSizeIDs;
                lblError.InnerHtml = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AddEditPurchase", "ShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);
            }
            else
            {
                lblError.InnerHtml = "Product already added";
            }
        }
        else
        {
            lblError.InnerHtml = "Invalid entry";
        }

        
    }

    private Label CreateLabel(int intSizeID, string strSizeName)
    {
        Label objLabel = new Label();
        objLabel.ID = "lblSize_" + intSizeID.ToString();
        objLabel.Text = strSizeName;
        return objLabel;
    }

    private TextBox CreateTextBox(int intSizeID, int intQuantity)
    {
        TextBox objTextBox = new TextBox();
        objTextBox.ID = "txtSize_" + intSizeID.ToString();
        objTextBox.Text = intQuantity.ToString();
        objTextBox.CssClass = "txtCred";
        objTextBox.Attributes.Add("onkeyup", "extractNumber(this,0,false);");
        objTextBox.Attributes.Add("onblur", "extractNumber(this,0,false);");
        return objTextBox;
    }
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        if (Session["PurchasedProduct"] != null)
        {
            gobjProduct = (List<PurchasedProduct>)Session["PurchasedProduct"];
            PurchaseRecord objPurchaseRecord = new PurchaseRecord();
            objPurchaseRecord.ManufacturerID = Convert.ToInt32(cmbManufacturer.SelectedValue);
            objPurchaseRecord.PurchaseDate = txtDateOfPurchase.Value;
            objPurchaseRecord.PurchaseID = Convert.ToInt32(txtPurchaseID.Value);
            objPurchaseRecord.ProductsPurchased = gobjProduct;

            new ProductPurchaseBLL().AddEditProductPurchase(ref objPurchaseRecord);

            if (objPurchaseRecord.ReturnValue > 0)
            {
                Response.Redirect("BuyingInterface.aspx", true);
            }
            else
            {
                txtProductBarCode.Text = "";
                Productid.Value = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMsg", "alert('Error in saving record!')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopulateType", "Populate('2')", true);

            }

        }
    }
}