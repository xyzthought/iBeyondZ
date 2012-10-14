using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.Component;
using CSWeb.Utility;
using BLL.BusinessObject;
using System.Diagnostics;

public partial class Modules_Product : System.Web.UI.Page
{
    PageInfo objPI = new PageInfo();
    public const string DEFAULTCOLUMNNAME = "ProductName";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                objPI.SortColumnName = DEFAULTCOLUMNNAME;
                objPI.SortDirection = Constants.DESC;
                ViewState[Constants.SORTCOLUMNNAME] = DEFAULTCOLUMNNAME;
                ViewState[Constants.SORTDERECTION] = Constants.DESC;
                
                BindSizeMasterGrid();
                BindCategory();
                BindBrand();
                BindProduct();
                BindSeason();
            }
            catch (Exception ex)
            {
                SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }

        }
    }

    protected void lnkAddNew2_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditProduct.aspx");
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

        int mintReturn = new BLL.Component.SizeBLL().DeleteSize(SizeID);

        if (mintReturn == -1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cannot delete as it is associated with a Product');", true);
        }

        BindSizeMasterGrid();
        

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
    private void BindSeason()
    {
        List<Season> lstSeason = new SeasonBLL().GetSeason();
        grvSeason.DataSource = lstSeason;
        grvSeason.DataBind();
    }
    private void BindProduct()
    {
        try
        {

            if (objPI.SortDirection == null && objPI.SortColumnName == null)
            {
                objPI.SortDirection = Convert.ToString(ViewState[Constants.SORTDERECTION]);
                objPI.SortColumnName = Convert.ToString(ViewState[Constants.SORTCOLUMNNAME]);
            }
            //List<Manufacturer> objData = new ManufacturerBLL().GetAll(objPI);

            BLL.Component.ProductBLL objprod = new BLL.Component.ProductBLL();
            gvGrid.DataSource = objprod.GetAllProducts(objPI);
            gvGrid.DataBind();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
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
        BindCategory();
       
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

        grvCategory.EditIndex = -1;
        BindCategory();
       
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product.aspx");
    }
    protected void grvBrand_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvBrand.EditIndex = e.NewEditIndex;
        BindBrand();
       
    }
    protected void grvBrand_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grvBrand.EditIndex = -1;
        BindBrand();
       
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
       
            }

        }
        catch (Exception ex)
        {

        }
       
    }
    protected void grvBrand_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int BrandID = Convert.ToInt32(grvBrand.DataKeys[e.RowIndex].Values[0].ToString());
        new BLL.Component.BrandBLL().DeleteBrand(BrandID);
        BindBrand();
       
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
       

    }

    protected void gvGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvGrid.EditIndex = -1;
        BindProduct();
    }
    protected void gvGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            Product objProd=new Product();


            if (e.CommandName.Equals("Add"))
            {
              
                TextBox txtControl;

                txtControl = ((TextBox)gvGrid.FooterRow.FindControl("txtProductName"));
                if (txtControl.Text != null)
                {
                    objProd.ProductName = txtControl.Text.Trim();
                }
                txtControl = ((TextBox)gvGrid.FooterRow.FindControl("txtDescription"));
                if (txtControl.Text != null)
                {
                    objProd.Description = txtControl.Text.Trim();
                }
                txtControl = ((TextBox)gvGrid.FooterRow.FindControl("txtMargin"));
                if (txtControl.Text != null)
                {
                    objProd.Margin =Convert.ToDecimal(txtControl.Text);
                }

                int mintReturn = new BLL.Component.ProductBLL().AddProduct(objProd);
                if (mintReturn == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product already exists');", true);
                }
                else
                {
                    BindProduct();
                }

                
            }

        }
        catch (Exception ex)
        {

        }
       
    }
    protected void gvGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intProductID = Convert.ToInt32(gvGrid.DataKeys[e.RowIndex].Values[0].ToString());
        bool blnReturn = new ProductBLL().DeleteProduct(intProductID);

        BindProduct();
    }
    protected void gvGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvGrid.EditIndex = e.NewEditIndex;
        BindProduct();
    }
    protected void gvGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Product objProd = new Product();
            TextBox txtControl;

            txtControl = ((TextBox)gvGrid.Rows[e.RowIndex].FindControl("txtProductIDE"));
            if (txtControl.Text != null)
            {
                objProd.ProductID = Convert.ToInt32(txtControl.Text.Trim());
            }

            txtControl = ((TextBox)gvGrid.Rows[e.RowIndex].FindControl("txtProductNameE"));
            if (txtControl != null)
            {
                objProd.ProductName = txtControl.Text.Trim();

            }

            txtControl = ((TextBox)gvGrid.Rows[e.RowIndex].FindControl("txtDescriptionE"));
            if (txtControl != null)
            {
                objProd.Description = txtControl.Text.Trim();

            }

            txtControl = ((TextBox)gvGrid.Rows[e.RowIndex].FindControl("txtMarginE"));
            if (txtControl != null)
            {
                objProd.Margin =Convert.ToDecimal(txtControl.Text.Trim());

            }
            objProd.UpdatedBy = 1;
            int mintReturn = new BLL.Component.ProductBLL().EditProduct(objProd);
            if (mintReturn == -1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Brand with same name already exists.');", true);
            }
        }
        catch (Exception ex)
        {
        }

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated successfully');", true);
        gvGrid.EditIndex = -1;
        BindProduct();
       
    }
    protected void gvGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    protected void grvSeason_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvSeason.EditIndex = e.NewEditIndex;
        BindSeason();

    }
    protected void grvSeason_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grvSeason.EditIndex = -1;
        BindSeason();

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

        }
        catch (Exception ex)
        {

        }

    }
    protected void grvSeason_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int SeasonID = Convert.ToInt32(grvSeason.DataKeys[e.RowIndex].Values[0].ToString());
        new BLL.Component.SeasonBLL().DeleteSeason(SeasonID);
        BindSeason();

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


    }

}