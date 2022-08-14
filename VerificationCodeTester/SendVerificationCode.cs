using System.Net;
using System.Net.Mail;
using System.Configuration;
using System;

namespace Send_Email_Console_CS
{
    class SendVerificationCode
    {
        static void Main(string[] args)
        {
            #region MyRegion
            //try
            //{
            //    Console.WriteLine("Enter To Address:");
            //    string to = Console.ReadLine().Trim();

            //    Console.WriteLine("Enter Subject:");
            //    string subject = Console.ReadLine().Trim();

            //    Console.WriteLine("Enter Body:");
            //    string body = Console.ReadLine().Trim();

            //    using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["taleh29@outlook.com"], to))
            //    {
            //        mm.Subject = subject;
            //        mm.Body = body;
            //        mm.IsBodyHtml = false;
            //        SmtpClient smtp = new SmtpClient();
            //        smtp.Host = ConfigurationManager.AppSettings["smtp.gmail.com"];
            //        smtp.EnableSsl = true;
            //        NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["taleh29@outlook.com"], ConfigurationManager.AppSettings["MTT2906@"]);
            //        smtp.UseDefaultCredentials = true;
            //        smtp.Credentials = NetworkCred;
            //        smtp.Port = int.Parse(ConfigurationManager.AppSettings["587"]);
            //        Console.WriteLine("Sending Email......");
            //        smtp.Send(mm);
            //        Console.WriteLine("Email Sent.");
            //        System.Threading.Thread.Sleep(3000);
            //        Environment.Exit(0);
            //    }

            //    Console.ReadLine();
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex.Message);
            //    Console.ReadLine();
            //}
            #endregion


            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                MailMessage mail = new MailMessage();

                mail.To.Add("taleh.malikov.k201@teams.dia.edu.az");

                mail.From = new MailAddress("talehmelik29@gmail.com");
                mail.Body = "Hi";
                mail.Subject = "Test";
                mail.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient("smtp.mail.yahoo.com", 587);
                smtpClient.Credentials = new NetworkCredential("talehmelik29@gmail.com", "t1a2l3e4h5@", "gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);

                Console.WriteLine("Successfully sended");
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();

            }


        }
    }
}
