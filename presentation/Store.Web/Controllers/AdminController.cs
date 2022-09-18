using Microsoft.AspNetCore.Mvc;
using Store.Web.App;

namespace Store.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProductService productService;
        private readonly AdminControlService adminControlService;
        private readonly IInfoRepository infoRepository;
        private readonly ICategoryRepository categoryRepository;

        public AdminController(ProductService productService, AdminControlService adminControlService,  
                                IInfoRepository infoRepository, ICategoryRepository categoryRepository)
        {
            this.productService = productService;
            this.infoRepository = infoRepository;
            this.categoryRepository = categoryRepository;
            this.adminControlService = adminControlService;
        }

        public IActionResult Index()
        {
            var model = productService.GetAllByQuery(1);
            return View(model);
        }

        public IActionResult InfoList()
        {
            var model = infoRepository.GetAllInfo();
            return View(model);
        }







        public IActionResult Category()
        {
            var content = categoryRepository.GetAllCategories();
            return View(content);
        }

        public IActionResult CategoryAdd(int categoryId, string categoryName)
        {
            var category = new Category(categoryId, categoryName);
            adminControlService.AddCategory(category);

            var content = categoryRepository.GetAllCategories();
            return View(content);
        }

        public IActionResult CategoryEdit(int categoryId, string categoryName)
        {
            var category = new Category(categoryId, categoryName);
            adminControlService.EditCategory(category);

            var content = categoryRepository.GetAllCategories();
            return View(content);
        }

        public IActionResult CategoryDelete(int categoryId, string categoryName)
        {
            var category = new Category(categoryId, categoryName);
            adminControlService.DeleteCategory(category);

            var content = categoryRepository.GetAllCategories();
            return View(content);
        }





        public IActionResult Info(int id)
        {
            var content = infoRepository.GetInfoById(id);
            return View("Info", content);
        }

        public IActionResult InfoEdit(int id, string title, string description)
        {
            var info = new Info(id, title, description);
            adminControlService.EditInfo(info);

            var content = infoRepository.GetInfoById(id);
            return View("Info", content);
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
