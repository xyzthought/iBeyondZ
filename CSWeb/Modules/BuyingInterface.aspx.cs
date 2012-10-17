using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL.BusinessObject;
using BLL.Component;
using CSWeb.Utility;

public partial class BuyingInterface : PageBase
{
    PageInfo objPI = new PageInfo();
    string vstrLink = string.Empty;
    string param = string.Empty;
    public const string DEFAULTCOLUMNNAME = "PurchaseDate";
    protected bool iFlag = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            objPI.SortColumnName = DEFAULTCOLUMNNAME;
            objPI.SortDirection = Constants.DESC;
            ViewState[Constants.SORTCOLUMNNAME] = DEFAULTCOLUMNNAME;
            ViewState[Constants.SORTDERECTION] = Constants.DESC;
            fromDate.Value = string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-30));
            toDate.Value = string.Format("{0:MM/dd/yyyy}", DateTime.Today);
            PopulateGrid();
            PopulateManufacturer();
            PopulateProduct();
            PopulateSize();
            txtSearchManufacturerId.Value = "0";
        }
    }

    #region PopulateGrid
    private void PopulateGrid()
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
            string strManufacturerName = (txtSearchManufacturer.Value.Trim()=="Manufacturer Name"? "": txtSearchManufacturer.Value.Trim());

            List<ProductPurchase> objData = new ProductPurchaseBLL().GetAll(Convert.ToDateTime(fromDate.Value), Convert.ToDateTime(toDate.Value), strManufacturerName, objPI);

            gvGrid.DataSource = objData;
            gvGrid.ExportTemplate = "export_template_4Column.xlsx";
            gvGrid.ExportCaption = "";
            gvGrid.ExcelColumn = "";
            gvGrid.DataBind();

            /*if (objData != null)
            {
                if (!iFlag && lblMsg.Text == "")
                {
                    int mintMode = Common.GetQueryStringIntValue("Mode");
                    if (mintMode > 0)
                    {
                        if (mintMode == 1)
                        {
                            divMess.Attributes.Add("class", "success");
                            divMess.Visible = true;
                            lblMsg.Text = "-'" + objData[0].FirstName + "' " + Constants.Added;

                        }
                        else if (mintMode == 2)
                        {

                        }

                    }
                    else
                    {
                        divMess.Visible = false;
                        lblMsg.Text = "";
                    }
                }
                else
                {
                    if (lblMsg.Text == "")
                    {
                        divMess.Visible = false;
                        lblMsg.Text = "";
                    }
                }

            }*/

        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }
    #endregion

    #region GRID VIEW EVENTS
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
            PopulateGrid();
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
            PopulateGrid();
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

                /*param = Constants.MODE + "=" + Constants.MODE_EDIT + "&" + Constants.ID + "=" + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UserID"));
                param = Common.GenerateBASE64WithObfuscateApp(param);
                vstrLink = "AddEditUser?q=" + param;
                HtmlControl aEdit = (HtmlControl)e.Row.FindControl("aEdit");
                aEdit.Attributes.Add("on", vstrLink);*/


                LinkButton lnkDelete = new LinkButton();
                lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.OnClientClick = "return confirm('Purchase record :" + BLL.BusinessObject.Constants.DeleteConf + "');";

                /* if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ManufacturerID")) == ((User)Session["UserData"]).UserID)
                     lnkDelete.Visible = false;*/


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
                int intPurchaseID = Convert.ToInt32(e.CommandArgument.ToString());
                param = Constants.MODE + "=" + Constants.MODE_EDIT + "&" + Constants.ID + "=" + intPurchaseID;
                param = Common.GenerateBASE64WithObfuscateApp(param);
                vstrLink = "AddEditPurchase.aspx?q=" + param;
                Response.Redirect(vstrLink, false);
                //ViewState["intPurchaseID"] = intPurchaseID;
                //LoadData(intPurchaseID);
            }

            if (e.CommandName == "Delete")
            {

                ProductPurchase objProductPurchase = new ProductPurchase();
                int intPurchaseID = Convert.ToInt32(e.CommandArgument.ToString());
                objProductPurchase.ProductPurchaseID = intPurchaseID;
                new ProductPurchaseBLL().Delete(ref objProductPurchase);

                if (objProductPurchase.ReturnStatus > 0)
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
                    lblMsg.Text = objProductPurchase.ReturnMessage;
                }
            }
            
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        param = Constants.MODE + "=" + Constants.MODE_ADD + "&" + Constants.ID + "=0";
        param = Common.GenerateBASE64WithObfuscateApp(param);
        vstrLink = "AddEditPurchase.aspx?q=" + param;
        Response.Redirect(vstrLink, false);
    }

    private void PopulateSize()
    {
        Common.BindControl(ddlSize, new SizeBLL().GetSize(0), "SizeName", "SizeID", Constants.ControlType.DropDownList, true);
    }

    private void PopulateManufacturer()
    {
        List<Manufacturer> lstManufacturer = new ManufacturerBLL().GetAllActive();
        hdnManufacturer.Value = "";
        for (int i = 0; i < lstManufacturer.Count; i++)
        {
            hdnManufacturer.Value += lstManufacturer[i].ManufacturerID + "##" + lstManufacturer[i].CompanyName + "@@";
        }

        if (hdnManufacturer.Value.Length > 0)
        {
            hdnManufacturer.Value = hdnManufacturer.Value.Substring(0, hdnManufacturer.Value.Length - 2);
        }

        Common.BindControl(ddlManufacturer, lstManufacturer, "CompanyName", "ManufacturerID", Constants.ControlType.DropDownList, true);
    }

    public void PopulateProduct()
    {
        Common.BindControl(ddlProduct, new ProductBLL().GetAllActiveProduct(), "ProductName", "ProductID", Constants.ControlType.DropDownList, true);
    }


    private void LoadData(int vintPurchaseID)
    {
        try
        {
            ProductPurchase objProductPurchase = new ProductPurchase();
            objProductPurchase.ProductPurchaseID = vintPurchaseID;

            new ProductPurchaseBLL().GetByID(ref objProductPurchase);

            if (0 != objProductPurchase.ProductID)
            {
                ddlProduct.SelectedValue = objProductPurchase.ProductID.ToString();
                ddlManufacturer.SelectedValue = objProductPurchase.ManufacturerID.ToString();
                //ddlSize.SelectedValue = objProductPurchase.SizeID.ToString();
                txtQuantity.Text = objProductPurchase.Quantity.ToString();
                //txtPrice.Text = objProductPurchase.Price.ToString("#.##");
                txtDateOfPurchase.Value = objProductPurchase.PurchaseDate.ToShortDateString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "AddEditProductPurchase", "ChangeDatePicker();ShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);

            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }

    }

    protected void gvGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PopulateGrid();

    }

    protected void gvGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gvGrid.EditIndex = e.NewEditIndex;
    }

    #endregion

    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {
        PopulateGrid();
    }

    protected void lnkBtnSaveDS_Click(object sender, EventArgs e)
    {

        if (null != ViewState["intPurchaseID"] && Convert.ToInt32(ViewState["intPurchaseID"].ToString()) > 0)
        {
            SaveData(Constants.MODE_EDIT, Convert.ToInt32(ViewState["intPurchaseID"].ToString()));
        }
        else
        {
            SaveData(Constants.MODE_ADD, 0);
        }

    }

    #region Save Data
    private void SaveData(string vstrMode, int vintProductPurchaseID)
    {
        /*ProductPurchase objProductPurchase = new ProductPurchase();
        objProductPurchase.ProductPurchaseID = vintProductPurchaseID;
        objProductPurchase.ManufacturerID = Convert.ToInt32(ddlManufacturer.SelectedItem.Value);
        objProductPurchase.ProductID = Convert.ToInt32(ddlProduct.SelectedItem.Value);
        objProductPurchase.SizeID = Convert.ToInt32(ddlSize.SelectedItem.Value);
        objProductPurchase.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
        objProductPurchase.Price = Convert.ToDecimal(txtPrice.Text.Trim());
        objProductPurchase.PurchaseDate = Convert.ToDateTime(txtDateOfPurchase.Value);

        User objUser = (User)Session["UserData"];

        if (vstrMode.Equals(Constants.MODE_EDIT))
        {
            new ProductPurchaseBLL().Update(ref objProductPurchase);
        }
        else
        {
            new ProductPurchaseBLL().Add(ref objProductPurchase);
        }

        lblError.InnerHtml = objProductPurchase.ReturnMessage;
        if (objProductPurchase.ReturnValue > 0)
        {
            divMess.Visible = true;
            lblMsg.Text = objProductPurchase.ReturnMessage;
            ViewState["intManufacturerID"] = null;
            PopulateGrid();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "AddEditProductPurchase", "objProductPurchaseShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);
        }*/
    }
    #endregion

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        PopulateGrid();
    }
}