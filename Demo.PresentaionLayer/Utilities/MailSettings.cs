using System.Net;
using System.Net.Mail;

namespace Demo.PresentaionLayer.Utilities
{
    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipient { get; set; }

    }
    public class MailSettings
    {
        public static void SendEmail(Email email)
        {
            var client=new SmtpClient("smtp.gmail.com",587); 
            client.EnableSsl = true;

            var creds = new NetworkCredential("abdelrhmankamal3@gmail.com","qwfggiaihfh");

            client.Send("abdelrhmankamal3@gmail.com",email.Recipient,email.Subject,email.Body);

        }
    }
}
