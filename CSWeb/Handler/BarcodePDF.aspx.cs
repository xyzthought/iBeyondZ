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
            string strQuery = Request.QueryString["q"];

            if (!string.IsNullOrEmpty(strQuery))
            {
                PurchaseID = Convert.ToInt32(Request.QueryString["q"].ToString());
                PopulateBarCode(PurchaseID);
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


    private void PopulateBarCode(int PurchaseID)
    {
        Document document = new Document(PageSize.A4, 10, 10, 10, 10);
        try
        {
            StringBuilder sbBarcode = new StringBuilder();
            string thisBarcode = string.Empty;

            objPI.SearchText = "";

            SaleBLL objSaleBLL = new SaleBLL();

            List<Sale> objData = new List<Sale>();
            objData = objSaleBLL.GetAllProductBarCode(objPI, PurchaseID);

            
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~") + "/Handler/BarCode.pdf", FileMode.Create));
            document.Open();
            PdfContentByte pdfContentByte = pdfWriter.DirectContent;
            iTextSharp.text.Image img;

            if (objData != null)
            {
                int Kounter = 0;
                for (int i = 0; i < objData.Count; i++)
                {
                    int PurchaseQuantity = (int)objData[i].Quantity;

                    for (int ii = 0; ii < PurchaseQuantity; ii++)
                    {
                        if (ii > 0)
                            document.NewPage();

                        PdfPTable table = new PdfPTable(2);
                        //table.TotalWidth = 176f;
                        //fix the absolute width of the table
                        //table.LockedWidth = true;

                        //relative col widths in proportions
                        //float[] widths = new float[] { 1f, 1f };
                        //table.SetWidths(widths);
                        table.HorizontalAlignment = 1;
                        //leave a gap before and after the table
                        table.SpacingBefore = 20f;
                        table.SpacingAfter = 30f;

                        PdfPCell cell = new PdfPCell(new Phrase(objData[i].Brand, new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD)));
                        cell.Colspan = 2;
                        cell.Border = 0;
                        cell.Padding = 2f;
                        cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(objData[i].ProductName, new Font(Font.FontFamily.HELVETICA, 22, Font.NORMAL)));
                        cell.Colspan = 2;
                        cell.Border = 0;
                        cell.Padding = 2f;
                        cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Size: " + objData[i].SizeName, new Font(Font.FontFamily.HELVETICA, 22, Font.NORMAL)));
                        cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        cell.Border = 0;
                        cell.Padding = 2f;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase("€" + string.Format("{0:0.00}", objData[i].Price), new Font(Font.FontFamily.HELVETICA, 28, Font.NORMAL)));
                        cell.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        cell.Border = 0;
                        cell.Padding = 2f;
                        table.AddCell(cell);

                        pdfContentByte = pdfWriter.DirectContent;
                        img = GetBarcode128(pdfContentByte, objData[i].BarCode, false, Barcode.CODE128);

                        cell = new PdfPCell(); //new Phrase(new Chunk(img, 0, 0)));
                        cell.AddElement(img);
                        cell.Colspan = 2;
                        cell.Border = 0;
                        cell.Padding = 2f;
                        cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        table.AddCell(cell);

                        document.Add(table);

                        Kounter++;
                    }

                }
                document.Close();

            }

            HttpContext context = HttpContext.Current;
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Barcode.pdf");
            Response.WriteFile(Server.MapPath("~/Handler/BarCode.pdf"));
            Response.Flush();
            Response.Close();
            if (File.Exists(Server.MapPath("~/Handler/BarCode.pdf")))
            {
                File.Delete(Server.MapPath("~/Handler/BarCode.pdf"));
            }

        }
        catch (Exception ex)
        {
            if (document.IsOpen())
                document.Close();
            SendMail.MailMessage("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString());
            Response.Write(ex.ToString());
        }
    }

    public iTextSharp.text.Image GetBarcode128(PdfContentByte pdfContentByte, string code, bool extended, int codeType)
    {
        Barcode128 code128 = new Barcode128 { Code = code, Extended = extended, CodeType = codeType };
        code128.ChecksumText = true;
        return code128.CreateImageWithBarcode(pdfContentByte, null, null);
    }
}