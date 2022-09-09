using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net.Mail;
using System.Net;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Library.Core.Utils
{
    public static class MailKitUtil
    {

        public static string GenerateVerificationCode()
        {
            Random random = new Random();
            string code = random.Next(100000, 1000000).ToString();
            return code;
        }
        public static void SendMail(string emailTo, string subject, string content)
        {
            string emailFrom = "taleh29@outlook.com";
            using var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailFrom));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = "Verification code: " + content };

            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            //smtp.Connect("smtp.live.com", 587, SecureSocketOptions.StartTls);
            //smtp.Connect("smtp.mail.yahoo.com", 465, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailFrom, "t1a2l3e4h5@");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        //public static void SendMail(string emailTo, string subject, string content)
        //{
        //    try
        //    {
        //        MailMessage message = new MailMessage();
        //        SmtpClient smtp = new SmtpClient();
        //        message.From = new MailAddress("talehmalikov@yahoo.com");
        //        message.To.Add(new MailAddress(emailTo));
        //        message.Subject = "Verification code";
        //       // message.IsBodyHtml = true; //to make message body as html  
        //        message.Body = "Verification code: " + content;
        //        smtp.Port = 465;
        //        smtp.Host = "smtp.mail.yahoo.com"; //for gmail host  
        //        smtp.EnableSsl = true;
        //        smtp.UseDefaultCredentials = false;
        //        smtp.Credentials = new NetworkCredential("talehmalikov@yahoo.com", "Mtt2906@");
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.Send(message);
        //    }
        //    catch { }
        //}

    }
}
