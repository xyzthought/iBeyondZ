using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Component;
using System.Data;
using BLL.BusinessObject;

namespace BLL.Component
{
    public class CustomerBLL
    {
        CustomerDB objDB;
        public CustomerBLL()
        {
            objDB = new CustomerDB();
        }
        public List<Customer> GetCustomerDetailByCustID(ref Customer objData)
        {
            return objDB.GetCustomerDetailByCustID(ref objData);
        }

        public List<Customer> GetAllCustomer(List<Customer> objData, PageInfo objPI)
        {
            return objDB.GetAllCustomer(objData, objPI);
        }

        
        public Message DeletePlatformCustomer(Customer objData)
        {
            return objDB.DeletePlatformCustomer(objData);
        }

        public Message InsertUpdatePlatformCustomer(Customer objCustomer)
        {
            return objDB.InsertUpdatePlatformCustomer(objCustomer);
        }
    }
}

