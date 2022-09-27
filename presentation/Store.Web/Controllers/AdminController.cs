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


        [HttpPost]
        public IActionResult ProductAdd(int productId, string title, int categoryId, decimal price, string description)
        {
            ProductModel productModel = new ProductModel
            {
                Id = productId,
                Title = title,
                Category = categoryRepository.GetCategoryById(categoryId),
                Description = description,
                Price = price,
            };

            if (productService.IsValid(productModel))
            {
                adminControlService.AddProduct(productModel);
                TempData["message"] = string.Format("Добавлено");

                return RedirectToAction("Index");
            }

            ViewBag.Categories = categoryRepository.GetAllCategories();
            ViewBag.Mode = "ProductAdd";

            return View("Product", productModel);
        }

        [HttpPost]
        public IActionResult ProductEdit(int productId, string title, int categoryId, decimal price, string description)
        {
            ProductModel productModel = new ProductModel
            {
                Id = productId,
                Title = title,
                Category = categoryRepository.GetCategoryById(categoryId),
                Description = description,
                Price = price,
            };
            
            if (productService.IsValid(productModel))
            {
                adminControlService.EditProduct(productModel);
                TempData["message"] = string.Format("Изменения сохранены");

                return RedirectToAction("Index");
            }

            ViewBag.Categories = categoryRepository.GetAllCategories();
            ViewBag.Mode = "ProductEdit";

            var model = productService.GetById(productId);

            return View("Product", model);
        }

        [HttpPost]
        public IActionResult ProductDelete(int productId)
        {
            adminControlService.DeleteProduct(productService.GetById(productId));

            TempData["message"] = string.Format("Изменения сохранены");

            return RedirectToAction("Index");
        }


        public IActionResult Category()
        {
            var model = categoryRepository.GetAllCategories();
            return View(model);
        }

        [HttpPost]
        public IActionResult CategoryAdd(int categoryId, string categoryName)
        {
            var category = new Category(categoryId, categoryName);
            adminControlService.AddCategory(category);

            var model = categoryRepository.GetAllCategories();
            return View("Category", model);
        }

        [HttpPost]
        public IActionResult CategoryEdit(int categoryId, string categoryName)
        {
            var category = new Category(categoryId, categoryName);
            adminControlService.EditCategory(category);

            var model = categoryRepository.GetAllCategories();
            return View("Category", model);
        }

        [HttpPost]
        public IActionResult CategoryDelete(int categoryId, string categoryName)
        {
            var category = new Category(categoryId, categoryName);
            adminControlService.DeleteCategory(category);

            var model = categoryRepository.GetAllCategories();
            return View("Category", model);
        }


        [HttpPost]
        public IActionResult Info(int id)
        {
            var model = infoRepository.GetInfoById(id);
            return View("Info", model);
        }

        [HttpPost]
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
