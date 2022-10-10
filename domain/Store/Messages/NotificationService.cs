using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System;

namespace Store.Messages
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "gamestore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @".\\store_emails";

        public static EmailSettings GetEmailSettings()
        {
            return new EmailSettings();
        }
    }
    public class NotificationService : INotificationService
    {
        //private EmailSettings emailSettings;

        //public NotificationService(EmailSettings settings)
        //{
        //    emailSettings = settings;
        //}
        public void SendConfirmationCode(string cellPhone, int code)
        {
            Debug.WriteLine("Cell phone: {0}, code: {1:0000}.", cellPhone, code);
        }

        public Task SendConfirmationCodeAsync(string cellPhone, int code)
        {
            Debug.WriteLine("Cell phone: {0}, code: {1:0000}.", cellPhone, code);

            return Task.CompletedTask;
        }

        public void StartProcess(Order order, ShippingDetails shippingInfo)
        {
            var emailSettings =EmailSettings.GetEmailSettings();
            //using (var client = new SmtpClient())
            //{
            //    var message = new MailMessage("from@at.my.domain", "to@at.my.domain");
            //    message.Subject = "Заказ #" + order.Id;

            //    var builder = new StringBuilder();
            //    foreach (var item in order.Items)
            //    {
            //        builder.Append("{0}, {1}", item.ProductId, item.Count);
            //        builder.AppendLine();
            //    }

            //    message.Body = builder.ToString();
            //    client.Send(message);
            //}
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("---")
                    .AppendLine("Товары:");

                foreach (var item in order.Items)
                        {
                            body.Append("{0}, {1}", item.ProductId, item.Count);
                            body.AppendLine();
                        }

                    body.AppendFormat("Общая стоимость: {0:c}", order.TotalPrice)
                    .AppendLine("---")
                    .AppendLine("Доставка:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Address)                   
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine("---")
                    .AppendFormat("Подарочная упаковка: {0}",
                        shippingInfo.GiftWrap ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       emailSettings.MailToAddress,		// Кому
                                       "Новый заказ отправлен!",		// Тема
                                       body.ToString()); 				// Тело письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }

        public Task StartProcessAsync(Order order, ShippingDetails shippingInfo)
        {
            StartProcess(order, shippingInfo);

            return Task.CompletedTask;
        }
    }
}
