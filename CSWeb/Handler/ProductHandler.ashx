<%@ WebHandler Language="C#" Class="ProductHandler" %>

using System;
using System.Web;
using BLL.BusinessObject;

public class ProductHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        int intProductID = 0;
        decimal dcmBuyingPrice = 0;
        decimal dcmTax = 0;
        decimal dcmMargin = 0;
        decimal dcmSellingPrice = 0;
        Message objMessage = new Message();
        
        try
        {
            intProductID = Convert.ToInt32(context.Request["productID"]);
            dcmBuyingPrice = Convert.ToDecimal(context.Request["buyingPrice"]);
            dcmTax = Convert.ToDecimal(context.Request["tax"]);
            dcmMargin = Convert.ToDecimal(context.Request["margin"]);
            dcmSellingPrice = Convert.ToDecimal(context.Request["sellingPrice"]);

            objMessage.ReturnStatus = 1;
            objMessage.ReturnMessage = "Price updated successfully";
        }
        catch (Exception ex)
        {
            objMessage.ReturnStatus = -1;
            objMessage.ReturnMessage = "Error in updating successfully";
            
        }
        
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string serMessage = javaScriptSerializer.Serialize(objMessage);
        context.Response.ContentType = "text/html";
        context.Response.Write(serMessage);

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}