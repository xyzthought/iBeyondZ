using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BLL.BusinessObject;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using Utility;
using System.Net.Mime;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSWeb.Utility
{
    public class Common
    {
        private static string[] strSourceCh = { "w", "b", "Z", "Y", "J", "a", "u" };
        private static string[] strReplaceCh = { "@", "#", "$", "~", "!", "[", "]" };
        private static string[] strReplaceChApp = { "@", "*", "$", "_", "!", "[", "]" };

        #region Populate Dictionary from querystring
        public static Dictionary<String, String> PopulateDictionaryFromQueryString(String vstrQueryString)
        {
            Dictionary<String, String> mobjQueryString = new Dictionary<String, String>();
            String[] mstrQueryStrings = DecryptBASE64WithObfuscateApp(vstrQueryString).Split('&');
            String[] mstQueryString;
            for (int i = 0; i < mstrQueryStrings.Length; i++)
            {
                mstQueryString = mstrQueryStrings[i].Split('=');
                mobjQueryString.Add(mstQueryString[0], mstQueryString[1]);
            }

            return mobjQueryString;
        }
        #endregion

        #region Populate Dropdown
        public void PopulateDropDownList(DropDownList vobjList, Dictionary<int, String> varrDataSource, string vstrInitialDataTextField, string vstrInitialDataValueField)
        {
            try
            {
                vobjList.DataSource = varrDataSource;
                vobjList.DataTextField = "Value";
                vobjList.DataValueField = "Key";
                vobjList.DataBind();
                if (vstrInitialDataTextField != null && vstrInitialDataValueField != null)
                {
                    vobjList.Items.Insert(0, new ListItem(vstrInitialDataTextField, vstrInitialDataValueField));
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message.ToString();
            }
        }
        #endregion

        #region LogError

        public static string SupportedDataForException(string[] strField, string[] strData)
        {
            string strSupportData = "<b>Supported Data :";
            if (strData.Length > 0)
            {
                if (strField.Length == strData.Length)
                {
                    for (int i = 0; i < strData.Length; i++)
                    {
                        strSupportData += "<br />" + strField[i] + " : " + strData[i];
                    }
                }
            }
            strSupportData += "<br /><br /></b>";
            return strSupportData;
        }

        public static void LogError(string strSubject, string strMessage, string strSupportedData = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(strSupportedData))
                    strSupportedData = string.Empty;
                string strErrorFrom = ConfigurationManager.AppSettings.Get("ErrorGeneratedFrom").ToString();
                string strIP = HttpContext.Current.Request.UserHostAddress;
                string strBrowserName = HttpContext.Current.Request.Browser.Browser;
                string strBrowserVersion = HttpContext.Current.Request.Browser.Version;
                bool blnIsBrowserJavaScriptSupport = HttpContext.Current.Request.Browser.JavaScript;
                string strBrowserJavaScriptVersion = HttpContext.Current.Request.Browser.EcmaScriptVersion.ToString();

                System.OperatingSystem osInfo = System.Environment.OSVersion;

                MailService objMail = new MailService();
                objMail.UserName = ConfigurationManager.AppSettings.Get("UID").ToString();
                objMail.Password = ConfigurationManager.AppSettings.Get("PWD").ToString();
                objMail.SMTPServer = ConfigurationManager.AppSettings.Get("MailServer").ToString();
                //objMail.SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Port"));
                //if (Convert.ToInt32(ConfigurationManager.AppSettings.Get("EnableSSL")) > 0)
                //    objMail.IsSSLEnabled = true;
                objMail.FromFriendlyName = ConfigurationManager.AppSettings.Get("ErrorMailFromFriendlyName").ToString();
                objMail.Priority = System.Net.Mail.MailPriority.Low;
                objMail.From = ConfigurationManager.AppSettings.Get("ErrorMailFrom").ToString();
                objMail.To = ConfigurationManager.AppSettings.Get("ErrorEmailTo").ToString();
                string strCC = ConfigurationManager.AppSettings.Get("ErrorMailCC").ToString();
                if (!string.IsNullOrWhiteSpace(strCC))
                    objMail.CC = strCC.Split(',').ToList();
                objMail.Subject = strSubject;
                objMail.MailBody = "<b>Error Generated from Server - " + strErrorFrom +
                    "</b><br /><b>Error Generated from IP Address - " + strIP +
                    "</b><br /><b>Browser Name - " + strBrowserName +
                    "</b><br /><b>Browser Version - " + strBrowserVersion +
                    "</b><br /><b>JavaScript Support - " + blnIsBrowserJavaScriptSupport +
                    "</b><br /><b>JavaScript Version - " + strBrowserJavaScriptVersion +
                    "</b><br /><b>Browser Platform Name - " + osInfo.VersionString + "</b><br /><br /><br />" + strSupportedData + strMessage;

                if (strSubject.ToLower().Contains("success"))
                {
                    int intretvalue = objMail.SendMail();
                    string strOutPut = objMail.MailException;
                }
                else if (strSubject.ToLower().Contains("error"))
                {
                    int intretvalue = objMail.SendMail();
                    string strOutPut = objMail.MailException;
                }
                else if (strSubject.ToLower().Contains("issue"))
                {
                    int intretvalue = objMail.SendMail();
                    string strOutPut = objMail.MailException;
                }
            }
            catch (Exception ex)
            {
                string strException = ex.Message.ToString();
            }
        }
        #endregion

        #region SendMail

        public static bool SendMail(MailMessage oMailMessage, bool blAllowCC, string strCC = null)
        {
            try
            {
                //using (SmtpClient oMailClient = new SmtpClient(ConfigurationManager.AppSettings.Get("MailServer"),587))
                using (SmtpClient oMailClient = new SmtpClient(ConfigurationManager.AppSettings.Get("MailServer"), Convert.ToInt32(ConfigurationManager.AppSettings.Get("Port"))))
                {
                    NetworkCredential netcred = new NetworkCredential(ConfigurationManager.AppSettings.Get("UID"), ConfigurationManager.AppSettings.Get("PWD"));

                    oMailClient.UseDefaultCredentials = false;
                    oMailClient.Credentials = netcred;
                    if (oMailMessage.From == null || string.IsNullOrEmpty(oMailMessage.From.Address))
                        oMailMessage.From = new MailAddress(ConfigurationManager.AppSettings.Get("MailFrom"));
                    oMailMessage.IsBodyHtml = true;
                    if (oMailMessage.Body.Contains("cid:"))
                    {
                        AlternateView avwSiteLogo = AlternateView.CreateAlternateViewFromString(oMailMessage.Body, null, MediaTypeNames.Text.Html);
                        if (oMailMessage.Body.Contains("cid:sitelogo"))
                        {
                            ContentType oContentType = new ContentType("image/gif");
                            oContentType.Name = "logo.jpg";
                            string strpath = HttpContext.Current.Server.MapPath("~/Images/Email_logo.png");
                            LinkedResource lresSiteLogo = new LinkedResource(strpath, oContentType);
                            lresSiteLogo.ContentId = "sitelogo";
                            avwSiteLogo.LinkedResources.Add(lresSiteLogo);
                        }

                        oMailMessage.AlternateViews.Add(avwSiteLogo);
                    }

                    if (blAllowCC)
                    {
                        if (string.IsNullOrWhiteSpace(strCC))
                        {
                            if (Convert.ToInt32(ConfigurationManager.AppSettings.Get("EnableCC")) > 0)
                                oMailMessage.CC.Add(ConfigurationManager.AppSettings.Get("MailCC"));
                        }
                        else
                            oMailMessage.CC.Add(strCC);
                    }

                    if (Convert.ToInt32(ConfigurationManager.AppSettings.Get("EnableSSL")) > 0)
                    {
                        oMailClient.EnableSsl = true;
                    }
                    oMailClient.Send(oMailMessage);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogError("CSWeb > Error > " + (new StackTrace()).GetFrame(0).GetMethod().Name, ex.ToString(), "");
                return false;
            }
        }

        #endregion SendMail

        #region BASE64 Related Methods

        /// <summary>
        /// Encrypts using base 64 
        /// </summary>
        /// <param name="vstrData"></param>
        /// <returns></returns>
        public static string GenerateBASE64WithObfuscate(string vstrData)
        {
            string strGetBase64 = EncodeTo64(vstrData);
            for (int i = 0; i < strSourceCh.Length; i++)
            {
                strGetBase64 = ReplaceCharacter(strGetBase64, strSourceCh[i], strReplaceCh[i]);
            }
            return strGetBase64;
        }

        public static string GenerateBASE64WithObfuscateApp(string vstrData)
        {
            string strGetBase64 = EncodeTo64(vstrData);
            for (int i = 0; i < strSourceCh.Length; i++)
            {
                strGetBase64 = ReplaceCharacter(strGetBase64, strSourceCh[i], strReplaceChApp[i]);
            }
            return strGetBase64;
        }

        /// <summary>
        /// Replaces some characters in the source string. 
        /// </summary>
        /// <param name="vstrSource"></param>
        /// <param name="vstrMatchPattern"></param>
        /// <param name="vstrReplaceStr"></param>
        /// <returns></returns>
        private static string ReplaceCharacter(string vstrSource, string vstrMatchPattern, string vstrReplaceStr)
        {
            string strOutput = vstrSource.Replace(vstrMatchPattern, vstrReplaceStr);
            return strOutput;
        }

        /// <summary>
        /// Decrypts the passed string using Base 64 encryptions.
        /// </summary>
        /// <param name="vstrData"></param>
        /// <returns></returns>
        public static string DecryptBASE64WithObfuscate(string vstrData)
        {
            string strGetDecryptedData = string.Empty;
            for (int i = 0; i < strSourceCh.Length; i++)
            {
                vstrData = ReplaceCharacter(vstrData, strReplaceCh[i], strSourceCh[i]);
            }
            strGetDecryptedData = DecodeFrom64(vstrData);
            return strGetDecryptedData;
        }

        public static string DecryptBASE64WithObfuscateApp(string vstrData)
        {
            string strGetDecryptedData = string.Empty;
            for (int i = 0; i < strSourceCh.Length; i++)
            {
                vstrData = ReplaceCharacter(vstrData, strReplaceChApp[i], strSourceCh[i]);
            }
            strGetDecryptedData = DecodeFrom64(vstrData);
            return strGetDecryptedData;
        }

        /// <summary>
        /// Encodes the passed string to Base 64
        /// </summary>
        /// <param name="vstrToEncode"></param>
        /// <returns></returns>
        public static string EncodeTo64(string vstrToEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(vstrToEncode);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;
        }

        /// <summary>
        /// Decrypts the passed string from Base 64.
        /// </summary>
        /// <param name="vstrEncodedData"></param>
        /// <returns></returns>
        public static string DecodeFrom64(string vstrEncodedData)
        {

            byte[] encodedDataAsBytes = System.Convert.FromBase64String(vstrEncodedData);

            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;

        }
        #endregion

        #region Populate Data Table from Excel
        /// <summary>
        /// Populate Data Table from Excel
        /// </summary>
        /// <param name="vstrFileExtension"></param>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public DataTable PopulateToDataTable(String vstrFileExtension, String vstrFilePath, String vstrisHDR)
        {
            String conStr = String.Empty;
            DataTable dtCustSalesOrd = new DataTable();
            switch (vstrFileExtension)
            {
                case "xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["xls"]
                             .ConnectionString;
                    break;
                case "xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["xlsx"]
                              .ConnectionString;
                    break;
            }

            conStr = String.Format(conStr, vstrFilePath, vstrisHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();

            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();




            String strSheetName = String.Empty;






            cmdExcel.CommandText = " SELECT * From [Sheet1$] ";

            oda.SelectCommand = cmdExcel;
            oda.Fill(dtCustSalesOrd);





            connExcel.Close();

            return dtCustSalesOrd;
        }
        #endregion

        #region ListToDataTable

        /// <summary>
        /// Converts IEnumerable(T) to datatable and returns the datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>

        public static DataTable ListToDataTable<T>(IEnumerable<T> list)
        {
            var dt = new DataTable();

            foreach (var info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }
            foreach (var t in list)
            {
                var row = dt.NewRow();
                foreach (var info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        #endregion

        #region string cutTextToSpecifiedSize
        public static string cutTextToSpecifiedSize(string stringToCut, int sizeToLimit)
        {
            string strReturnVal = "";

            if (!string.IsNullOrEmpty(stringToCut))
            {
                int strLength = stringToCut.Length;

                if (strLength > sizeToLimit)
                    strReturnVal = stringToCut.Substring(0, sizeToLimit) + "<strong>...</strong>";
                else
                    strReturnVal = stringToCut;
            }

            return strReturnVal;
        }

        public static string cutHtmlToSpecifiedSize(string stringToCut, int sizeToLimit, bool encodeHtml = false)
        {
            string strReturnVal = "";

            if (!string.IsNullOrEmpty(stringToCut))
            {
                int strLength = stringToCut.Length;

                if (strLength > sizeToLimit)
                    strReturnVal = stringToCut.Substring(0, sizeToLimit) + "[@@Continue@@]";
                else
                    strReturnVal = stringToCut;

                if (encodeHtml)
                    strReturnVal = HttpContext.Current.Server.HtmlEncode(strReturnVal);

                strReturnVal = strReturnVal.Replace("[@@Continue@@]", "<strong>...</strong>");
            }

            return strReturnVal;
        }
        #endregion

        #region BindControl
        public static void BindControl(Control vobjList, object varrDataSource, string vstrDataTextField,
                                        string vstrDataValueField, BLL.BusinessObject.Constants.ControlType ctrlType, bool WithInitialSelect)
        {
            try
            {
                vobjList.Controls.Clear();
                switch (ctrlType)
                {
                    case BLL.BusinessObject.Constants.ControlType.DropDownList:
                        DropDownList ddl = (DropDownList)vobjList;
                        ddl.DataSource = varrDataSource;
                        ddl.DataTextField = vstrDataTextField;
                        ddl.DataValueField = vstrDataValueField;
                        ddl.DataBind();
                        if (WithInitialSelect)
                            ddl.Items.Insert(0, new ListItem("--Select--", "-1"));
                        break;
                    case BLL.BusinessObject.Constants.ControlType.CheckBoxList:
                        CheckBoxList chk = (CheckBoxList)vobjList;
                        chk.DataSource = varrDataSource;
                        chk.DataTextField = vstrDataTextField;
                        chk.DataValueField = vstrDataValueField;
                        chk.DataBind();
                        break;
                    case BLL.BusinessObject.Constants.ControlType.RadioButtonList:
                        RadioButtonList rdbtn = (RadioButtonList)vobjList;
                        rdbtn.DataSource = varrDataSource;
                        rdbtn.DataTextField = vstrDataTextField;
                        rdbtn.DataValueField = vstrDataValueField;
                        rdbtn.DataBind();
                        break;
                    case BLL.BusinessObject.Constants.ControlType.ListBox:
                        ListBox lstBox = (ListBox)vobjList;
                        lstBox.DataSource = varrDataSource;
                        lstBox.DataTextField = vstrDataTextField;
                        lstBox.DataValueField = vstrDataValueField;
                        lstBox.DataBind();
                        if (WithInitialSelect)
                            lstBox.Items.Insert(0, new ListItem("--Select--", "-1"));
                        break;
                    default:
                        break;

                }


            }
            catch (Exception ex)
            {
                string s = ex.Message.ToString();
            }

        }
        #endregion


        #region GetMailContent
        public static string GetMailContent(string vstrTemplate, string vstrMailContent)
        {

            string strTemplateFilePath = System.Web.HttpContext.Current.Server.MapPath("~/EmailBodyContent/" + vstrTemplate);
            string strMailContentFilePath = System.Web.HttpContext.Current.Server.MapPath("~/EmailBodyContent/" + vstrMailContent);

            string strTemplateText = System.IO.File.ReadAllText(strTemplateFilePath);
            string strBodyText = System.IO.File.ReadAllText(strMailContentFilePath);
            string strContactMail = ConfigurationManager.AppSettings.Get("ContactMail");
            strTemplateText = strTemplateText.Replace("[[BODY]]", strBodyText);
            strTemplateText = strTemplateText.Replace("[[CONTACTMAIL]]", strContactMail);
            return strTemplateText;

        }

        #endregion

        #region ReplaceCarriageReturnWithBR
        public static string ReplaceCarriageReturnWithBR(string vstrData)
        {
            Regex regx = new Regex(@"(\r\n|\r|\n)+");
            string strNewText = regx.Replace(vstrData, "<br /><br />");
            return strNewText;
        }
        #endregion

        public static bool SetCookie(string cookiename, string cookievalue, int iDaysToExpire)
        {
            try
            {
                HttpCookie objCookie = new HttpCookie(cookiename);
                // Response.Cookies.Clear();
                HttpContext.Current.Response.Cookies.Add(objCookie);
                objCookie.Values.Add(cookiename, cookievalue);
                DateTime dtExpiry = DateTime.Now.AddDays(iDaysToExpire);
                HttpContext.Current.Response.Cookies[cookiename].Expires = dtExpiry;
            }
            catch (Exception ex)
            {
                // HandleError(ex);
                return false;
            }
            return true;
        }

        public static string GetCookie(string cookiename)
        {
            string cookyval = "";
            try
            {
                if (null != HttpContext.Current.Request.Cookies[cookiename])
                {
                    cookyval = HttpContext.Current.Request.Cookies[cookiename].Value;
                }
            }
            catch (Exception ex)
            {
                cookyval = "";
                //HandleError(ex);
            }
            return cookyval;
        }

        public static int GetQueryStringIntValue(string strName)
        {
            int QuerStringValue = 0;
            if (null != HttpContext.Current.Request.QueryString[strName])
            {
                int.TryParse(HttpContext.Current.Request.QueryString[strName].ToString(), out QuerStringValue);
            }
            return QuerStringValue;

        }

        public static string GetQueryStringValue(string strName)
        {
            string QuerStringValue = string.Empty;
            if (null != HttpContext.Current.Request.QueryString[strName])
            {
                QuerStringValue = HttpContext.Current.Request.QueryString[strName].ToString();
            }
            return QuerStringValue;

        }

        public static long GetViewStateIntValue(string strName, StateBag oStateBag)
        {
            long ViewStateValue = 0;
            if (null != oStateBag[strName])
            {
                long.TryParse(oStateBag[strName].ToString(), out ViewStateValue);
            }
            return ViewStateValue;
        }

        public static int GetViewStateSmallIntValue(string strName, StateBag oStateBag)
        {
            int ViewStateValue = 0;
            if (null != oStateBag[strName])
            {
                int.TryParse(oStateBag[strName].ToString(), out ViewStateValue);
            }
            return ViewStateValue;
        }

        public static string GetViewStateValue(string strName, StateBag oStateBag)
        {
            string ViewStateValue = string.Empty;
            if (null != oStateBag[strName])
            {
                ViewStateValue = oStateBag[strName].ToString();
            }
            return ViewStateValue;
        }

        public static string GetSessionValue(string strName)
        {
            string SessionValue = string.Empty;
            if (null != HttpContext.Current.Session[strName])
            {
                SessionValue = Convert.ToString(HttpContext.Current.Session[strName]);
            }
            return SessionValue;
        }

        #region GetFormattedValue

        public static string GetFormattedValue(string vstrColumnName, Dictionary<string, string> Format)
        {
            string strFormatString = string.Empty;
            string format = string.Empty;


            if (vstrColumnName.Length > 4 && vstrColumnName.Substring(0, 3) == "Y2_")
            {
                vstrColumnName = vstrColumnName.Substring(3);
            }

            if (Format != null)
            {
                if (Format.Count > 0)
                {
                    foreach (KeyValuePair<string, string> item in Format)
                    {
                        if (item.Key == vstrColumnName)
                        {
                            format = item.Value;
                            if (!string.IsNullOrEmpty(format))
                            {
                                strFormatString = format.Replace("{0:", "").Replace("}", "");
                                break;
                            }
                        }
                    }
                }
            }
            return strFormatString;

        }

        #endregion

        public static void SaveLogMessage(string strErrorMssage)
        {
            string strfilepath = ConfigurationSettings.AppSettings.Get("FilePath");
            strfilepath = System.Web.HttpContext.Current.Server.MapPath(strfilepath);
            StreamWriter sw = null;
            if (!File.Exists(strfilepath))
            {
                sw = File.CreateText(strfilepath);
            }
            else
            {
                sw = File.AppendText(strfilepath);
            }
            string ReadString = strErrorMssage + " DateTime : " +
              System.DateTime.Now;
            sw.WriteLine(ReadString);
            sw.WriteLine("----------------------------");
            sw.Flush();
            sw.Close();
        }

        public static void SaveLogMessage(string strErrorMssage, bool vblIsWindow)
        {
            string strfilepath = ConfigurationSettings.AppSettings.Get("FilePath");
            StreamWriter sw = null;
            if (!File.Exists(strfilepath))
            {
                sw = File.CreateText(strfilepath);
            }
            else
            {
                sw = File.AppendText(strfilepath);
            }
            string ReadString = strErrorMssage + " DateTime : " +
              System.DateTime.Now;
            sw.WriteLine(ReadString);
            sw.WriteLine("----------------------------");
            sw.Flush();
            sw.Close();
        }

        public static string SerializeBase64(object o)
        {
            // Serialize to a base 64 string
            byte[] bytes;
            long length = 0;
            MemoryStream ws = new MemoryStream();
            BinaryFormatter sf = new BinaryFormatter();
            sf.Serialize(ws, o);
            length = ws.Length;
            bytes = ws.GetBuffer();
            string encodedData = bytes.Length + ":" + Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None);
            return encodedData;
        }
        public static object DeserializeBase64(string s)
        {
            // We need to know the exact length of the string - Base64 can sometimes pad us by a byte or two
            int p = s.IndexOf(':');
            int length = Convert.ToInt32(s.Substring(0, p));

            // Extract data from the base 64 string!
            byte[] memorydata = Convert.FromBase64String(s.Substring(p + 1));
            MemoryStream rs = new MemoryStream(memorydata, 0, length);
            BinaryFormatter sf = new BinaryFormatter();
            object o = sf.Deserialize(rs);
            return o;
        }

        #region CSV Upload

        public static string UploadCSVFile(FileUpload objFileUpload, string strFileName, string strSubDirectory = "", bool blIsAppendTimeTick = true, bool blIsDeletePrevious = true)
        {
            string strUploadFilePath = HttpContext.Current.Server.MapPath("~/Upload/" + (string.IsNullOrWhiteSpace(strSubDirectory) ? string.Empty : strSubDirectory + "/"));

            if (blIsDeletePrevious)
            {
                string[] strFiles = Directory.GetFiles(strUploadFilePath, strFileName + "*.csv", SearchOption.TopDirectoryOnly);
                for (int intIdx = 0; intIdx < strFiles.Length; intIdx++)
                {
                    if (File.GetLastAccessTimeUtc(strFiles[intIdx]).CompareTo(DateTime.UtcNow.AddDays(-1)) < 0)
                        File.Delete(strFiles[intIdx]);
                }
            }

            if (blIsAppendTimeTick)
                strFileName += DateTime.UtcNow.Ticks.ToString() + ".csv";
            else
                strFileName += ".csv";

            strUploadFilePath += strFileName;
            objFileUpload.SaveAs(strUploadFilePath);

            return strUploadFilePath;
        }

        #endregion

        #region API Utility Methods

        public static Dictionary<string, string> GetDictionaryFromPostData(string vstrPostData, string vstrSeparator = "@@")
        {
            Dictionary<string, string> objDictionary = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(vstrPostData))
            {
                string[] lstPostData = vstrPostData.Split(new string[] { vstrSeparator }, StringSplitOptions.None);
                for (int idx = 0; idx < lstPostData.Length; idx++)
                {
                    int intIdx = lstPostData[idx].IndexOf('=');
                    if (intIdx > 0)
                        objDictionary.Add(lstPostData[idx].Substring(0, intIdx), lstPostData[idx].Substring(intIdx + 1));
                }
            }

            return objDictionary;
        }

        public static bool IsValidNumericListString(string vstrNumericList, string vstrSeparator = ",")
        {
            if (string.IsNullOrEmpty(vstrNumericList))
                return true;

            Regex objRegex = new Regex("^([0-9]+)(,([0-9]+))*$");
            return objRegex.IsMatch(vstrNumericList);
        }

        public static string GetDecodedJson(string vstrUnicodedJson)
        {
            string[] strUnicodeCh = { "\\u0027", "\\u003c", "\\u003e" };
            string[] strDecodeCh = { "'", "<", ">" };

            for (int i = 0; i < strUnicodeCh.Length; i++)
            {
                vstrUnicodedJson = vstrUnicodedJson.Replace(strUnicodeCh[i], strDecodeCh[i]);
            }
            return vstrUnicodedJson;
        }

        #endregion API Utility Methods
        public DataTable  GetConfigFileData(string vstrSourceTableName)
        {
            DataTable dtData = new DataTable();
            string strMailServerInfo = string.Empty;
            strMailServerInfo = @"xml\Config.xml";
            DataSet dsData = new DataSet();
            dsData.ReadXml(strMailServerInfo);
            if (null != dsData)
            {
                if (null != dsData.Tables[vstrSourceTableName])
                {
                    dtData = dsData.Tables[vstrSourceTableName];
                }
            }
            return dtData;
        }

       
    }
}
