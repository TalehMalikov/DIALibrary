using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace Library.Core.Utils
{
    public static class MailKitUtil
    {
        public static string GenerateVerificationCode()
        {
            Random random = new Random();
            string code = random.Next(100000,1000000).ToString();
            return code;
        }
        public static void SendMail(string emailTo, string subject, string content)
        {
            string emailFrom = "eehmedov17@outlook.com";
            using var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailFrom));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = content };

            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.SslOnConnect);
            //smtp.Connect("smtp.live.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailFrom, "ehmed123");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

    }
}
