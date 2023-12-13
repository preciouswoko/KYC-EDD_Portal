using KYC_EDDPortal.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace KYC_EDDPortal.Services
{
    public class EmailService : IEmailService
    {
        private static ILogger<EmailService> _logging;

         private readonly IConfiguration _configuration;
        public EmailService(ILogger<EmailService> logging, IConfiguration configuration)
        {
            _logging = logging;
           _configuration = configuration;
        }
       

        public void SmtpSendMail(string strTo, string StrBody, string StrSubject, string strFrom = null)
        {
            if (strFrom == null)
            {
                strFrom = _configuration["Email:SourceEmail"]; 
            };
            // return;
            new Thread(() =>
            {
                // string strSMTP = _configuration["AppSettings:MailServer"];
                string strSMTP = _configuration["Email:MailServer"];  /*GetConfigValue("MailServer");*/
                SmtpClient client = new SmtpClient(strSMTP.Trim(), 25)
                {
                    // Configure the SmtpClient with the credentials used to connect
                    // to the SMTP server.
                    Credentials = new NetworkCredential()//"user@somecompany.com", "password");
                };
                // Create the MailMessage to represent the e-mail being sent.
                try
                {



                    using (MailMessage msg = new MailMessage())
                    {
                        // strFrom = /*string.IsNullOrWhiteSpace(strFrom)*/ /*? $"{Helper.GetConfigValue("SenderName")}<{Util.Helper.GetConfigValue("SenderEmail")}>" : */ strFrom;





                        // Configure the e-mail sender and subject.
                        msg.IsBodyHtml = true;
                        msg.From = new MailAddress(strFrom);



                        msg.Subject = StrSubject;
                        //msg.Body = "<html>" + "<font size='4'  face='Book Antiqua'>Please find attached the Cheque & Cash inflow and Outflow report for " + strDate + "." + " " + "Kindly disseminate to the appropriate business head.<br><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p> Best Regards,</font>" + "</html>";
                        msg.Body = StrBody;



                        msg.To.Add(new MailAddress(strTo));
                        //if (strFileAttachment != null)
                        //{
                        //    string[] arrayAttach = strFileAttachment.Split(',');
                        //    foreach (string strAttach in arrayAttach)
                        //    {
                        //        msg.Attachments.Add(new Attachment(strAttach, "application/vnd.ms-excel"));
                        //    }
                        //}
                        //for (int i = 0; i < strTo.Length; i++)
                        //{
                        //    msg.To.Add(new MailAddress(strTo[i].ToString()));
                        //}



                        //if (strCc != null)
                        //{
                        //    for (int i = 0; i < strCc.Length; i++)
                        //    {
                        //        msg.CC.Add(new MailAddress(strCc[i].ToString()));
                        //    }



                        //}
                        string strCc = _configuration["Email:CC"];
                        if (strCc != null)
                        {
                            string[] ccAddresses = strCc.Split(',');
                            foreach (string ccAddress in ccAddresses)
                            {
                                msg.CC.Add(new MailAddress(ccAddress.Trim()));
                            }
                        }


                        client.Send(msg);
                        _logging.LogInformation($"Message {StrSubject}) Sent to: {strTo}", "SmtpSendMail");



                    }
                }
                catch (SmtpException ex)
                {
                    _logging.LogError(string.Format("SmtpException from SmtpSendMail {0}", ex.Message + " " + ex.Source.ToString() + " " + ex.StackTrace), "SmtpSendMail");
                    //  throw new Exception(ex.Message + " " + ex.Source.ToString() + " " + ex.StackTrace);
                }
                catch (Exception ex)
                {
                    _logging.LogError(string.Format("Exception from SmtpSendMail {0}", ex.Message + " " + ex.Source.ToString() + " " + ex.StackTrace), "SmtpSendMail");
                    // throw new Exception(ex.Message + " " + ex.Source.ToString() + " " + ex.StackTrace);
                }
            }).Start();
        }
    }
}
