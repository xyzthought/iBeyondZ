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

    protected string ThisBarcode
    {
        get
        {
            if (ViewState["ThisBarcode"] != null)
                return Convert.ToString(ViewState["ThisBarcode"]);
            else
                return string.Empty;
        }
        set
        {
            ViewState["ThisBarcode"] = value;
        }
    }

    protected int Quantity
    {
        get
        {
            if (ViewState["Quantity"] != null)
                return Convert.ToInt32(ViewState["Quantity"]);
            else
                return 0;
        }
        set
        {
            ViewState["Quantity"] = value;
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
            DeleteOldFiles();
            string strQuery1 = Request.QueryString["q"];
            string strQuery2 = Request.QueryString["bc"];
            if (!string.IsNullOrEmpty(strQuery1))
            {
                PurchaseID = Convert.ToInt32(Request.QueryString["q"].ToString());
                PopulateBarCode(PurchaseID);
            }
            else if (!string.IsNullOrEmpty(strQuery2))
            {
                ThisBarcode =Request.QueryString["bc"].ToString();
                Quantity = Convert.ToInt32(Request.QueryString["Qty"].ToString());
                PrepareBarCodePDF(ThisBarcode,Quantity);
            }

        }
    }

    private void DeleteOldFiles()
    {
        var files = new DirectoryInfo(Server.MapPath("~/Handler/Barcode/")).GetFiles("*.pdf");
        foreach (var file in files)
        {
            if (DateTime.UtcNow - file.CreationTimeUtc > TimeSpan.FromDays(2))
            {
                File.Delete(file.FullName);
            }
        }


    }

    private void PrepareBarCodePDF(string vThisBarcode, int vQuantity )
    {
        //Rectangle pageSize = new Rectangle(842, 595);
        Rectangle pageSize = new Rectangle(62, 29);
        //Document document = new Document(PageSize.A4_LANDSCAPE, 10, 10, 10, 10);
        Document document = new Document(pageSize, 0, 0, 2, 0);
        string TimeStamp = DateTime.Now.Ticks.ToString();
        try
        {
            StringBuilder sbBarcode = new StringBuilder();
            string thisBarcode = string.Empty;

            objPI.SearchText = "";

            SaleBLL objSaleBLL = new SaleBLL();

            List<Sale> objData = new List<Sale>();
            objData = objSaleBLL.GetProductBarCode(vThisBarcode, vQuantity);


            PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~") + "/Handler/Barcode/BarCode_" + TimeStamp + ".pdf", FileMode.Create));
            document.Open();
            PdfContentByte pdfContentByte = pdfWriter.DirectContent;
            iTextSharp.text.Image img;

            if (objData != null)
            {
                int Kounter = 0;
                for (int i = 0; i < objData.Count; i++)
                {
                    int PurchaseQuantity = (int)objData[i].Quantity;

                    if (Kounter > 0)
                        document.NewPage();

                    PdfPTable table = new PdfPTable(2);

                    table.HorizontalAlignment = 1;
                    table.WidthPercentage = 95;

                    PdfPCell cell = new PdfPCell(new Phrase(objData[i].Brand, new Font(Font.FontFamily.HELVETICA, 3f, Font.BOLD)));
                    cell.Colspan = 2;
                    cell.Border = 0;
                    cell.Padding = 0f;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(objData[i].ProductName, new Font(Font.FontFamily.HELVETICA, 2f, Font.NORMAL)));
                    cell.Colspan = 2;
                    cell.Border = 0;
                    cell.Padding = 0f;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Size: " + objData[i].SizeName, new Font(Font.FontFamily.HELVETICA, 3f, Font.NORMAL)));
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cell.Border = 0;
                    cell.Padding = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("€ " + string.Format("{0:0.00}", objData[i].Price), new Font(Font.FontFamily.HELVETICA, 4f, Font.BOLD)));
                    cell.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    cell.Border = 0;
                    cell.Padding = .5f;
                    table.AddCell(cell);

                    pdfContentByte = pdfWriter.DirectContent;
                    img = GetBarcode128(pdfContentByte, objData[i].BarCode, false, Barcode.CODE128);

                    cell = new PdfPCell(); //new Phrase(new Chunk(img, 0, 0)));
                    cell.AddElement(img);
                    cell.Colspan = 2;
                    cell.Border = 0;
                    cell.Padding = 0f;
                    //cell.FixedHeight = img.Height + 200;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    document.Add(table);

                    Kounter++;

                }
                document.Close();

            }

            HttpContext context = HttpContext.Current;
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Barcode_" + TimeStamp + ".pdf");
            Response.WriteFile(Server.MapPath("~/Handler/Barcode/BarCode_" + TimeStamp + ".pdf"));
            Response.Flush();
            Response.Close();
            if (File.Exists(Server.MapPath("~/Handler/Barcode/BarCode_" + TimeStamp + ".pdf")))
            {
                File.Delete(Server.MapPath("~/Handler/Barcode/BarCode_" + TimeStamp + ".pdf"));
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
        string TimeStamp = DateTime.Now.Ticks.ToString();
        HttpContext context = HttpContext.Current;
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Barcode_" + TimeStamp + ".pdf");

        //Render PlaceHolder to temporary stream 
        System.IO.StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        StringReader reader = new StringReader(HTMLCode);

        //Create PDF document 
        Document doc = new Document(PageSize.A4);
        HTMLWorker parser = new HTMLWorker(doc);
        PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~") + "/Handler/Barcode/BarCode_" + TimeStamp + ".pdf", FileMode.Create));
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

        Response.WriteFile(Server.MapPath("~/Handler/Barcode/BarCode_" + TimeStamp + ".pdf"));
        Response.Flush();
        Response.Close();
        if (File.Exists(Server.MapPath("~/Handler/Barcode/BarCode_" + TimeStamp + ".pdf")))
        {
            File.Delete(Server.MapPath("~/Handler/Barcode/BarCode_" + TimeStamp + ".pdf"));
        }

        /********************************************************************************/

    }


    private void PopulateBarCode(int PurchaseID)
    {
        //Rectangle pageSize = new Rectangle(842, 595);
        Rectangle pageSize = new Rectangle(62,29);
        //Document document = new Document(PageSize.A4_LANDSCAPE, 10, 10, 10, 10);
        Document document = new Document(pageSize, 0, 0, 2, 0);
        string TimeStamp = DateTime.Now.Ticks.ToString();
        try
        {
            StringBuilder sbBarcode = new StringBuilder();
            string thisBarcode = string.Empty;

            objPI.SearchText = "";

            SaleBLL objSaleBLL = new SaleBLL();

            List<Sale> objData = new List<Sale>();
            objData = objSaleBLL.GetAllProductBarCode(objPI, PurchaseID);


            PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~") + "/Handler/BarCode_" + TimeStamp + ".pdf", FileMode.Create));
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
                        if (Kounter > 0)
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
                        //table.SpacingBefore = 1f;
                        //table.SpacingAfter = 1f;
                        table.WidthPercentage = 95;

                        PdfPCell cell = new PdfPCell(new Phrase(objData[i].Brand, new Font(Font.FontFamily.HELVETICA, 3f, Font.BOLD)));
                        cell.Colspan = 2;
                        cell.Border = 0;
                        cell.Padding = 0f;
                        cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(objData[i].ProductName, new Font(Font.FontFamily.HELVETICA, 2f, Font.NORMAL)));
                        cell.Colspan = 2;
                        cell.Border = 0;
                        cell.Padding = 0f;
                        cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Size: " + objData[i].SizeName, new Font(Font.FontFamily.HELVETICA, 3f, Font.NORMAL)));
                        cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        cell.Border = 0;
                        cell.Padding = 0f;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase("€ " + string.Format("{0:0.00}", objData[i].Price), new Font(Font.FontFamily.HELVETICA, 4f, Font.BOLD)));
                        cell.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        cell.Border = 0;
                        cell.Padding = .5f;
                        table.AddCell(cell);

                        pdfContentByte = pdfWriter.DirectContent;
                        img = GetBarcode128(pdfContentByte, objData[i].BarCode, false, Barcode.CODE128);

                        cell = new PdfPCell(); //new Phrase(new Chunk(img, 0, 0)));
                        cell.AddElement(img);
                        cell.Colspan = 2;
                        cell.Border = 0;
                        cell.Padding = 0f;
                        //cell.FixedHeight = img.Height + 200;
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
            Response.AppendHeader("Content-Disposition", "attachment; filename=Barcode_" + TimeStamp + ".pdf");
            Response.WriteFile(Server.MapPath("~/Handler/BarCode_" + TimeStamp + ".pdf"));
            Response.Flush();
            Response.Close();
            if (File.Exists(Server.MapPath("~/Handler/BarCode_" + TimeStamp + ".pdf")))
            {
                File.Delete(Server.MapPath("~/Handler/BarCode_" + TimeStamp + ".pdf"));
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