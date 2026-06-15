using System.Net;
using System.Net.Mail;


namespace Bank_Account.Services
{
    public class EmailService
    {
        public void sendEmail(string toEmail , string subject ,string body  )
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("testheaven617@gmail.com", "hzjh fnrt izpq yewr"),
                EnableSsl = true
            };

            MailMessage msg = new MailMessage("testheaven617@gmail.com", toEmail, subject, body);
            client.Send(msg);
        
        }
    }
}
