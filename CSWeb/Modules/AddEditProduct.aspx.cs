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
            PopulateManufacturer();
            PopulateSize();
            PopulateCategory();
            if (Request.QueryString["ProductID"] != null)
            {
                LoadData(int.Parse(Request.QueryString["ProductID"]));
            }

            BindSizeMasterGrid();
            BindCategory();
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product for the Manufacturer already exists!');", true);
                    return;
                }
            }
            else
            {
                mobjProduct.UpdatedBy = 1;
                int mintReturn = objProd.EditProduct(mobjProduct);
                if (mintReturn == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product for the Manufacturer already exists!');", true);
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
            objProduct.ManufacturerID = int.Parse(cmbManufacturer.SelectedValue);
            objProduct.CategoryID = int.Parse(cmbCategory.SelectedValue);
            objProduct.SizeID = GetSizeIDs();// chkSize.SelectedValue == "" ? 0 : int.Parse(chkSize.SelectedValue);
            objProduct.BuyingPrice = Convert.ToDecimal(txtBuyingPrice.Text);
            objProduct.Tax = Convert.ToDecimal(txtTax.Text);
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
                cmbManufacturer.SelectedValue = objProduct.ManufacturerID.ToString();
                cmbCategory.SelectedValue = objProduct.CategoryID.ToString();

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

                txtBuyingPrice.Text = objProduct.BuyingPrice.ToString();
                txtTax.Text = objProduct.Tax.ToString();
                txtMargin.Text = objProduct.Margin.ToString();
                txtSellingPrice.Text = objProduct.SellingPrice.ToString();
                txtBarcode.Text = objProduct.BarCode;

                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "AddEditProduct", "ShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);

            }
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
    }
    protected void gvSize_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int SizeID = 0;
            string SizeName = string.Empty;
            if (e.CommandName.Equals("Add"))
            {
                int retVal = 0;
                TextBox txtControl;

                txtControl = ((TextBox)gvSize.FooterRow.FindControl("txtSizeName"));
                if (txtControl.Text != null)
                {
                    SizeName = txtControl.Text.Trim();
                }

                int mintReturn = new BLL.Component.SizeBLL().InsertSize(SizeName);
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
    }
    protected void gvSize_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvSize_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        int SizeID = Convert.ToInt32(gvSize.DataKeys[e.RowIndex].Values[0].ToString());

        new BLL.Component.SizeBLL().DeleteSize(SizeID);


        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully');", true);

        BindSizeMasterGrid();
        PopulateSize();

    }
    protected void gvSize_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSize.EditIndex = e.NewEditIndex;
        BindSizeMasterGrid();
    }
    protected void gvSize_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int SizeID = 0;
            string SizeName = string.Empty;
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

            int mintReturn = new BLL.Component.SizeBLL().UpdateSize(SizeID, SizeName);
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
    protected void grvCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvCategory.EditIndex = e.NewEditIndex;
        BindCategory();
    }
    protected void grvCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grvCategory.EditIndex = -1;

        BindCategory();
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

        }
        catch (Exception ex)
        {

        }
    }
    protected void grvCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int CategoryID = Convert.ToInt32(grvCategory.DataKeys[e.RowIndex].Values[0].ToString());

        new BLL.Component.CategoryBLL().DeleteCategory(CategoryID);


        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record deleted successfully');", true);

        BindCategory();
        PopulateCategory();
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
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product.aspx");
    }
}