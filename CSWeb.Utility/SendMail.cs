using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Utility;


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
    }
}
