using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    public static class Constants
    {

        /// <summary>
        /// Marks the application environment Stage or Live.
        /// </summary>
        public enum ApplicationEnvironment
        {
            Live = 1,
            Stage = 2
        }

        /// <summary>
        /// Marks the export type of any file (basically reports) Excel or CSV.
        /// </summary>
        public enum ExportType
        {
            Excel = 0,
            CSV = 1
        }

        /// <summary>
        /// Marks the user type logged in.
        /// </summary>
        public enum UserType
        {
            ErrorValue0 = 0,
            Admin = 1,
            StockManager = 2,
            SellingDesk = 3
        }

        /// <summary>
        /// Represent the browser type. 
        /// </summary>
        public enum BrowserType
        {
            Unknown = 0,
            InternetExplorer = 1,
            FireFox = 2,
            Safari = 3,
            Chrome = 4,
            Opera = 10
        }


        public enum ControlType
        {
            DropDownList = 1,
            CheckBoxList = 2,
            RadioButtonList = 3,
            ListBox = 4
        }

        /// <summary>
        /// Marks the user type logged in.
        /// </summary>
        public enum ContactType
        {
            Admin = 0,
            SalesDesk = 1
        }


        public enum Status
        {
            Inactive = 0,
            Active = 1
        }

       



        public const string SORTDERECTION = "SortDirection";
        public const string SORTCOLUMNNAME = "SortColumnName";
        public const string CURRENTPAGENO = "CurrentPageNo";
        public const string COMMAND_EDIT = "edit";
        public const string COMMAND_DELETE = "Delete";
        public const string COMMAND_ADD = "Add";
        public const int PAGE_SIZE = 10;
        public const string DRP_DEFAULT = "--Please Select--";
        public const string DRP_DEFAULT_VAL = "-1";
        public const string DRP_DEFAULT_SHORT = "--Select--";
        public const string ID = "ID";
        public const string Name = "Name";
        public const string PageID = "PageID";
        public const string MODE = "MODE";
        public const string MODE_ADD = "ADD";
        public const string MODE_EDIT = "EDIT";
        public const string MODE_VIEW = "VIEW";
        public const string DATE_FORMAT = "MM/dd/yyyy";
        public const string DATE_FORMAT_ddMMMyy = "dd-MMM-yy";
       
        public const string ExpiryStatus = "ExpiryStatus";
        public const string ASC = "ASC";
        public const string DESC = "DESC";
        public const string VIEW_BTN = "Exit";
        public const string VIEWCONT_BTN = "Continue & View";
        public const string LOGGEDIN_ID = "UserId";
        public const string LOGGEDIN_Type = "UserType";
        public const string HYPENSTRING = "-";

        public const string Update = "updated successfully";
        public const string Activate = "activated successfully";

        public const string InActivate = "inactivated successfully";
        public const string Deleted = "deleted successfully";
        public const string Removed = "removed successfully";
        public const string Added = "added successfully";
        public const string AddedNew = "added successfully";
        public const string UpdatedNew = "updated successfully";
        public const string CancelConf = " are you sure want to cancel?";
        public const string DeleteConf = " are you sure want to delete the selected data?";
        public const string RemoveConf = " are you sure want to remove the selected data?";
        public const string ActiveConf = " are you sure want to change the status to Active?";
        public const string InActiveConf = " are you sure want to change the status to Inactive?";

        public const string Error = " Error occurred";
       
        public const string USER_TYPE = "UserType";
        public const string LOGIN_ADMIN = "LoginAdmin";
        public const string LOGIN_SOCKMANAGER = "LoginSMgr";
        public const string LOGIN_SELLINGDESK = "LoginSDesk";
        


    }
}
