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

public partial class Modules_AddEditProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

           // PopulateManufacturer();
            PopulateSize();
            PopulateCategory();
            PopulateBrand();
            PopulateSeason();
            if (Request.QueryString["ProductID"] != null)
            {
                LoadData(int.Parse(Request.QueryString["ProductID"]));
            }

            BindSizeMasterGrid();
            BindCategory();
            BindBrand();
            BindSeason();
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

    private void PopulateManufacturer()
    {
        try
        {
          /*  List<Manufacturer> lstManufacturer = new ManufacturerBLL().GetAll(new PageInfo());
            cmbManufacturer.DataSource = lstManufacturer;
            cmbManufacturer.DataValueField = "ManufacturerID";
            cmbManufacturer.DataTextField = "CompanyName";
            cmbManufacturer.DataBind();
            cmbManufacturer.Items.Insert(0, new ListItem("--Select--"));
            cmbManufacturer.SelectedIndex = 0;*/
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    protected void lnkBtnSaveDS_Click(object sender, EventArgs e)
    {
        try
        {
            BLL.BusinessObject.Product mobjProduct = PopulateFromForm();
            if (null != ViewState["intProductID"] && int.Parse(ViewState["intProductID"].ToString()) > 0)
            {
                mobjProduct.ProductID = int.Parse(ViewState["intProductID"].ToString());
            }
            SaveProduct(mobjProduct);

        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    private void SaveProduct(Product mobjProduct)
    {
        try
        {
            BLL.Component.ProductBLL objProd = new ProductBLL();
            if (mobjProduct.ProductID == 0)
            {
                int mintReturn = objProd.AddProduct(mobjProduct);
                if (mintReturn == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product already exists!');", true);
                    return;
                }
            }
            else
            {
                mobjProduct.UpdatedBy = 1;
                int mintReturn = objProd.EditProduct(mobjProduct);
                if (mintReturn == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product already exists!');", true);
                    return;
                }
            }

            Response.Redirect("Product.aspx");
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    private Product PopulateFromForm()
    {
        BLL.BusinessObject.Product objProduct = new Product();
        try
        {
            objProduct.ProductID = 0;
            objProduct.ProductName = txtProductName.Text.Trim();
            objProduct.Description = txtDescription.Text.Trim();
<<<<<<< HEAD
            objProduct.ManufacturerID = -1; //int.Parse(cmbManufacturer.SelectedValue);
=======
            //objProduct.ManufacturerID = int.Parse(cmbManufacturer.SelectedValue);
>>>>>>> 6b0487ec9674b28a7c0c9dfd5e2a5b2108f99b2e
            objProduct.BrandID = int.Parse(cmbBrand.SelectedValue);
            objProduct.SeasonID = int.Parse(ddlSeason.SelectedValue);
            objProduct.CategoryID = int.Parse(cmbCategory.SelectedValue);
            objProduct.SizeID = GetSizeIDs();// chkSize.SelectedValue == "" ? 0 : int.Parse(chkSize.SelectedValue);
            objProduct.BuyingPrice = Convert.ToDecimal(txtBuyingPrice.Text);
            objProduct.Tax = txtTax.Text == "" ? 0 : Convert.ToDecimal(txtTax.Text);
            objProduct.Margin = Convert.ToDecimal(txtMargin.Text);
            objProduct.SellingPrice = Convert.ToDecimal(txtSellingPrice.Text);
            objProduct.BarCode = txtBarcode.Text.Trim();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
        return objProduct;
    }

    private string GetSizeIDs()
    {
        string strSizeIds = string.Empty;
        for (int i = 0; i < chkSize.Items.Count; i++)
        {
            if (chkSize.Items[i].Selected)
            {
                if (strSizeIds == "")
                {
                    strSizeIds = chkSize.Items[i].Value;
                }
                else
                {
                    strSizeIds = strSizeIds + "," + chkSize.Items[i].Value;
                }
            }
        }
        return strSizeIds;
    }

    private void PopulateProductByID(int ProductID)
    {
        try
        {
            BLL.Component.ProductBLL objProd = new ProductBLL();
            BLL.BusinessObject.Product objProduct = objProd.GetProductByID(ProductID);

            txtProductName.Text = objProduct.ProductName;
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    private void LoadData(int intProductID)
    {
        try
        {
            Product objProduct = new ProductBLL().GetProductByID(intProductID);
            ViewState["intProductID"] = intProductID;

            if (null != objProduct)
            {
                txtProductName.Text = objProduct.ProductName;
                txtDescription.Text = objProduct.Description;
                //cmbManufacturer.SelectedValue = objProduct.ManufacturerID.ToString();
                cmbCategory.SelectedValue = objProduct.CategoryID.ToString();
                cmbBrand.SelectedValue = objProduct.BrandID.ToString();
                ddlSeason.SelectedValue = objProduct.SeasonID.ToString();
                string[] strVals = objProduct.SizeID.Split(',');
                for (int i = 0; i < strVals.Length; i++)
                {
                    for (int j = 0; j < chkSize.Items.Count; j++)
                    {
                        if (chkSize.Items[j].Value == strVals[i])
                        {
                            chkSize.Items[j].Selected = true;
                            break;
                        }
                    }
                }

                txtBuyingPrice.Text = String.Format("{0:0.00}",objProduct.BuyingPrice);
                txtTax.Text = String.Format("{0:0.00}",objProduct.Tax);
                txtMargin.Text = String.Format("{0:0.00}",objProduct.Margin);
                txtSellingPrice.Text = String.Format("{0:0.00}",objProduct.SellingPrice);
                txtBarcode.Text = objProduct.BarCode;

                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "AddEditProduct", "ShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);

            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
    protected void gvSize_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvSize.PageIndex = e.NewPageIndex;
            BindSizeMasterGrid();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
    protected void gvSize_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSize.EditIndex = -1;

        BindSizeMasterGrid();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow2','dvInnerWindow1',0);", true);
    }
    protected void gvSize_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int SizeID = 0;
            string SizeName = string.Empty;
            string SizeBarCode = string.Empty;
            if (e.CommandName.Equals("Add"))
            {
                int retVal = 0;
                TextBox txtControl;

                txtControl = ((TextBox)gvSize.FooterRow.FindControl("txtSizeName"));
                if (txtControl.Text != null)
                {
                    SizeName = txtControl.Text.Trim();
                }

                txtControl = ((TextBox)gvSize.FooterRow.FindControl("txtSizeBarCode"));
                if (txtControl.Text != null)
                {
                    SizeBarCode = txtControl.Text.Trim();
                }
                int mintReturn = new BLL.Component.SizeBLL().InsertSize(SizeName,SizeBarCode);
                if (mintReturn == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Size with same name already exists');", true);
                }
                else
                {
                    //          ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Size created successfully');", true);
                    BindSizeMasterGrid();
                    PopulateSize();
                }
            }
            if (e.CommandName.Equals("AddEmpty"))
            {
                int retVal = 0;
                TextBox txtControl;

                GridViewRow emptyRow = gvSize.Controls[0].Controls[0] as GridViewRow;

                txtControl = (TextBox)emptyRow.FindControl("txtSizeName1");

                if (txtControl.Text != null)
                {
                    SizeName = txtControl.Text.Trim();
                }

                txtControl = (TextBox)emptyRow.FindControl("txtSizeBarCode1");

                if (txtControl.Text != null)
                {
                    SizeBarCode = txtControl.Text.Trim();
                }

                int mintReturn = new BLL.Component.SizeBLL().InsertSize(SizeName,SizeBarCode);
                if (mintReturn == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Size with same name already exists');", true);
                }
                else
                {
                    //          ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Size created successfully');", true);
                    BindSizeMasterGrid();
                    PopulateSize();
                }
            }
        }
        catch (Exception ex)
        {

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow2','dvInnerWindow1',0);", true);
    }
    protected void gvSize_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvSize_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        int SizeID = Convert.ToInt32(gvSize.DataKeys[e.RowIndex].Values[0].ToString());

        int mintReturn =new BLL.Component.SizeBLL().DeleteSize(SizeID);

        if (mintReturn == -1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cannot delete as it is associated with a Product');", true);
        }

        BindSizeMasterGrid();
        PopulateSize();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow2','dvInnerWindow1',0);", true);

    }
    protected void gvSize_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSize.EditIndex = e.NewEditIndex;
        BindSizeMasterGrid();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow2','dvInnerWindow1',0);", true);
    }
    protected void gvSize_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int SizeID = 0;
            string SizeName = string.Empty;
            string SizeBarCode = string.Empty;
            TextBox txtControl;

            txtControl = ((TextBox)gvSize.Rows[e.RowIndex].FindControl("txtSizeIDE"));
            if (txtControl.Text != null)
            {
                SizeID = Convert.ToInt32(txtControl.Text.Trim());
            }

            txtControl = ((TextBox)gvSize.Rows[e.RowIndex].FindControl("txtSizeNameE"));
            if (txtControl != null)
            {
                SizeName = txtControl.Text.Trim();

            }

            txtControl = ((TextBox)gvSize.Rows[e.RowIndex].FindControl("txtSizeBarCodeE"));
            if (txtControl != null)
            {
                SizeBarCode = txtControl.Text.Trim();

            }
            int mintReturn = new BLL.Component.SizeBLL().UpdateSize(SizeID, SizeName,SizeBarCode);
            if (mintReturn == -1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Size with same name already exists');", true);
            }
        }
        catch (Exception ex)
        {
        }

        // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated successfully');", true);
        gvSize.EditIndex = -1;
        BindSizeMasterGrid();
        PopulateSize();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow2','dvInnerWindow1',0);", true);
    }

    private void BindSizeMasterGrid()
    {
        List<Size> lstSize = new SizeBLL().GetSize(0);
        gvSize.DataSource = lstSize;
        gvSize.DataBind();
    }

    private void BindCategory()
    {
        List<Category> lstCategory = new CategoryBLL().GetCategory(0);
        grvCategory.DataSource = lstCategory;
        grvCategory.DataBind();
    }

    private void BindBrand()
    {
        List<Brand> lstBrand = new BrandBLL().GetBrand();
        grvBrand.DataSource = lstBrand;
        grvBrand.DataBind();
    }

    protected void grvCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvCategory.EditIndex = e.NewEditIndex;
        BindCategory();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow3','dvInnerWindow2',0);", true);
    }
    protected void grvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvCategory.PageIndex = e.NewPageIndex;
            BindCategory();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
    protected void grvCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grvCategory.EditIndex = -1;

        BindCategory();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow3','dvInnerWindow2',0);", true);
    }
    protected void grvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int CategoryID = 0;
            string CategoryName = string.Empty;
            if (e.CommandName.Equals("Add"))
            {
                int retVal = 0;
                TextBox txtControl;

                txtControl = ((TextBox)grvCategory.FooterRow.FindControl("txtCategoryName"));
                if (txtControl.Text != null)
                {
                    CategoryName = txtControl.Text.Trim();
                }

                retVal = new BLL.Component.CategoryBLL().AddEditCategory(0, CategoryName);
                if (retVal == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Category with same name already exists');", true);
                }
                BindCategory();
                PopulateCategory();
            }
            if (e.CommandName.Equals("AddEmpty"))
            {
                int retVal = 0;
                TextBox txtControl;

                GridViewRow emptyRow = grvCategory.Controls[0].Controls[0] as GridViewRow;



                txtControl = (TextBox)emptyRow.FindControl("txtCategoryName1");


                if (txtControl.Text != null)
                {
                    CategoryName = txtControl.Text.Trim();
                }

                retVal = new BLL.Component.CategoryBLL().AddEditCategory(0, CategoryName);
                if (retVal == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Category with same name already exists');", true);
                }
                BindCategory();
                PopulateCategory();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow3','dvInnerWindow2',0);", true);
        }
        catch (Exception ex)
        {

        }
    }
    protected void grvCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int CategoryID = Convert.ToInt32(grvCategory.DataKeys[e.RowIndex].Values[0].ToString());

        int mintReturn  = new BLL.Component.CategoryBLL().DeleteCategory(CategoryID);

        if (mintReturn == -1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cannot delete. Category is associated to a Product.');", true);
        }
        else
        {
            BindCategory();
            PopulateCategory();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow3','dvInnerWindow2',0);", true);
    }
    protected void grvCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int CategoryID = 0;
            string CategoryName = string.Empty;
            TextBox txtControl;

            txtControl = ((TextBox)grvCategory.Rows[e.RowIndex].FindControl("txtCategoryIDE"));
            if (txtControl.Text != null)
            {
                CategoryID = Convert.ToInt32(txtControl.Text.Trim());
            }

            txtControl = ((TextBox)grvCategory.Rows[e.RowIndex].FindControl("txtCategoryNameE"));
            if (txtControl != null)
            {
                CategoryName = txtControl.Text.Trim();

            }

            int mintReturn = new BLL.Component.CategoryBLL().AddEditCategory(CategoryID, CategoryName);
            if (mintReturn == -1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Category with same name already exists.');", true);
            }
        }
        catch (Exception ex)
        {
        }

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated successfully');", true);
        grvCategory.EditIndex = -1;
        BindCategory();
        PopulateCategory();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow3','dvInnerWindow2',0);", true);
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product.aspx");
    }
    protected void grvBrand_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvBrand.EditIndex = e.NewEditIndex;
        BindBrand();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow4','dvInnerWindow3',0);", true);
    }
    protected void grvBrand_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grvBrand.EditIndex = -1;
        BindBrand();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow4','dvInnerWindow3',0);", true);
    }
    protected void grvBrand_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grvBrand_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int BrandID = 0;
            string BrandName = string.Empty;
            if (e.CommandName.Equals("Add"))
            {
                int retVal = 0;
                TextBox txtControl;

                txtControl = ((TextBox)grvBrand.FooterRow.FindControl("txtBrand"));
                if (txtControl.Text != null)
                {
                    BrandName = txtControl.Text.Trim();
                }

                retVal = new BLL.Component.BrandBLL().AddEditBrand(-1, BrandName);
                if (retVal == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Brand with same name already exists');", true);
                }
                BindBrand();
                PopulateBrand();
            }
            if (e.CommandName.Equals("AddEmpty"))
            {
                int retVal = 0;
                TextBox txtControl;

                GridViewRow emptyRow = grvBrand.Controls[0].Controls[0] as GridViewRow;

                txtControl = (TextBox)emptyRow.FindControl("txtBrand1");


                if (txtControl.Text != null)
                {
                    BrandName = txtControl.Text.Trim();
                }

                retVal = new BLL.Component.BrandBLL().AddEditBrand(-1, BrandName);
                if (retVal == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Brand with same name already exists');", true);
                }
                BindBrand();
                PopulateBrand();
            }
        }
        catch (Exception ex)
        {

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow4','dvInnerWindow3',0);", true);
    }
    protected void grvBrand_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int BrandID = Convert.ToInt32(grvBrand.DataKeys[e.RowIndex].Values[0].ToString());

        int mintReturn = new BLL.Component.BrandBLL().DeleteBrand(BrandID);

        if (mintReturn == -1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cannot delete. Brand is associated to a Product.');", true);
        }
        else
        {
            BindBrand();
            PopulateBrand();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow4','dvInnerWindow3',0);", true);
    }
    protected void grvBrand_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int BrandID = 0;
            string BrandName = string.Empty;
            TextBox txtControl;

            txtControl = ((TextBox)grvBrand.Rows[e.RowIndex].FindControl("txtBrandIDE"));
            if (txtControl.Text != null)
            {
                BrandID = Convert.ToInt32(txtControl.Text.Trim());
            }

            txtControl = ((TextBox)grvBrand.Rows[e.RowIndex].FindControl("txtBrandE"));
            if (txtControl != null)
            {
                BrandName = txtControl.Text.Trim();

            }

            int mintReturn = new BLL.Component.BrandBLL().AddEditBrand(BrandID, BrandName);
            if (mintReturn == -1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Brand with same name already exists.');", true);
            }
        }
        catch (Exception ex)
        {
        }

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated successfully');", true);
        grvBrand.EditIndex = -1;
        BindBrand();
        PopulateBrand();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow4','dvInnerWindow3',0);", true);
    }

    private void BindSeason()
    {
        List<Season> lstSeason = new SeasonBLL().GetSeason();
        grvSeason.DataSource = lstSeason;
        grvSeason.DataBind();
    }

    private void PopulateSeason()
    {
        List<Season> lstSeason = new SeasonBLL().GetSeason();
        ddlSeason.DataSource = lstSeason;
        ddlSeason.DataValueField = "SeasonID";
        ddlSeason.DataTextField = "SeasonName";
        ddlSeason.DataBind();
        ddlSeason.Items.Insert(0, new ListItem("--Select--"));
        ddlSeason.SelectedIndex = 0;
    }
    protected void grvSeason_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvSeason.PageIndex = e.NewPageIndex;
            BindSeason();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow5','dvInnerWindow4',0);", true);
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    protected void grvSeason_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvSeason.EditIndex = e.NewEditIndex;
        BindSeason();
        PopulateSeason();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow5','dvInnerWindow4',0);", true);
    }
    protected void grvSeason_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grvSeason.EditIndex = -1;
        BindSeason();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow5','dvInnerWindow4',0);", true);
    }
    protected void grvSeason_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grvSeason_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int SeasonID = 0;
            string SeasonName = string.Empty;
            if (e.CommandName.Equals("Add"))
            {
                int retVal = 0;
                TextBox txtControl;

                txtControl = ((TextBox)grvSeason.FooterRow.FindControl("txtSeason"));
                if (txtControl.Text != null)
                {
                    SeasonName = txtControl.Text.Trim();
                }

                retVal = new BLL.Component.SeasonBLL().AddEditSeason(-1, SeasonName);
                if (retVal == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Season with same name already exists');", true);
                }
                BindSeason();

            }

            if (e.CommandName.Equals("AddEmpty"))
            {
                int retVal = 0;
                TextBox txtControl;

                GridViewRow emptyRow = grvSeason.Controls[0].Controls[0] as GridViewRow;


                txtControl = (TextBox)emptyRow.FindControl("txtSeason1");
                if (txtControl.Text != null)
                {
                    SeasonName = txtControl.Text.Trim();
                }

                retVal = new BLL.Component.SeasonBLL().AddEditSeason(-1, SeasonName);
                if (retVal == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Season with same name already exists');", true);
                }
                BindSeason();

            }
            PopulateSeason();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow5','dvInnerWindow4',0);", true);
        }
        catch (Exception ex)
        {

        }

    }
    protected void grvSeason_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int SeasonID = Convert.ToInt32(grvSeason.DataKeys[e.RowIndex].Values[0].ToString());
        int mintReturn = new BLL.Component.SeasonBLL().DeleteSeason(SeasonID);
        if (mintReturn == -1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cannot delete.Season is associeted to Product');", true);
        }
        else
        {
            BindSeason();
            PopulateSeason();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow5','dvInnerWindow4',0);", true);
    }
    protected void grvSeason_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int SeasonID = 0;
            string SeasonName = string.Empty;
            TextBox txtControl;

            txtControl = ((TextBox)grvSeason.Rows[e.RowIndex].FindControl("txtSeasonIDE"));
            if (txtControl.Text != null)
            {
                SeasonID = Convert.ToInt32(txtControl.Text.Trim());
            }

            txtControl = ((TextBox)grvSeason.Rows[e.RowIndex].FindControl("txtSeasonE"));
            if (txtControl != null)
            {
                SeasonName = txtControl.Text.Trim();

            }

            int mintReturn = new BLL.Component.SeasonBLL().AddEditSeason(SeasonID, SeasonName);
            if (mintReturn == -1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Season with same name already exists.');", true);
            }
        }
        catch (Exception ex)
        {
        }

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated successfully');", true);
        grvSeason.EditIndex = -1;
        BindSeason();
        PopulateSeason();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowModalDiv('ModalWindow5','dvInnerWindow4',0);", true);
    }
    protected void grvBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grvBrand.PageIndex = e.NewPageIndex;
            BindBrand();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
}