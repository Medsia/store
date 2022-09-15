using Microsoft.AspNetCore.Mvc;
using Store.Web.App;

namespace Store.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProductService productService;
        private readonly IInfoRepository infoRepository;
        private readonly ICategoryRepository categoryRepository;

        public AdminController(ProductService productService, IInfoRepository infoRepository, ICategoryRepository categoryRepository)
        {
            this.productService = productService;
            this.infoRepository = infoRepository;
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var model = productService.GetAllByQuery(1);
            return View(model);
        }
        
        public IActionResult CategoryEdit(int? categoryId = null, string categoryName = "")
        {
            if (categoryId != null && string.IsNullOrEmpty(categoryName))
            {
                // some edit logic
            }

            var content = categoryRepository.GetAllCategories();
            return View(content);
        }

        public IActionResult CategoryDelete(int categoryId)
        {
            // some delete logic

            var content = categoryRepository.GetAllCategories();
            return View("CategoryEdit", content);
        }

        public IActionResult ContactsEdit()
        {
            var content = infoRepository.GetContactsInfo();
            return View("InfoEdit", content);
        }

        public IActionResult PaymentEdit()
        {
            var content = infoRepository.GetPaymentInfo();
            return View("InfoEdit", content);
        }

        public IActionResult DeliveryEdit()
        {
            var content = infoRepository.GetDeliveryInfo();
            return View("InfoEdit", content);
        }

        public IActionResult AboutEdit()
        {
            var content = infoRepository.GetAboutInfo();
            return View("InfoEdit", content);
        }

        public IActionResult AccountManagement()
        {
            return View();
        }

        public IActionResult Security()
        {
            return View();
        }
    }
}
