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

        public IActionResult Info(int id)
        {
            switch (id)
            {
                case 1: var contactsSO = contentService.GetContacts();  return View("ContactsInfo", contactsSO);
                case 2: var paymentSO = contentService.GetPayment();  return View("PaymentInfo", paymentSO);
                case 3: var deliverySO = contentService.GetDelivery();  return View("DeliveryInfo", deliverySO);
                case 4: var aboutSO = contentService.GetAbout();  return View("AboutInfo", aboutSO);
                default: return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
