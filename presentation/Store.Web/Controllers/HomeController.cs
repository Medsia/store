using Microsoft.AspNetCore.Mvc;
using Store.Memory;
using Store.Web.Models;
using System.Diagnostics;

namespace Store.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IInfoRepository infoRepository;

        public HomeController(ICategoryRepository categoryRepository, IInfoRepository infoRepository)
        {
            this.categoryRepository = categoryRepository;
            this.infoRepository = infoRepository;
        }

        public IActionResult Index()
        {
            return View(categoryRepository.GetAllCategories());
        }

        public IActionResult Payment()
        {
            var content = infoRepository.GetPaymentInfo();
            return View("Info", content);
        }

        public IActionResult Delivery()
        {
            var content = infoRepository.GetDeliveryInfo();
            return View("Info", content);
        }

        public IActionResult Contacts()
        {
            var content = infoRepository.GetContactsInfo();
            return View("Info", content);
        }

        public IActionResult About()
        {
            var content = infoRepository.GetAboutInfo();
            return View("Info", content);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
