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

        public IActionResult Info(int id)
        {
            var content = infoRepository.GetInfoById(id);
            return View("Info", content);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
