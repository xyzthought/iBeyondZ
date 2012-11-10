<%@ WebHandler Language="C#" Class="ProductHandler" %>

using System;
using System.Web;
using BLL.BusinessObject;
using BLL.Component;

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
            ProductPurchase objProductPurchase = new ProductPurchase();
            
            objProductPurchase.ProductID = Convert.ToInt32(context.Request["productID"]);
            objProductPurchase.BuyingPrice = Convert.ToDecimal(context.Request["buyingPrice"]);
            objProductPurchase.Tax = Convert.ToDecimal(context.Request["tax"]);
            objProductPurchase.Margin = Convert.ToDecimal(context.Request["margin"]);
            objProductPurchase.SellingPrice = Convert.ToDecimal(context.Request["sellingPrice"]);

            new ProductPurchaseBLL().UpdateProductPrice(ref objProductPurchase);

            if (objProductPurchase.ReturnValue > 0)
            {
                objMessage.ReturnStatus = 1;
                objMessage.ReturnMessage = "Price updated successfully";
            }
            else
            {
                objMessage.ReturnStatus = -1;
                objMessage.ReturnMessage = "Error in updating Price";
            }
            
            
        }
        catch (Exception ex)
        {
            objMessage.ReturnStatus = -1;
            objMessage.ReturnMessage = "Error in updating Price";
            
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