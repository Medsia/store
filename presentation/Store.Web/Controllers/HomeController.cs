using Microsoft.AspNetCore.Mvc;
using Store.Web.App;
using Store.Web.Models;
using System.Diagnostics;

namespace Store.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CategoryService categoryService;
        private readonly ContentService contentService;

        public HomeController(CategoryService categoryService, ContentService contentService)
        {
            this.categoryService = categoryService;
            this.contentService = contentService;
        }

        public IActionResult Index()
        {
            var model = categoryService.GetAll();
            return View(model);
        }

        public IActionResult ContactsInfo()
        {
            var contactsSO = contentService.GetContacts();
            return View(contactsSO);
        }

        public IActionResult PaymentInfo()
        {
            var paymentSO = contentService.GetPayment();
            return View(paymentSO);
        }

        public IActionResult DeliveryInfo()
        {
            var deliverySO = contentService.GetDelivery();
            return View(deliverySO);
        }

        public IActionResult AboutInfo()
        {
            var aboutSO = contentService.GetAbout();
            return View(aboutSO);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
