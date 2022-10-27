using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Web.App;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ProductService productService;
        private readonly AdminControlService adminControlService;
        private readonly CategoryService categoryService;
        private readonly ContentService contentService;
        private readonly AuthService authService;

        private readonly IWebHostEnvironment appEnvironment;

        public AdminController(ProductService productService, AdminControlService adminControlService,  
                                CategoryService categoryService, ContentService contentService, AuthService authService,
                                IWebHostEnvironment appEnvironment)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.adminControlService = adminControlService;
            this.contentService = contentService;
            this.authService = authService;
            this.appEnvironment = appEnvironment;
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
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                adminControlService.AddCategory(categoryName);
                TempData["message"] = string.Format("Добавлено");
            }
            else TempData["warn"] = string.Format("Поле \"Название\" не должно быть пустым");

            return RedirectToAction("Category");
        }

        [HttpPost]
        public async Task<IActionResult> CategoryImageEdit(IFormFile uploadedFile, int categoryId)
        {
            string message;
            if(contentService.IsImageValid(uploadedFile, out message))
            {
                string fileName = "CategoryImg_" + categoryId.ToString() + "_";
                string path = "/Img/Categories/" + fileName + uploadedFile.FileName;

                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                string oldImgLink;
                adminControlService.EditCategoryImage(categoryId, path, out oldImgLink);

                FileInfo fileInf = new FileInfo(appEnvironment.WebRootPath + oldImgLink);
                fileInf.Delete();

                TempData["message"] = message;
            }
            else TempData["warn"] = message;

            return RedirectToAction("Category");
        }

        [HttpPost]
        public IActionResult CategoryNameEdit(int categoryId, string categoryName)
        {
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                adminControlService.EditCategoryName(categoryId, categoryName);
                TempData["message"] = string.Format($"{categoryName} -> сохранено");
            }
            else TempData["warn"] = string.Format("Поле \"Название\" не должно быть пустым");

            return RedirectToAction("Category");
        }

        [HttpPost]
        public IActionResult CategoryDelete(int categoryId, string categoryName, string oldImgLink)
        {
            adminControlService.DeleteCategory(categoryId);

            FileInfo fileInf = new FileInfo(appEnvironment.WebRootPath + oldImgLink);
            fileInf.Delete();

            TempData["message"] = string.Format("Удалено -> " + categoryName);

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

        [HttpPost]
        public IActionResult ContactsEdit(string title, string location, string worktime, List<string> numbers, string additional)
        {
            adminControlService.EditContacts(title, location, worktime, numbers, additional);
            return RedirectToAction("InfoEdited");
        }

        [HttpPost]
        public IActionResult PaymentEdit(string title, List<string> options, string additional)
        {
            adminControlService.EditPayment(title, options, additional);
            return RedirectToAction("InfoEdited");
        }

        [HttpPost]
        public IActionResult DeliveryEdit(string title, List<string> options, string additional)
        {
            adminControlService.EditDelivery(title, options, additional);
            return RedirectToAction("InfoEdited");
        }

        [HttpPost]
        public IActionResult AboutEdit(string title, string description)
        {
            adminControlService.EditAbout(title, description);
            return RedirectToAction("InfoEdited");
        }


        private string GetLogin()
        {
            return User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
        }
        private bool IsMaster()
        {
            if (GetLogin() == "master")
                return true;

            return false;
        }


        public IActionResult Security()
        {
            if (IsMaster())
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePass(string oldPassword, string newPassword)
        {
            string login = GetLogin();

            if (!string.IsNullOrWhiteSpace(oldPassword) && !string.IsNullOrWhiteSpace(newPassword))
            {
                var userIsCorrect = await authService.UserIsCorrect(login, oldPassword);
                if (userIsCorrect)
                {
                    adminControlService.ChangePassword(login, newPassword);

                    TempData["message"] = string.Format("Изменения сохранены");
                }
                else TempData["warn"] = string.Format("Не правильный пароль!");
            }
            else TempData["warn"] = string.Format("Поля ввода не должны быть пустыми!");

            return View("Security");
        }


        public IActionResult AccountManagement()
        {
            if (IsMaster())
            {
                var model = authService.GetAllAccounts();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AccountAdd(string login, string password)
        {
            if (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(password))
            {
                adminControlService.CreateAccount(login, password);

                TempData["message"] = string.Format("Аккаунт добавлен -> " + login);
            }
            else TempData["warn"] = string.Format("Поля ввода не должны быть пустыми!");

            var model = authService.GetAllAccounts();
            return View("AccountManagement", model);
        }

        [HttpPost]
        public IActionResult AccountEdit(string login, string password)
        {
            if (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(password))
            {
                adminControlService.ChangePassword(login, password);

                TempData["message"] = string.Format("Изменения сохранены -> " + login);
            }
            else TempData["warn"] = string.Format("Поля ввода не должны быть пустыми!");

            var model = authService.GetAllAccounts();
            return View("AccountManagement", model);
        }

        [HttpPost]
        public IActionResult AccountDelete(string login, string confirmation)
        {
            if (confirmation == login)
            {
                if (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(confirmation))
                {
                    adminControlService.DeleteAccount(login);

                    TempData["message"] = string.Format("Удалено -> " + login);
                }
                else TempData["warn"] = string.Format("Поля ввода не должны быть пустыми!");
            }
            else TempData["warn"] = string.Format("Ошибка подтверждения!");

            var model = authService.GetAllAccounts();
            return View("AccountManagement", model);
        }
    }
}
