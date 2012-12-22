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
using System.Web.UI.HtmlControls;

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
                objPI.SortDirection = Constants.ASC;
                ViewState[Constants.SORTCOLUMNNAME] = DEFAULTCOLUMNNAME;
                ViewState[Constants.SORTDERECTION] = Constants.DESC;


                BindProduct();

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



    private void BindProduct()
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



    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Product.aspx");
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
            if (e.CommandName == "Edit")
            {
                Response.Redirect("AddEditProduct.aspx?ProductID="+e.CommandArgument.ToString());
            }
            /*
            Product objProd = new Product();


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
                    objProd.Margin = Convert.ToDecimal(txtControl.Text);
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
            if (e.CommandName.Equals("AddEmpty"))
            {

                TextBox txtControl;

                GridViewRow emptyRow = gvGrid.Controls[0].Controls[0] as GridViewRow;

                //txtControl = (TextBox)emptyRow.FindControl("txtSizeName1");

                txtControl = (TextBox)emptyRow.FindControl("txtProductName1");
                if (txtControl.Text != null)
                {
                    objProd.ProductName = txtControl.Text.Trim();
                }
                txtControl = (TextBox)emptyRow.FindControl("txtDescription1");
                if (txtControl.Text != null)
                {
                    objProd.Description = txtControl.Text.Trim();
                }
                txtControl = (TextBox)emptyRow.FindControl("txtMargin1");
                if (txtControl.Text != null)
                {
                    objProd.Margin = Convert.ToDecimal(txtControl.Text);
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
            */
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
                objProd.Margin = Convert.ToDecimal(txtControl.Text.Trim());

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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lbl = (Label)e.Row.FindControl("lblQuantity");
            HtmlGenericControl divQty = (HtmlGenericControl)e.Row.FindControl("dvQtyDetails");
            //lbl.Attributes.Add("onmouseover", "show('" + divQty.ClientID + "')");
            //lbl.Attributes.Add("onmouseout", "hide('" + divQty.ClientID + "')");

            Label lblStock = (Label)e.Row.FindControl("lblStock");
            HtmlGenericControl divStock = (HtmlGenericControl)e.Row.FindControl("dvStockDetails");
            //lbl.Attributes.Add("onmouseover", "ShowModalDiv('" + divStock.ClientID + "')");
            //lbl.Attributes.Add("onmouseout", "CloseAddDiv('" + divStock.ClientID + "')");

            e.Row.Cells[5].ToolTip = divQty.InnerHtml.Trim();
            e.Row.Cells[6].ToolTip = divStock.InnerHtml.Trim();
        }
    }





    protected void gvGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvGrid.PageIndex = e.NewPageIndex;
            BindProduct();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
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
            BindProduct();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEditProduct.aspx");
    }

    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {
        BindProduct();
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          