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

                BindGrid();
                PopulateManufacturer();
                PopulateCategory();
                PopulateSize();
            }
            catch (Exception ex)
            {
                 SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            }
           
        }
    }

    private void PopulateSize()
    {
        try
        {
            List<Size> lstSize = new SizeBLL().GetSize(0);
            chkSize.DataSource = lstSize;
            chkSize.DataMember = "SizeID";
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
            cmbCategory.DataMember = "CategoryID";
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
            cmbManufacturer.DataMember = "ManufacturerID";
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

    private void BindGrid()
    {
        try
        {
            if (txtSearch.Text.Trim() != "Search")
            {
                objPI.SearchText = txtSearch.Text.Trim();
            }
            else
            {
                objPI.SearchText = "";
            }

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

    protected void lnkBtnSaveDS_Click(object sender, EventArgs e)
    {
        try
        {
            BLL.BusinessObject.Product mobjProduct = PopulateFromForm();
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
                bool mblnReturn = objProd.AddProduct(mobjProduct);
            }
            else
            {
                objProd.EditProduct(mobjProduct);
            }
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
            objProduct.SizeID = int.Parse(chkSize.SelectedValue);
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
    protected void gvGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            divMess.Visible = false;
            lblMsg.Text = "search";
            if (Convert.ToString(e.SortDirection) == Convert.ToString(SortDirection.Ascending))
            {
                e.SortDirection = SortDirection.Descending;
                objPI.SortDirection = Constants.DESC;
                ViewState[Constants.SORTDERECTION] = Constants.DESC;
            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                objPI.SortDirection = Constants.ASC;
                ViewState[Constants.SORTDERECTION] = Constants.ASC;
            }
            //objPI.SortDirection = e.SortDirection.ToString();
            objPI.SortColumnName = e.SortExpression;
            ViewState[Constants.SORTCOLUMNNAME] = e.SortExpression;
            BindGrid();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
    protected void gvGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            divMess.Visible = false;
            lblMsg.Text = "search";
            gvGrid.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
    protected void gvGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDelete = new LinkButton();
                lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.OnClientClick = "return confirm('Manufacturer :" + BLL.BusinessObject.Constants.DeleteConf + "');";

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
                ViewState["intProductID"] = intProductID;
                LoadData(intProductID);
            }

            if (e.CommandName == "Delete")
            {

                int intProductID = Convert.ToInt32(e.CommandArgument.ToString());
                bool blnReturn = new ProductBLL().DeleteProduct(intProductID);

                if (blnReturn)
                {
                    divMess.Visible = true;
                    lblMsg.Text = Constants.Deleted;
                    divMess.Attributes.Add("class", "Deleted");
                    lblMsg.Style.Add("color", "Black");
                }
                else
                {
                    divMess.Visible = true;
                    lblMsg.Style.Add("color", "Red");
                    divMess.Attributes.Add("class", "error");
                    lblMsg.Text = "Delete failed";
                }
            }



        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    private void LoadData(int intProductID)
    {
        try
        {
            List<Product> lstProduct = new ProductBLL().GetAllProducts(intProductID);


            if (null != lstProduct && lstProduct.Count>0)
            {
                txtProductName.Text = lstProduct[0].ProductName;
                txtDescription.Text = lstProduct[0].Description;
              //  cmbManufacturer.SelectedValue = lstProduct[0].ManufacturerID.ToString();
                //cmbCategory.SelectedValue = lstProduct[0].CategoryID.ToString();

                txtBuyingPrice.Text = lstProduct[0].BuyingPrice.ToString();
                txtTax.Text = lstProduct[0].Tax.ToString();
                txtMargin.Text = lstProduct[0].Margin.ToString();
                txtSellingPrice.Text = lstProduct[0].SellingPrice.ToString();
                txtBarcode.Text = lstProduct[0].BarCode;
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AddEditProduct", "ShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);

            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
}