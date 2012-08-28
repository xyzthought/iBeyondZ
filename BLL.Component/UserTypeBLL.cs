using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Component;
using System.Data;
using BLL.BusinessObject;

namespace BLL.Component
{
    public class UserTypeBLL
    {
         UserDB objDB;
         public UserTypeBLL()
         {
             objDB = new UserDB();
         }

         public List<UserTypeBO> GetAllUserType()
         {
             return objDB.GetAllUserType();
         }
    }
}
