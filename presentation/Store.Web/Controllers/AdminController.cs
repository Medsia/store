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
            var model = productService.GetAll();
            return View(model);
        }

        public IActionResult InfoList()
        {
            var model = infoRepository.GetAllInfo();
            return View(model);
        }




        public IActionResult Product(int productId)
        {
            ViewBag.Categories = categoryRepository.GetAllCategories();

            var model = productService.GetById(productId);
            return View(model);
        }

        public IActionResult ProductAdd(int productId, string title, int categoryId, decimal price, string description)
        {
            Product product = new Product(productId, title, categoryId, description, price);
            adminControlService.AddProduct(product);

            ViewBag.Categories = categoryRepository.GetAllCategories();

            var model = productService.GetAll();
            return View("Product", model);
        }

        public IActionResult ProductEdit(int productId, string title, int categoryId, decimal price, string description)
        {
            Product product = new Product(productId, title, categoryId, description, price);
            adminControlService.EditProduct(product);

            ViewBag.Categories = categoryRepository.GetAllCategories();

            var model = productService.GetById(productId);
            return View("Product", model);
        }

        public IActionResult ProductDelete(int productId)
        {
            //adminControlService.DeleteProduct();

            ViewBag.Categories = categoryRepository.GetAllCategories();

            var model = productService.GetAll();
            return View("Product", model);
        }





        public IActionResult Category()
        {
            var model = categoryRepository.GetAllCategories();
            return View(model);
        }

        public IActionResult CategoryAdd(int categoryId, string categoryName)
        {
            var category = new Category(categoryId, categoryName);
            adminControlService.AddCategory(category);

            var model = categoryRepository.GetAllCategories();
            return View("Category", model);
        }

        public IActionResult CategoryEdit(int categoryId, string categoryName)
        {
            var category = new Category(categoryId, categoryName);
            adminControlService.EditCategory(category);

            var model = categoryRepository.GetAllCategories();
            return View("Category", model);
        }

        public IActionResult CategoryDelete(int categoryId, string categoryName)
        {
            adminControlService.ResetCategoryIdInProducts(categoryId);

            var category = new Category(categoryId, categoryName);
            adminControlService.DeleteCategory(category);

            var model = categoryRepository.GetAllCategories();
            return View("Category", model);
        }


        public IActionResult Info(int id)
        {
            var model = infoRepository.GetInfoById(id);
            return View("Info", model);
        }

        public IActionResult InfoEdit(int id, string title, string description)
        {
            var info = new Info(id, title, description);
            adminControlService.EditInfo(info);

            var model = infoRepository.GetInfoById(id);
            return View("Info", model);
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
