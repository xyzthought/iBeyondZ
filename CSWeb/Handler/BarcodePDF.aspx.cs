using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using BLL.Component;
using BLL.BusinessObject;
using CSWeb.Utility;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;
using iTextSharp.text.html.simpleparser;


public partial class Handler_BarcodePDF : System.Web.UI.Page
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

    protected int PurchaseID
    {
        get
        {
            if (ViewState["PurchaseID"] != null)
                return Convert.ToInt32(ViewState["PurchaseID"]);
            else
                return 0;
        }
        set
        {
            ViewState["PurchaseID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            /*Readfile();
            string strQuery = Request.QueryString["q"];

            if (!string.IsNullOrEmpty(strQuery))
            {
                PurchaseID = Convert.ToInt32(Request.QueryString["q"].ToString());
            }
            string strBarCode = PopulateBarCode(PurchaseID);
            ConvertHTMLToPDF(strBarCode);
            */

            if (null != Session["BarCodeHTML"])
            {
                // ConvertHTMLToPDF(Session["BarCodeHTML"].ToString());
            }
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

    protected void ConvertHTMLToPDF(string HTMLCode)
    {
        HttpContext context = HttpContext.Current;
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Barcode.pdf");
 
        //Render PlaceHolder to temporary stream 
        System.IO.StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
 
        StringReader reader = new StringReader(HTMLCode);
 
        //Create PDF document 
        Document doc = new Document(PageSize.A4);
        HTMLWorker parser = new HTMLWorker(doc);
        PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~") + "/Handler/BarCode.pdf", FileMode.Create));
        doc.Open();
 
        /********************************************************************************/
        //var interfaceProps = new Dictionary<string, object="">();
        //var ih = new ImageHander() { BaseUri = Request.Url.ToString() };
 
        //interfaceProps.Add(HTMLWorker.IMG_PROVIDER, ih);
 
        foreach (IElement element in HTMLWorker.ParseToList(
            new StringReader(HTMLCode), null))
        {
            doc.Add(element);
        }
        doc.Close();
        //Response.End();

        Response.WriteFile(Server.MapPath("~/Handler/BarCode.pdf"));
        Response.Flush();
        Response.Close();
        if (File.Exists(Server.MapPath("~/Handler/BarCode.pdf")))
        {
            File.Delete(Server.MapPath("~/Handler/BarCode.pdf"));
        }
 
        /********************************************************************************/
         
    }

    /*public void WritePDF(string content)
    {
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=table.pdf");
        var doc = new Document();
        PdfWriter.GetInstance(doc, Response.OutputStream);
        doc.Open();
        var html = new StringBuilder();
        var stringWriter = new StringWriter(html);
        var htmlWriter = new HtmlTextWriter(stringWriter);
        toPDF.RenderControl(htmlWriter);
        var interfaceProps = new Dictionary<string, Object>();
    }*/

    private string PopulateBarCode(int PurchaseID)
    {
        try
        {
            StringBuilder sbBarcode = new StringBuilder();
            string thisBarcode = string.Empty;
            
            objPI.SearchText = "";

            SaleBLL objSaleBLL = new SaleBLL();

            List<Sale> objData = new List<Sale>();
            objData = objSaleBLL.GetAllProductBarCode(objPI, PurchaseID);

            if (objData != null)
            {
                int Kounter = 0;
                for (int i = 0; i < objData.Count; i++)
                {
                    int PurchaseQuantity = (int)objData[i].Quantity;

                    for (int ii = 0; ii < PurchaseQuantity; ii++)
                    {
                        thisBarcode = BarcodeHTML;
                        thisBarcode = thisBarcode.Replace("[brandname]", objData[i].Brand);
                        thisBarcode = thisBarcode.Replace("[productname]", objData[i].ProductName);
                        thisBarcode = thisBarcode.Replace("[productsize]", objData[i].SizeName);
                        thisBarcode = thisBarcode.Replace("[sellingprice]", string.Format("{0:0.00}", objData[i].Price));
                        string BarCode = objData[i].BarCode;
                        thisBarcode = thisBarcode.Replace("[dvbarcode]", "dvbarcode" + (Kounter + 1).ToString());
                        thisBarcode = thisBarcode.Replace("[barcode]", BarCode);
                        thisBarcode = thisBarcode.Replace("[dvbarcodePrint]", "dvbarcodePrint" + (Kounter + 1).ToString());
                        thisBarcode = thisBarcode.Replace("[barcodePrint]", "[barcodePrint" + (Kounter + 1).ToString() + "]");
                        sbBarcode.Append(thisBarcode);
                        Kounter++;
                    }

                }
            }

            return sbBarcode.ToString();
        }
        catch (Exception ex)
        {
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            return "";
        }
    }
}