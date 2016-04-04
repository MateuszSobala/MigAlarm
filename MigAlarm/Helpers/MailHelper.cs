using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace MigAlarm.Helpers
{
    public class MailHelper
    {
        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        static MailHelper()
        {
            GmailHost = ConfigurationManager.AppSettings["GmailHost"];
            GmailPort = int.Parse(ConfigurationManager.AppSettings["GmailPort"]);
            GmailSSL = bool.Parse(ConfigurationManager.AppSettings["GmailSSL"]);
            GmailUsername = ConfigurationManager.AppSettings["GmailUsername"];
            GmailPassword = ConfigurationManager.AppSettings["GmailPassword"];
        }

        public void Send(string receiver, string subject, string body, bool isHtml)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);

            using (var message = new MailMessage(GmailUsername, receiver))
            {
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;
                smtp.Send(message);
            }
        }
    }
}