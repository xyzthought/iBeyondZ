using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.BusinessObject;
using BLL.Component;
using CSWeb.Utility;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using CSWeb.Utility;
using System;
using System.Collections.Generic;

public partial class Modules_Manufacturer : PageBase
{   
    PageInfo objPI = new PageInfo();
    string vstrLink = string.Empty;
    string param = string.Empty;
    public const string DEFAULTCOLUMNNAME = "CompanyName";
    protected bool iFlag = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objPI.SortColumnName = DEFAULTCOLUMNNAME;
            objPI.SortDirection = Constants.DESC;
            ViewState[Constants.SORTCOLUMNNAME] = DEFAULTCOLUMNNAME;
            ViewState[Constants.SORTDERECTION] = Constants.DESC;
             PopulateManufacturerGrid();
        }

    }

    #region PopulateManufacturerGrid
    private void PopulateManufacturerGrid()
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

            List<Manufacturer> objData = new ManufacturerBLL().GetAll(objPI);

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
            PopulateManufacturerGrid();
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
            PopulateManufacturerGrid();
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
                lnkDelete.OnClientClick = "return confirm('Manufacturer :" + BLL.BusinessObject.Constants.DeleteConf + "');";

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
                int intManufacturerID = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["intManufacturerID"] = intManufacturerID;
                LoadData(intManufacturerID);
            }

            if (e.CommandName == "Delete")
            {

                Manufacturer objManufacturer = new Manufacturer();
                new ManufacturerBLL().Delete(ref objManufacturer);

                if (objManufacturer.ReturnStatus > 0)
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
                    lblMsg.Text = objManufacturer.ReturnMessage;
                }
            }



        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }




    private void LoadData(int vintManufacturerID)
    {
        try
        {
            Manufacturer objManufacturer = new Manufacturer();
            objManufacturer.ManufacturerID = vintManufacturerID;

            new ManufacturerBLL().GetByID(ref objManufacturer);

            if (null != objManufacturer.CompanyName)
            {
                txtCompanyName.Text = objManufacturer.CompanyName;
                txtContactFirstName.Text = objManufacturer.ContactFirstName;
                txtContactLastName.Text = objManufacturer.ContactLastName;
                txtAddress.Text = objManufacturer.Address;
                txtZIP.Text = objManufacturer.ZIP;
                txtCity.Text = objManufacturer.City;
                txtCountry.Text = objManufacturer.Country;
                txtPhone.Text = objManufacturer.Phone;
                txtEmailID.Text = objManufacturer.Email;
                chkIsActive.Checked = objManufacturer.IsActive;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "AddEditManufacturer", "ShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);

            }
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }

    }

    protected void gvGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PopulateManufacturerGrid();

    }

    protected void gvGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gvGrid.EditIndex = e.NewEditIndex;
    }

    #endregion

    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {
        PopulateManufacturerGrid();
    }

    protected void lnkBtnSaveDS_Click(object sender, EventArgs e)
    {

        if (null != ViewState["intManufacturerID"] && Convert.ToInt32(ViewState["intManufacturerID"].ToString()) > 0)
        {
            SaveData(Constants.MODE_EDIT, Convert.ToInt32(ViewState["intManufacturerID"].ToString()));
        }
        else
        {
            SaveData(Constants.MODE_ADD, 0);
        }
       
    }

    #region Save Data
    private void SaveData(string vstrMode, int vintManufacturerID)
    {
        Manufacturer objManufacturer = new Manufacturer();
        objManufacturer.ManufacturerID = vintManufacturerID;
        objManufacturer.CompanyName = txtCompanyName.Text.Trim();
        objManufacturer.ContactFirstName = txtContactFirstName.Text.Trim();
        objManufacturer.ContactLastName = txtContactLastName.Text.Trim();
        objManufacturer.Address = txtAddress.Text.Trim();
        objManufacturer.ZIP = txtAddress.Text.Trim();
        objManufacturer.City = txtCity.Text.Trim();
        objManufacturer.Country = txtCountry.Text.Trim();
        objManufacturer.Phone = txtPhone.Text.Trim();
        objManufacturer.Email = txtEmailID.Text.Trim();
        objManufacturer.IsActive = chkIsActive.Checked;

        User objUser = (User)Session["UserData"];

        if (vstrMode.Equals(Constants.MODE_EDIT))
        {
            objManufacturer.UpdatedBy = objUser.UserID;
            new ManufacturerBLL().Update(ref objManufacturer);
        }
        else
        {
            objManufacturer.CreatedBy = objUser.UserID;
            new ManufacturerBLL().Add(ref objManufacturer);
        }

        lblError.InnerHtml = objManufacturer.ReturnMessage;
        if (objManufacturer.ReturnValue > 0)
        {
            divMess.Visible = true;
            lblMsg.Text = objManufacturer.ReturnMessage;
            ViewState["intManufacturerID"] = null;
            PopulateManufacturerGrid();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "AddEditManufacturer", "ShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);
        }
    }
    #endregion
}