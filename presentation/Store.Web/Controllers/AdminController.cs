using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Web.App;

namespace Store.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProductService productService;
        private readonly AdminControlService adminControlService;
        private readonly CategoryService categoryService;

        public AdminController(ProductService productService, AdminControlService adminControlService,  
                                CategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.adminControlService = adminControlService;
        }

        public IActionResult Index()
        {
            //var model = productService.GetAll();
            return View();
        }

        public IActionResult ProductList()
        {
            var model = productService.GetAll();
            return View(model);
        }

        [HttpPost]
        public IActionResult ProductAdd(int productId, string title, int categoryId, decimal price, string description)
        {
            ProductModel productModel = new ProductModel
            {
                Id = productId,
                Title = title,
                Category = categoryService.GetById(categoryId),
                Description = description,
                Price = price,
            };

            if (productService.IsValid(productModel))
            {
                adminControlService.AddProduct(productModel);
                TempData["message"] = string.Format("Добавлено");

                return RedirectToAction("ProductList");
            }

            ViewBag.Categories = categoryService.GetAll();
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
                Category = categoryService.GetById(categoryId),
                Description = description,
                Price = price,
            };
            
            if (productService.IsValid(productModel))
            {
                adminControlService.EditProduct(productModel);
                TempData["message"] = string.Format("Изменения сохранены");

                return RedirectToAction("ProductList");
            }

            ViewBag.Categories = categoryService.GetAll();
            ViewBag.Mode = "ProductEdit";

            var model = productService.GetById(productId);

            return View("Product", model);
        }

        [HttpPost]
        public IActionResult ProductDelete(int productId)
        {
            adminControlService.DeleteProduct(productId);

            TempData["message"] = string.Format("Изменения сохранены");

            return RedirectToAction("ProductList");
        }


        public IActionResult Category()
        {
            var model = categoryService.GetAll();
            return View(model);
        }

        [HttpPost]
        public IActionResult CategoryAdd(string categoryName)
        {
            CategoryModel categoryModel = new CategoryModel
            {
                Name = categoryName,
            };

            if (categoryService.IsValid(categoryModel))
            {
                adminControlService.AddCategory(categoryModel);
                TempData["message"] = string.Format("Добавлено");
            }

            return RedirectToAction("Category");
        }

        [HttpPost]
        public IActionResult CategoryEdit(int categoryId, string categoryName)
        {
            CategoryModel categoryModel = new CategoryModel
            {
                Id = categoryId,
                Name = categoryName,
            };

            if (categoryService.IsValid(categoryModel))
            {
                adminControlService.EditCategory(categoryModel);
                TempData["message"] = string.Format("Изменения сохранены");
            }

            return RedirectToAction("Category");
        }

        [HttpPost]
        public IActionResult CategoryDelete(int categoryId, string categoryName)
        {
            adminControlService.DeleteCategory(categoryId);
            TempData["message"] = string.Format("Удалено: " + categoryName);

            return RedirectToAction("Category");
        }


        public IActionResult InfoList()
        {
            return View();
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
