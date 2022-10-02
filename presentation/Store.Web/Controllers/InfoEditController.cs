using Microsoft.AspNetCore.Mvc;
using Store.Web.App;

namespace Store.Web.Controllers
{
    public class InfoEditController : Controller
    {
        public IActionResult ContactsInfo()
        {
            return View();
        }
        public IActionResult PaymentInfo()
        {
            return View();
        }
        public IActionResult DeliveryInfo()
        {
            return View();
        }
        public IActionResult AboutInfo()
        {
            return View();
        }
    }
}
