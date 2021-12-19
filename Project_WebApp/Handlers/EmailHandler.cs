using System.Net;
using System.Net.Mail;

namespace Project_WebApp.Handlers
{
    public class EmailHandler
    {
        public void SendMail(string emailTo, string nameTo, string messageSubject, string messageBody)
        {
            //Send mail & open login view
            SmtpClient smtpClient = new SmtpClient("send.one.com", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("school@yarimarien.be", "School123_"),
            };
            MailAddress emailSender = new MailAddress("school@yarimarien.be", "Project Web Apps");
            MailAddress emailReceiver = new MailAddress(emailTo, nameTo);
            MailMessage mailMessage = new MailMessage(emailSender, emailReceiver)
            {
                Subject = "Email test",
                Body = messageBody,
                IsBodyHtml = true
            };
            smtpClient.Send(mailMessage);
        }
    }
}
