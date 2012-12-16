using System;
using System.Collections.Generic;
using System.Linq;
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

public partial class Modules_ManageCustomer : System.Web.UI.Page
{
    PageInfo objPI = new PageInfo();
    string vstrLink = string.Empty;
    string param = string.Empty;
    public const string DEFAULTCOLUMNNAME = "CreatedOn";
    protected bool iFlag = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objPI.SortColumnName = DEFAULTCOLUMNNAME;
            objPI.SortDirection = Constants.DESC;
            ViewState[Constants.SORTCOLUMNNAME] = DEFAULTCOLUMNNAME;
            ViewState[Constants.SORTDERECTION] = Constants.DESC;
           
            PopulateGrid();
        }
    }




    #region Populate Grid
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
            CustomerBLL objCustomerBLL = new CustomerBLL();

            List<Customer> objData = new List<Customer>();
            objData = objCustomerBLL.GetAllCustomer(objData, objPI);

            gvGrid.DataSource = objData;
            gvGrid.ExportTemplate = "export_template_4Column.xlsx";
            gvGrid.ExportCaption = "";
            gvGrid.ExcelColumn = "";
            gvGrid.DataBind();

            if (objData != null)
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

            }

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

                /*param = Constants.MODE + "=" + Constants.MODE_EDIT + "&" + Constants.ID + "=" + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CustomerID"));
                param = Common.GenerateBASE64WithObfuscateApp(param);
                vstrLink = "AddEditCustomer?q=" + param;
                HtmlControl aEdit = (HtmlControl)e.Row.FindControl("aEdit");
                aEdit.Attributes.Add("on", vstrLink);*/


                LinkButton lnkDelete = new LinkButton();
                lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                lnkDelete.OnClientClick = "return confirm('Customer :" + BLL.BusinessObject.Constants.DeleteConf + "');";

                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CustomerID")) == ((Customer)Session["CustomerData"]).CustomerID)
                    lnkDelete.Visible = false;


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
                int intCustomerID = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["intCustomerID"] = intCustomerID;
                LoadData(intCustomerID);
            }

            if (e.CommandName == "Delete")
            {

                Message vobjMsg = new Message();
                int intCustomerID = Convert.ToInt32(e.CommandArgument.ToString());
                Customer objCustomer = new Customer();
                CustomerBLL objUBLL = new CustomerBLL();
                objCustomer.CustomerID = intCustomerID;
                vobjMsg = objUBLL.DeletePlatformCustomer(objCustomer);

                if (vobjMsg.ReturnValue > 0)
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
                    lblMsg.Text = vobjMsg.ReturnMessage;
                }
            }



        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }




    private void LoadData(int vintCustomerID)
    {
        try
        {
            Customer objCustomer = new Customer();
            CustomerBLL objUBLL = new CustomerBLL();
            objCustomer.CustomerID = vintCustomerID;
            List<Customer> objList = objUBLL.GetCustomerDetailByCustID(ref objCustomer);

            if (null != objList)
            {
                txtFirstName.Text = objList[0].FirstName;
                txtLastName.Text = objList[0].LastName;
                txtAddress.Text = objList[0].Address;
                txtZIP.Text = objList[0].ZIP;
                txtCity.Text = objList[0].City;
                txtCountry.Text = objList[0].Country;
                txtPhone.Text = objList[0].TeleNumber;
                txtEmailID.Text = objList[0].Email;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AddEditCustomer", "ShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);

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
        if (null != ViewState["intCustomerID"] && Convert.ToInt32(ViewState["intCustomerID"].ToString()) > 0)
        {
            SaveData(Constants.MODE_EDIT, Convert.ToInt32(ViewState["intCustomerID"].ToString()));
        }
        else
        {
            SaveData(Constants.MODE_ADD, 0);
        }
    }


    #region Save Data
    private void SaveData(string vstrMode, int vintCustomerID)
    {
        Customer objCustomer = new Customer();
        CustomerBLL objUBLL = new CustomerBLL();
        objCustomer.CustomerID = vintCustomerID;
        
        objCustomer.FirstName = txtFirstName.Text.Trim();
        objCustomer.LastName = txtLastName.Text.Trim();
        objCustomer.Address = txtAddress.Text.Trim();
        objCustomer.ZIP = txtZIP.Text.Trim();
        objCustomer.City = txtCity.Text.Trim();
        objCustomer.Country = txtCountry.Text.Trim();
        objCustomer.TeleNumber = txtPhone.Text.Trim();
        objCustomer.Email = txtEmailID.Text.Trim();
        objCustomer.CreatedBy = ((User)Session["UserData"]).UserID;
        Message objMsg = objUBLL.InsertUpdatePlatformCustomer(objCustomer);

        lblError.InnerHtml = objMsg.ReturnMessage;
        if (objMsg.ReturnValue > 0)
        {
            divMess.Visible = true;
            lblMsg.Text = objMsg.ReturnMessage;
            ViewState["intCustomerID"] = null;
            PopulateGrid();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "AddEditCustomer", "ShowModalDiv('ModalWindow1','dvInnerWindow',0);", true);
        }
    }
    #endregion
}