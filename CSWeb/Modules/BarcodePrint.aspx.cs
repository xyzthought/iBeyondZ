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
using System.Text;

public partial class Modules_BarcodePrint : System.Web.UI.Page
{
    PageInfo objPI = new PageInfo();
    protected string BarcodeHTML
    {
        get
        {
            if (ViewState["BarcodeHTML"] != null)
                return Convert.ToString(ViewState["BarcodeHTML"]);
            else
                return string.Empty;
        }
        set
        {
            ViewState["BarcodeHTML"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Readfile();
            PopulateBarCode();
        }
    }

    private void Readfile()
    {
        try
        {
            System.IO.StreamReader myFile = new System.IO.StreamReader(Server.MapPath("~/Handler/BarcodeLabel.html"));
            BarcodeHTML = myFile.ReadToEnd();
            myFile.Close();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
        
    }

    private void PopulateBarCode()
    {
        try
        {
            StringBuilder sbBarcode = new StringBuilder();
            string thisBarcode = string.Empty;
            if (txtSearch.Text.Trim() != "Search")
            {
                objPI.SearchText = txtSearch.Text.Trim();
            }
            else
            {
                objPI.SearchText = "";
            }


            SaleBLL objSaleBLL = new SaleBLL();

            List<Sale> objData = new List<Sale>();
            objData = objSaleBLL.GetAllProductBarCode(objPI);

            if (objData != null)
            {
                for (int i = 0; i < objData.Count; i++)
                {
                    thisBarcode = BarcodeHTML;
                    thisBarcode = thisBarcode.Replace("[productname]", objData[i].ProductName);
                    thisBarcode = thisBarcode.Replace("[productsize]", objData[i].SizeName);
                    string BarCode = objData[i].BarCode;
                    thisBarcode = thisBarcode.Replace("[dvbarcode]", "dvbarcode"+(i+1).ToString());
                    thisBarcode = thisBarcode.Replace("[barcode]", BarCode);
                    thisBarcode = thisBarcode.Replace("[dvbarcodePrint]", "dvbarcodePrint" + (i + 1).ToString());
                    thisBarcode = thisBarcode.Replace("[barcodePrint]", "[barcodePrint" + (i + 1).ToString() + "]");
                    sbBarcode.Append(thisBarcode);
                }
            }

            dvBarcodes.InnerHtml = sbBarcode.ToString();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
        }
    }

    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {
        PopulateBarCode();
    }
}