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
            string strQuery = Request.QueryString["q"];
            if (!string.IsNullOrEmpty(strQuery))
            {
                Dictionary<String, String> objQuery = Common.PopulateDictionaryFromQueryString(strQuery);
                SelectedMode = objQuery["MODE"].ToString();
                int intProductPurchaseID = Convert.ToInt32(objQuery["ID"].ToString());
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

            if (SelectedMode == Constants.MODE_EDIT)
            {

                //PopulateSaleDetail(SaleID);
                lblHeader.Text = "EDIT | Product Purchase";
            }
            else
            {
                lblHeader.Text = "ADD | Product Purchase";
            }
        }
    }

    public void PopulateProduct()
    {
        Common.BindControl(cmbProduct, new ProductBLL().GetAllActiveProduct(), "ProductName", "ProductID", Constants.ControlType.DropDownList, true);
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
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("BuyingInterface.aspx", false);
    }
}