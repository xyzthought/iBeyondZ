using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Text;


namespace Utility
{
    public class MailService
    {
        #region Public Properties

        private string mstrSMTPServer;
        private string mstrUserName;
        private string mstrPassword;
        private string mstrFrom;
        private string mstrFromFriendlyName;
        private string mstrTo;
        private string mstrSubject;
        private string mstrMailBody;
        private List<string> mstrCC;
        private List<string> mstrBcc;
        private List<string> mstrServerMailAttachmentFiles;
        private string mstrException;
        private MailPriority objPriority;

        [Description("Provide the SMTP Server name."), Category("Appearance")]
        public string SMTPServer
        {
            get { return mstrSMTPServer; }
            set { mstrSMTPServer = value; }
        }

        [Description("Provide the SMTP user name."), Category("Appearance")]
        public string UserName
        {
            get { return mstrUserName; }
            set { mstrUserName = value; }
        }

        [Description("Provide the SMTP password."), Category("Appearance")]
        public string Password
        {
            get { return mstrPassword; }
            set { mstrPassword = value; }
        }

        [Description("Provide the from email address."), Category("Appearance")]
        public string From
        {
            get { return mstrFrom; }
            set { mstrFrom = value; }
        }

        [Description("Provide the from friendly name. (optional) {When optional send String.Empty}"), Category("Appearance")]
        public string FromFriendlyName
        {
            get { return mstrFromFriendlyName; }
            set { mstrFromFriendlyName = value; }
        }

        [Description("Provide the destination email address."), Category("Appearance")]
        public string To
        {
            get { return mstrTo; }
            set { mstrTo = value; }
        }

        [Description("Provide the CC email address."), Category("Appearance")]
        public List<string> CC
        {
            get { return mstrCC; }
            set { mstrCC = value; }
        }

        [Description("Provide the Bcc email address."), Category("Appearance")]
        public List<string> Bcc
        {
            get { return mstrBcc; }
            set { mstrBcc = value; }
        }

        [Description("Provide the Server File Attachment Files."), Category("Appearance")]
        public List<string> ServerMailAttachmentFiles
        {
            get { return mstrServerMailAttachmentFiles; }
            set { mstrServerMailAttachmentFiles = value; }
        }

        [Description("Provide the mail subject."), Category("Appearance")]
        public string Subject
        {
            get { return mstrSubject; }
            set { mstrSubject = value; }
        }

        [Description("Provide the mail body."), Category("Appearance")]
        public string MailBody
        {
            get { return mstrMailBody; }
            set { mstrMailBody = value; }
        }

        [Description("Provide the mail Exception."), Category("Appearance")]
        public string MailException
        {
            get { return mstrException; }
            set { mstrException = value; }
        }

        [Description("Provide the mail Priority."), Category("Appearance")]
        public MailPriority Priority
        {
            get { return objPriority; }
            set { objPriority = value; }
        }

        #endregion

        [DescriptionAttribute("This method sends email. First set the properties and than call this method."), CategoryAttribute("Appearance")]
        public int SendMail()
        {
            int  intReturnValue = -1;
            string vstrSMTPServer = this.SMTPServer;
            string vstrUserName = this.UserName;
            string vstrPassword = this.Password;
            string vstrFrom = this.From;
            string vstrFromFriendlyName = this.FromFriendlyName;
            string vstrTo = this.To;
            string vstrSubject = this.Subject;
            string vstrMailBody = this.MailBody;
            string vstrException = string.Empty;
            MailPriority objMailPriority = this.Priority;

            if (vstrSMTPServer != string.Empty && vstrUserName != string.Empty && vstrPassword != string.Empty && vstrFrom != string.Empty && vstrTo != string.Empty && vstrSubject != string.Empty && vstrMailBody != string.Empty)
            {
                try
                {
                    System.Net.Mail.MailMessage objMailMessage = new System.Net.Mail.MailMessage();
                    
                    

                    // Set Priority
                    switch (this.Priority)
                    {
                        case MailPriority.High:
                            objMailMessage.Priority = MailPriority.High;
                            break;
                        case MailPriority.Low:
                            objMailMessage.Priority = MailPriority.Low;
                            break;
                        case MailPriority.Normal:
                            objMailMessage.Priority = MailPriority.Normal;
                            break;
                        default:
                            objMailMessage.Priority = MailPriority.Normal;
                            break;
                    }
                    
                    // Set Friendly Name
                    if (vstrFromFriendlyName != string.Empty)
                    {
                        objMailMessage.From = new MailAddress(vstrFrom, vstrFromFriendlyName);
                    }
                    else
                    {
                        objMailMessage.From = new MailAddress(vstrFrom);
                    }

                    // Set Subject
                    objMailMessage.Subject = vstrSubject;

                    // Set To Address
                    objMailMessage.To.Add(vstrTo);

                    // CC Block
                    if (CC != null)
                    {
                        if (CC.Count > 0)
                        {
                            for (int i = 0; i < CC.Count; i++)
                            {
                                objMailMessage.CC.Add(CC[i]);
                            }
                        }
                    }

                    // BCc Block
                    if (Bcc != null)
                    {
                        if (Bcc.Count > 0)
                        {
                            for (int i = 0; i < Bcc.Count; i++)
                            {
                                objMailMessage.Bcc.Add(Bcc[i]);
                            }
                        }
                    }

                    // Set Attachments
                    if (ServerMailAttachmentFiles != null)
                    {
                        if (ServerMailAttachmentFiles.Count > 0)
                        {
                            for (int i = 0; i < ServerMailAttachmentFiles.Count; i++)
                            {
                                //MemoryStream streamFile = new MemoryStream(UTF32Encoding.Default.GetBytes(ServerMailAttachmentFiles[i]));
                                //// Rewind the stream.   
                                //streamFile.Position = 0;
                                objMailMessage.Attachments.Add(new Attachment(ServerMailAttachmentFiles[i], ""));
                            }
                        }
                    }

                    System.Net.Mail.SmtpClient objClient = new System.Net.Mail.SmtpClient(vstrSMTPServer);
                    System.Net.NetworkCredential objCredential = new System.Net.NetworkCredential(vstrUserName, vstrPassword);
                    objClient.Credentials = objCredential;

                    // Set Mail body encoding
                    objMailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                    // Creation of Alternate View
                    System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                        (System.Text.RegularExpressions.Regex.Replace(vstrMailBody, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                    System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(vstrMailBody, null, "text/html");

                    objMailMessage.AlternateViews.Add(plainView);
                    objMailMessage.AlternateViews.Add(htmlView);

                    objClient.Send(objMailMessage);
                    intReturnValue = 1;
                }
                catch(Exception ex)
                {
                    string str = ex.Message.ToString();
                    this.MailException = str;
                    intReturnValue = -1;
                }
            }
            return intReturnValue;
        }

        public bool ValidateEmail(string mstrEmail)
        {
            bool blnReturn = false;
            if (Regex.IsMatch(mstrEmail, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                blnReturn = true;
            }
            return blnReturn;
        }
    }
}
