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
        public List<Customer> GetCustomerDetailByCustID(ref Customer objCust)
        {
            return objDB.GetCustomerDetailByCustID(ref objCust);
        }
    }
}

