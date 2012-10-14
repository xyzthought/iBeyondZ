using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.BusinessObject;

namespace BLL.Component
{
    public class ProductBLL
    {
        public int AddProduct(BLL.BusinessObject.Product vobjProduct)
        {
            DAL.Component.Product objProdDAL = new DAL.Component.Product();
            return objProdDAL.InsertProduct(vobjProduct);
        }

        public int EditProduct(BLL.BusinessObject.Product vobjProduct)
        {
            DAL.Component.Product objProdDAL = new DAL.Component.Product();
            return objProdDAL.UpdateProduct(vobjProduct);
        }

        public List<BLL.BusinessObject.Product> GetAllProducts(PageInfo info)
        {
            DAL.Component.Product objProdDAL = new DAL.Component.Product();
            return objProdDAL.GetAllProduct(info);
        }

        public List<BLL.BusinessObject.Product> GetAllActiveProduct()
        {
            return new DAL.Component.Product().GetAllActiveProduct();
        }
        public BLL.BusinessObject.Product GetProductByID(int ProductID)
        {
            DAL.Component.Product objProdDAL = new DAL.Component.Product();
            return objProdDAL.GetProductByID(ProductID);
        }



        public bool DeleteProduct(int ProductID)
        {
            return new DAL.Component.Product().DeleteProduct(ProductID);
        }

        public List<Product> GetAllProducts(int intProductID)
        {
            return null;
        }
    }
}
