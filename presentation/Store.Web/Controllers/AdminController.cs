using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Web.App;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProductService productService;
        private readonly AdminControlService adminControlService;
        private readonly CategoryService categoryService;
        private readonly ContentService contentService;

        public AdminController(ProductService productService, AdminControlService adminControlService,  
                                CategoryService categoryService, ContentService contentService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.adminControlService = adminControlService;
            this.contentService = contentService;
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
                Category = categoryService.GetByIdAsync(categoryId).Result,
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
                Category = categoryService.GetByIdAsync(categoryId).Result,
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

            var model = productService.GetByIdAsync(productId).Result;

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


        public IActionResult InfoList(int id)
        {
            switch (id)
            {
                case 1: var contactsSO = contentService.GetContacts(); return View("ContactsInfo", contactsSO);
                case 2: var paymentSO = contentService.GetPayment(); return View("PaymentInfo", paymentSO);
                case 3: var deliverySO = contentService.GetDelivery(); return View("DeliveryInfo", deliverySO);
                case 4: var aboutSO = contentService.GetAbout(); return View("AboutInfo", aboutSO);
                default: return View();
            }
        }

        public IActionResult InfoEdited()
        {
            TempData["message"] = string.Format("Изменения сохранены");
            return View("InfoList");
        }

        public IActionResult ContactsEdit(string title, string location, string worktime, List<string> numbers, string additional)
        {
            adminControlService.EditContacts(title, location, worktime, numbers, additional);
            return RedirectToAction("InfoEdited");
        }

        public IActionResult PaymentEdit(string title, List<string> options, string additional)
        {
            adminControlService.EditPayment(title, options, additional);
            return RedirectToAction("InfoEdited");
        }

        public IActionResult DeliveryEdit(string title, List<string> options, string additional)
        {
            adminControlService.EditDelivery(title, options, additional);
            return RedirectToAction("InfoEdited");
        }

        public IActionResult AboutEdit(string title, string description)
        {
            adminControlService.EditAbout(title, description);
            return RedirectToAction("InfoEdited");
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
