using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BusinessObject;
using BLL.Component;
using CSWeb.Utility;
using System.Diagnostics;

public partial class Modules_AddEditPurchase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string SelectedMode = "";
            int intProductPurchaseID = 0;
            string strQuery = Request.QueryString["q"];
            if (!string.IsNullOrEmpty(strQuery))
            {
                Dictionary<String, String> objQuery = Common.PopulateDictionaryFromQueryString(strQuery);
                SelectedMode = objQuery["MODE"].ToString();
                intProductPurchaseID = Convert.ToInt32(objQuery["ID"].ToString());
            }
            else
            {
                Response.Redirect("BuyingInterface.aspx", false);
            }
            PopulateProduct();
            PopulateManufacturer();
            PopulateSize();
            PopulateCategory();
            PopulateBrand();
            PopulateSeason();

            if (SelectedMode == Constants.MODE_EDIT)
            {

                //PopulateSaleDetail(SaleID);
                PopulateData(intProductPurchaseID);
                lblHeader.Text = "EDIT | Product Purchase";
            }
            else
            {
                txtDateOfPurchase.Value = string.Format("{0:MM/dd/yyyy}", DateTime.Today);
                txtProductPurchaseID.Value = "0";
                lblHeader.Text = "ADD | Product Purchase";
            }

        }
    }

    public void PopulateProduct()
    {
        Common.BindControl(cmbProduct, new ProductBLL().GetAllActiveProduct(), "ProductName", "ProductID", Constants.ControlType.DropDownList, true);
    }
    public void PopulateSeason()
    {
        Common.BindControl(cmbSeason, new SeasonBLL().GetSeason(), "SeasonName", "SeasonID", Constants.ControlType.DropDownList, true);
    }

    private void PopulateManufacturer()
    {
        try
        {
            List<Manufacturer> lstManufacturer = new ManufacturerBLL().GetAll(new PageInfo());
            cmbManufacturer.DataSource = lstManufacturer;
            cmbManufacturer.DataValueField = "ManufacturerID";
            cmbManufacturer.DataTextField = "CompanyName";
            cmbManufacturer.DataBind();
            cmbManufacturer.Items.Insert(0, new ListItem("--Select--"));
            cmbManufacturer.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void PopulateSize()
    {
        try
        {
            List<Size> lstSize = new SizeBLL().GetSize(0);
            chkSize.DataSource = lstSize;
            chkSize.DataValueField = "SizeID";
            chkSize.DataTextField = "SizeName";
            chkSize.DataBind();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void PopulateCategory()
    {
        try
        {
            List<Category> lstCategory = new CategoryBLL().GetCategory(0);
            cmbCategory.DataSource = lstCategory;
            cmbCategory.DataValueField = "CategoryID";
            cmbCategory.DataTextField = "CategoryName";
            cmbCategory.DataBind();
            cmbCategory.Items.Insert(0, new ListItem("--Select--"));
            cmbCategory.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void PopulateBrand()
    {
        try
        {
            List<Brand> lstBrand = new BrandBLL().GetBrand();
            cmbBrand.DataSource = lstBrand;
            cmbBrand.DataValueField = "BrandID";
            cmbBrand.DataTextField = "BrandName";
            cmbBrand.DataBind();
            cmbBrand.Items.Insert(0, new ListItem("--Select--"));
            cmbBrand.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    protected void lnkBtnSaveDS_Click(object sender, EventArgs e)
    {
        ProductPurchase objProductPurchase = new ProductPurchase();
        objProductPurchase.ProductPurchaseID = Convert.ToInt32(txtProductPurchaseID.Value);
        objProductPurchase.ProductID = Convert.ToInt32(cmbProduct.SelectedValue);
        objProductPurchase.ManufacturerID = Convert.ToInt32(cmbManufacturer.SelectedValue);
        objProductPurchase.BrandID = Convert.ToInt32(cmbBrand.SelectedValue);
        objProductPurchase.CategoryID = Convert.ToInt32(cmbCategory.SelectedValue);
        objProductPurchase.SeasonID = Convert.ToInt32(cmbSeason.SelectedValue);
        objProductPurchase.PurchaseDate =txtDateOfPurchase.Value;
        objProductPurchase.Quantity = Convert.ToInt32(txtQuantity.Text);
        objProductPurchase.BuyingPrice = Convert.ToDecimal(txtBuyingPrice.Text);
        objProductPurchase.Tax = Convert.ToDecimal(txtTax.Text);
        objProductPurchase.Margin = Convert.ToDecimal(txtMargin.Text);
        objProductPurchase.SellingPrice = Convert.ToDecimal(txtSellingPrice.Text);
        objProductPurchase.BarCode = txtBarcode.Text.Trim();
        objProductPurchase.Quantity = Convert.ToInt32(txtQuantity.Text);

        string strSizeIDs = "<data>";
        foreach (ListItem lstSizeIDs in chkSize.Items)
        {
            if (lstSizeIDs.Selected == true)
            {
                strSizeIDs += "<sizes><SizeID>" + lstSizeIDs.Value + "</SizeID></sizes>";
            }
        }
        strSizeIDs += "</data>";

        objProductPurchase.SizeIDs = strSizeIDs;

        new ProductPurchaseBLL().AddEditPurchase(ref objProductPurchase);

        if (objProductPurchase.ReturnValue > 0)
        {
            Response.Redirect("BuyingInterface.aspx", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + objProductPurchase.ReturnMessage + "');", true);
        }
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    { 
        Response.Redirect("BuyingInterface.aspx", false);
    }

    private void PopulateData(int vintProductPurchaseID)
    {
        ProductPurchase objProductPurshase = new ProductPurchase();
        objProductPurshase.ProductPurchaseID = vintProductPurchaseID;
        new ProductPurchaseBLL().GetByID(ref objProductPurshase);
        txtProductPurchaseID.Value = objProductPurshase.ProductPurchaseID.ToString();
        txtDateOfPurchase.Value = objProductPurshase.PurchaseDate;
        cmbManufacturer.SelectedValue = objProductPurshase.ManufacturerID.ToString();
        cmbProduct.SelectedValue = objProductPurshase.ProductID.ToString();
        txtBarcode.Text = objProductPurshase.BarCode;
        cmbBrand.SelectedValue = objProductPurshase.BrandID.ToString();
        cmbCategory.SelectedValue = objProductPurshase.CategoryID.ToString();
        cmbSeason.SelectedValue = objProductPurshase.SeasonID.ToString();
        txtBuyingPrice.Text = objProductPurshase.BuyingPrice.ToString("F2");
        txtTax.Text = objProductPurshase.Tax.ToString("F2");
        txtMargin.Text = objProductPurshase.Margin.ToString("F2");
        txtSellingPrice.Text = objProductPurshase.SellingPrice.ToString("F2");
        txtQuantity.Text = objProductPurshase.Quantity.ToString();

        if (! String.IsNullOrEmpty(objProductPurshase.SizeIDs))
        {
            string[] arrSizeIDs = objProductPurshase.SizeIDs.Split(',');
            for (int i = 0; i < arrSizeIDs.Length; i++)
            {
                for (int j =0; j< chkSize.Items.Count; j++)
                {
                    if (chkSize.Items[j].Value.Equals(arrSizeIDs[i]))
                    {
                        chkSize.Items[j].Selected = true;
                    }
                }
            }
        }

    }

}