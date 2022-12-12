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
            string code = random.Next(10000000, 100000000).ToString();
            return code;
        }
        public static void SendMail(string emailTo, string subject, string content)
        {
            string emailFrom = "noreply@dia.edu.az";
            using var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailFrom));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = "Verification code: " + content };

            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Connect("smtp.yandex.com", 465, SecureSocketOptions.StartTls);
            //smtp.Connect("smtp.live.com", 587, SecureSocketOptions.StartTls);
            //smtp.Connect("smtp.mail.yahoo.com", 465, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailFrom, "D@t@center64@!");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

    }
}
