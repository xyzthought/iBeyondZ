using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Component;
using System.Data;
using BLL.BusinessObject;

namespace BLL.Component
{
    public class UserBLL
    {
        UserDB objDB;
        public UserBLL()
        {
            objDB = new UserDB();
        }

        public void AuthenticationValidation(ref User objUser)
        {
            objDB.AuthenticationValidation(ref objUser);
        }

        public DataTable GetSourceData()
        {
            return objDB.GetSourceData();
        }

        public void SaveSourceDataTable(DataTable dtSource)
        {
            objDB.SaveSourceDataTable(dtSource);
        }


        public List<User> GetAllUser(List<User> objData, PageInfo objPI)
        {
            return objDB.GetAllUser(objData, objPI);
        }

        public Message InsertUpdatePlatformUser(User objUser)
        {
            return objDB.InsertUpdatePlatformUser(objUser);
        }

        public List<User> GetPlatformUserByUserID(ref User objUser)
        {
            return objDB.GetPlatformUserByUserID(ref objUser);
        }

        public Message DeletePlatformUser(User objUser)
        {
            return objDB.DeletePlatformUser(objUser);
        }

        public Message ChangePassword(User objUser, string vstrNewPassword)
        {
            return objDB.ChangePassword(objUser, vstrNewPassword);
        }

       
        public User ChangeAccountInformation(ref User objUser)
        {
            return objDB.ChangeAccountInformation(ref objUser);
        }
    }
}
