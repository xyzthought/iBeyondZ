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
using System.Data;


public partial class Modules_AddEditSaleOrder : PageBase
{
    string vstrLink = string.Empty;
    string param = string.Empty;

    #region Protected Properties
    protected string SelectedMode
    {
        get
        {
            if (ViewState["SelectedMode"] != null)
                return Convert.ToString(ViewState["SelectedMode"]);
            else
                return string.Empty;
        }
        set
        {
            ViewState["SelectedMode"] = value;
        }
    }
    protected Int32 SaleID
    {
        get
        {
            if (ViewState["SaleID"] != null)
                return Convert.ToInt32(ViewState["SaleID"]);
            else
                return 0;
        }
        set
        {
            ViewState["SaleID"] = value;
        }
    }

    protected DataTable dtProductDetail
    {
        get
        {
            if (ViewState["dtProductDetail"] != null)
                return (DataTable)ViewState["dtProductDetail"];
            else
                return null;
        }
        set
        {
            ViewState["dtProductDetail"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (null == Session["dtProductDetail"])
            {
                dtProductDetail = CreateTableStructure();
            }
            else
            {
                dtProductDetail = (DataTable)Session["dtProductDetail"];
                PopulateProductDetail();
                txtDiscount.Text = Session["Discount"].ToString();
                CalculateTotalPrice();
            }
            string strQuery = Request.QueryString["q"];
            if(!string.IsNullOrEmpty(strQuery))
            {
                Dictionary<String, String> objQuery = Common.PopulateDictionaryFromQueryString(strQuery);
                SelectedMode = objQuery["MODE"].ToString();
                SaleID = Convert.ToInt32(objQuery["ID"].ToString());
                if (Constants.MODE == Constants.MODE_EDIT)
                {
                    PopulateSaleDetail();
                    lblHeader.Text = "EDIT | Sale Order";
                }
                else
                {
                    lblHeader.Text = "ADD | Sale Order";
                }
            }
            else
            {
                Response.Redirect("Sale.aspx",false);
            }
        }
    }

    private DataTable CreateTableStructure()
    {
        DataTable dtData = new DataTable();
       
        dtData.Columns.Add("ProductID", typeof(int));
        dtData.Columns.Add("BarCode", typeof(string));
        dtData.Columns.Add("ProductName", typeof(string));
        dtData.Columns.Add("SizeName", typeof(string));
        dtData.Columns.Add("Quantity", typeof(decimal));
        dtData.Columns.Add("Unit", typeof(decimal));
        dtData.Columns.Add("PDiscount", typeof(decimal));
        dtData.Columns.Add("Price", typeof(decimal));
        dtData.PrimaryKey = new DataColumn[] { dtData.Columns["ProductID"] };
        return dtData;
    }

   

    #region Populate Sale Detail
    private void PopulateSaleDetail()
    {
        throw new NotImplementedException();
    } 
    #endregion
    protected void lnkAddMore_Click(object sender, EventArgs e)
    {
        AddDataToDataTable();
        PopulateProductDetail();
        CalculateTotalPrice();
    }

    private void CalculateTotalPrice()
    {
        decimal dblTotalPrice = 0;
        decimal dblDiscounted = 0;
        if (null != dtProductDetail)
        {
            for (int i = 0; i < dtProductDetail.Rows.Count; i++)
            {
                dblTotalPrice += Convert.ToDecimal(dtProductDetail.Rows[i]["Price"].ToString());
            }

            dblDiscounted = dblTotalPrice;
            if(!string.IsNullOrEmpty(txtDiscount.Text.Trim()))
            {
                dblDiscounted = dblTotalPrice - Convert.ToDecimal(txtDiscount.Text.Trim());
            }

            lblTotalAmount.Text =Math.Round(dblTotalPrice,2).ToString();// String.Format("{0:C}", dblTotalPrice);
            lblTotalPay.Text = Math.Round(dblDiscounted,2).ToString();// String.Format("{0:C}", dblDiscounted);
        }
    }

    private void AddDataToDataTable()
    {
        try
        {
            SaleBLL objSBLL = new SaleBLL();
            List<Sale> objSale = objSBLL.GetProductDetailByBarCode(txtProductBarCode.Text.Trim());
            RepopulateDataTableWithDiscountPrice();
            if (null != objSale && objSale.Count>0)
            {
                DataRow dtRow = dtProductDetail.NewRow();
                dtRow["ProductID"] = objSale[0].ProductID;
                dtRow["BarCode"] = objSale[0].BarCode;
                dtRow["ProductName"] = objSale[0].ProductName;
                dtRow["SizeName"] = objSale[0].SizeName;
                dtRow["Quantity"] =Convert.ToDecimal(txtQuantity.Text.Trim());
                dtRow["Unit"] = objSale[0].Price;
                dtRow["PDiscount"] = 0;
                dtRow["Price"] = objSale[0].Price * Convert.ToDecimal(txtQuantity.Text.Trim());
                dtProductDetail.Rows.Add(dtRow);
            }
            else
            {
                lblError.InnerHtml = "Product Bar Code not found";
            }
        }
        catch (ConstraintException ex)
        {
            lblError.InnerHtml = "Product all ready added";
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new System.Diagnostics.StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    private void RepopulateDataTableWithDiscountPrice()
    {
        if (gvGrid.Rows.Count > 0)
        {
            for (int i = 0; i < gvGrid.Rows.Count; i++)
            {
                TextBox txtPDiscount=(TextBox) gvGrid.Rows[i].Cells[6].FindControl("txtPDiscount");
                dtProductDetail.Rows[i]["PDiscount"] = (string.IsNullOrEmpty(txtPDiscount.Text.Trim()) ? 0 : Convert.ToDecimal(txtPDiscount.Text.Trim()));

                if (!string.IsNullOrEmpty(txtPDiscount.Text))
                {
                    dtProductDetail.Rows[i]["Price"] = (Convert.ToDecimal(dtProductDetail.Rows[i]["Price"].ToString()) * Convert.ToDecimal(dtProductDetail.Rows[i]["Quantity"].ToString())) - Convert.ToDecimal(txtPDiscount.Text);
                }
                else
                {
                    dtProductDetail.Rows[i]["Price"] = (Convert.ToDecimal(gvGrid.Rows[i].Cells[5].Text) * Convert.ToDecimal(gvGrid.Rows[i].Cells[4].Text));
                }
                dtProductDetail.AcceptChanges();
            }
        }
    }

    private void PopulateProductDetail()
    {
        if (null != dtProductDetail)
        {
            gvGrid.DataSource = dtProductDetail;
            gvGrid.DataBind();
        }
    }

    #region GRID VIEW EVENTS
   

    
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
                lnkDelete.OnClientClick = "return confirm('Product :" + BLL.BusinessObject.Constants.DeleteConf + "');";

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
                PopulateForm(intProductID);
            }

            if (e.CommandName == "Delete")
            {

                int intProductID = Convert.ToInt32(e.CommandArgument.ToString());
                for (int i = 0; i < dtProductDetail.Rows.Count; i++)
                {
                    if (dtProductDetail.Rows[i]["ProductID"].ToString() == intProductID.ToString())
                    {
                        dtProductDetail.Rows[i].Delete();
                        break;
                    }
                }
                dtProductDetail.AcceptChanges();
            }



        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    private void PopulateForm(int vintProductID)
    {
        for (int i = 0; i < dtProductDetail.Rows.Count; i++)
        {
            if (dtProductDetail.Rows[i]["ProductID"].ToString() == vintProductID.ToString())
            {
                txtProductBarCode.Text = dtProductDetail.Rows[i]["BarCode"].ToString();
                txtQuantity.Text=dtProductDetail.Rows[i]["Quantity"].ToString();
                dtProductDetail.Rows[i].Delete();
                break;
            }
        }
        dtProductDetail.AcceptChanges();
        PopulateProductDetail();
        CalculateTotalPrice();

    }

   

    protected void gvGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        PopulateProductDetail();
        CalculateTotalPrice();
    }

    protected void gvGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gvGrid.EditIndex = e.NewEditIndex;
    }

    #endregion

    protected void lnkFinalChekout_Click(object sender, EventArgs e)
    {
        if (null != dtProductDetail && dtProductDetail.Rows.Count > 0)
        {
            RepopulateDataTableWithDiscountPrice();
            Session["Discount"] = txtDiscount.Text.Trim();
            Session["dtProductDetail"] = dtProductDetail;
            param = Constants.MODE + "=" + SelectedMode + "&" + Constants.ID + "="+SaleID;
            param = Common.GenerateBASE64WithObfuscateApp(param);
            vstrLink = "FinalChekoutSaleOrder.aspx?q=" + param;
            Response.Redirect(vstrLink, false);
        }
    }

    

}