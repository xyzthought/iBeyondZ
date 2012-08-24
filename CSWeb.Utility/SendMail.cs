using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Utility;
using System.Web.Mail;
using System.Configuration;

namespace CSWeb.Utility
{
    public class SendMail
    {
        #region Mail Message
        public static void MailMessage(string strSubject, string strMessage)
        {
            try
            {
                Common objCommon=new Common();
                DataTable dtData = objCommon.GetConfigFileData("Others");

                if (null != dtData)
                {
                    MailService objMail = new MailService();
                    objMail.UserName = dtData.Rows[0]["UID"].ToString();
                    objMail.Password = dtData.Rows[0]["Pwd"].ToString();
                    objMail.SMTPServer = dtData.Rows[0]["SmtpClient"].ToString();
                    objMail.FromFriendlyName = dtData.Rows[0]["FromFriendlyName"].ToString();
                    objMail.Priority = System.Net.Mail.MailPriority.Low;
                    objMail.From = dtData.Rows[0]["EmailFrom"].ToString();
                    objMail.To = dtData.Rows[0]["ErrorEmailTo"].ToString();
                    objMail.Subject = strSubject;
                    objMail.MailBody = strMessage;

                    if (strSubject.ToLower().Contains("success") && dtData.Rows[0]["IsSuccessfullMessageRequired"].ToString().ToLower() == "yes")
                    {
                        int intretvalue = objMail.SendMail();
                        string strOutPut = objMail.MailException;
                    }
                    else if (strSubject.ToLower().Contains("error"))
                    {
                        int intretvalue = objMail.SendMail();
                        string strOutPut = objMail.MailException;
                    }
                }
            }
            catch (Exception ex)
            {
                string strException = ex.Message.ToString();
            }
        }
        #endregion Mail Message


        #region SendExceptionMail
        public static void SendExceptionMail(string strMessage, string strSource, string strStackTrace)
        {
            try
            {
                string strFromMailId = ConfigurationManager.AppSettings.Get("EmailFrom");
                string strMailServer = ConfigurationManager.AppSettings.Get("SmtpClient");
                string strMailUser = ConfigurationManager.AppSettings.Get("UID");
                string strMailPwd = ConfigurationManager.AppSettings.Get("Pwd");
                string vstrSubject = "CSWeb Integration - Exception";
                string vstrBody = "<b>Message: </b>" + strMessage + "<br/><br/><b>Sourse: </b>" + strSource + "<br/><br/><b>StackTrace: </b>" + strStackTrace;
                string strMails = ConfigurationManager.AppSettings.Get("ErrorEmailTo");
                MailMessage objMessage = new MailMessage();

                if (strMailServer != "")
                {
                    SmtpMail.SmtpServer = strMailServer;
                    objMessage.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
                    objMessage.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = strMailUser;
                    objMessage.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = strMailPwd;
                }

                objMessage.From = strFromMailId;
                objMessage.To = strMails;
                objMessage.Subject = vstrSubject;
                objMessage.Body = vstrBody;
                objMessage.BodyFormat = MailFormat.Html;
                SmtpMail.Send(objMessage);
            }
            catch (Exception exc)
            {

            }
        }
        #endregion
    }
}
