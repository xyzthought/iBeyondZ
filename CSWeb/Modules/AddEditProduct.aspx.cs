using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BusinessObject;
using BLL.Component;
using CSWeb.Utility;

public partial class Modules_AddEditProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString.Get("ProductID")))
            {
                PopulateProductByID(int.Parse(Request.QueryString.Get("ProductID")));
            }
        }
    }
    protected void lnkSave_Click(object sender, EventArgs e)
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
}