using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace Library.Core.Utils
{
    public static class MailKitUtil
    {
        public static int GenerateVerificationCode()
        {
            Random random = new Random();
            int code = random.Next(100000,1000000);
            return code;
        }
        public static void SendMail(string emailTo, string subject, string content)
        {
            using var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("eehmedov17@outlook.com"));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = content };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("eehmedov17@outlook.com", "ehmed123");
            smtp.Send(email);

            smtp.Disconnect(true);
        }

    }
}
