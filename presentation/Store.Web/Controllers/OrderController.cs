using Microsoft.AspNetCore.Mvc;
using Store.Web.Models;
using Store.Contractors;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Store.Web.Contractors;
using Store.Web.App;
using System;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService orderService;
        private readonly IEnumerable<IDeliveryService> deliveryServices;
        private readonly IEnumerable<IPaymentService> paymentServices;
        private readonly IEnumerable<IWebContractorService> webContractorServices;
        public OrderController(OrderService orderService,
                                IEnumerable<IDeliveryService> deliveryServices,
                                IEnumerable<IPaymentService> paymentServices,
                                IEnumerable<IWebContractorService> webContractorServices)
        {
            this.orderService = orderService;
            this.deliveryServices = deliveryServices;
            this.paymentServices = paymentServices;
            this.webContractorServices = webContractorServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var (hasValue, model) = await orderService.TryGetModelAsync();
            if (hasValue)
                return View(model);

            return View("Empty");
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int productId, int count = 1)
        {
            await orderService.AddProductAsync(productId, count);

            return RedirectToAction("Index", "Product", new { id = productId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItem(int productId, int count)
        {
            var model = await orderService.UpdateProductAsync(productId, count);

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var model = await orderService.RemoveProductAsync(productId);

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> SendConfirmation(string cellPhone)
        {
            var model = await orderService.SendConfirmationAsync(cellPhone);

            return View("Confirmation", model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmCellPhone(string cellPhone, int confirmationCode)
        {
            var model = await orderService.ConfirmCellPhoneAsync(cellPhone, confirmationCode);

            if(model.Errors.Count > 0)
                return View("Confirmation", model);

            var deliveryMethods = deliveryServices.ToDictionary(service => service.Name, service => service.Title);

            return View("DeliveryMethod", deliveryMethods);
        }

        [HttpPost]
        public async Task<IActionResult> StartDelivery(string serviceName)
        {
            var deliveryService = deliveryServices.Single(service => service.Name == serviceName);
            var order = await orderService.GetOrderAsync();
            var form = deliveryService.FirstForm(order);

            var webContractorservice = webContractorServices.SingleOrDefault(service => service.Name == serviceName);
            if (webContractorservice == null)
                return View("DeliveryStep", form);

            var returnUri = GetReturnUri(nameof(NextDelivery));
            var redirectUri = await webContractorservice.StartSessionAsync(form.Parameters, returnUri);

            return Redirect(redirectUri.ToString());
        }

        private Uri GetReturnUri(string action)
        {
            var builder = new UriBuilder(Request.Scheme, Request.Host.Host)
            {
                Path = Url.Action(action),
                Query = null,
            };

            if (Request.Host.Port != null)
                builder.Port = Request.Host.Port.Value;

            return builder.Uri;
        }

        [HttpPost]
        public async Task<IActionResult> NextDelivery(string serviceName, int step, Dictionary<string, string> values)
        {
            var deliveryService = deliveryServices.Single(service => service.Name == serviceName);

            var form = deliveryService.NextForm(step, values);
            if (!form.IsFinal)
                return View("deliveryStep", form);

            var delivery = deliveryService.GetDelivery(form);
            await orderService.SetDeliveryAsync(delivery);

            var paymentMethods = paymentServices.ToDictionary(service => service.Name, service => service.Title);

            return View("PaymentMethod", paymentMethods);
        }

        [HttpPost]
        public async Task<IActionResult> StartPayment(string serviceName)
        {
            var paymentService = paymentServices.Single(service => service.Name == serviceName);
            var order = await orderService.GetOrderAsync();
            var form = paymentService.FirstForm(order);

            var webContractorservice = webContractorServices.SingleOrDefault(service => service.Name == serviceName);
            if (webContractorservice == null)
                return View("PaymentStep", form);

            var returnUri = GetReturnUri(nameof(NextPayment));
            var redirectUri = await webContractorservice.StartSessionAsync(form.Parameters, returnUri);

            return Redirect(redirectUri.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> NextPayment(string serviceName, int step, Dictionary<string, string> values)
        {
            var paymentService = paymentServices.Single(service => service.Name == serviceName);

            var form = paymentService.NextForm(step, values);
            if (!form.IsFinal)
                return View("PaymentStep", form);

            var payment = paymentService.GetPayment(form);
            var model = await orderService.SetPaymentAsync(payment);

            return View("Finish", model);
        }
    }
}
