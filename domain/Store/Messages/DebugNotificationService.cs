using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;


namespace Store.Messages
{
    public class DebugNotificationService : INotificationService
    {
        public void SendConfirmationCode(string cellPhone, int code)
        {
            Debug.WriteLine("Cell phone: {0}, code: {1:0000}.", cellPhone, code);
        }

        public Task SendConfirmationCodeAsync(string cellPhone, int code)
        {
            Debug.WriteLine("Cell phone: {0}, code: {1:0000}.", cellPhone, code);

            return Task.CompletedTask;
        }

        public void StartProcess(Order order)
        {
            //using (var smtpClient = new SmtpClient())
            //{
            //    smtpClient.EnableSsl = emailSettings.UseSsl;
            //    smtpClient.Host = emailSettings.ServerName;
            //    smtpClient.Port = emailSettings.ServerPort;
            //    smtpClient.UseDefaultCredentials = false;
            //    smtpClient.Credentials
            //        = new NetworkCredential(emailSettings.Username, emailSettings.Password);

            //    if (emailSettings.WriteAsFile)
            //    {
            //        smtpClient.DeliveryMethod
            //            = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            //        smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
            //        smtpClient.EnableSsl = false;
            //    }

            //    StringBuilder body = new StringBuilder()
            //        .AppendLine("Новый заказ обработан")
            //        .AppendLine("---")
            //        .AppendLine("Товары:");

            //    foreach (var line in cart.Lines)
            //    {
            //        var subtotal = line.Game.Price * line.Quantity;
            //        body.AppendFormat("{0} x {1} (итого: {2:c}",
            //            line.Quantity, line.Game.Name, subtotal);
            //    }

            //    body.AppendFormat("Общая стоимость: {0:c}", cart.ComputeTotalValue())
            //        .AppendLine("---")
            //        .AppendLine("Доставка:")
            //        .AppendLine(shippingInfo.Name)
            //        .AppendLine(shippingInfo.Line1)
            //        .AppendLine(shippingInfo.Line2 ?? "")
            //        .AppendLine(shippingInfo.Line3 ?? "")
            //        .AppendLine(shippingInfo.City)
            //        .AppendLine(shippingInfo.Country)
            //        .AppendLine("---")
            //        .AppendFormat("Подарочная упаковка: {0}",
            //            shippingInfo.GiftWrap ? "Да" : "Нет");

            //    MailMessage mailMessage = new MailMessage(
            //                           emailSettings.MailFromAddress,	// От кого
            //                           emailSettings.MailToAddress,		// Кому
            //                           "Новый заказ отправлен!",		// Тема
            //                           body.ToString()); 				// Тело письма

            //    if (emailSettings.WriteAsFile)
            //    {
            //        mailMessage.BodyEncoding = Encoding.UTF8;
            //    }

            //    smtpClient.Send(mailMessage);
            using (var client = new SmtpClient())
            {
                var message = new MailMessage("from@at.my.domain", "to@at.my.domain");
                message.Subject = "Заказ #" + order.Id;

                var builder = new StringBuilder();
                foreach (var item in order.Items)
                {
                    builder.Append("{0}, {1}", item.ProductId, item.Count);
                    builder.AppendLine();
                }

                message.Body = builder.ToString();
                client.Send(message);
            }
            Debug.WriteLine("Order ID {0}", order.Id);
            Debug.WriteLine("Delivery: {0}", (object)order.Delivery.Description);
            Debug.WriteLine("Payment: {0}", (object)order.Payment.Description);

        }
        public Task StartProcessAsync(Order order)
        {
            StartProcess(order);

            return Task.CompletedTask;
        }
    }
}
