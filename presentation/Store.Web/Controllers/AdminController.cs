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

        private readonly string WebRootPath;

        public AdminController(ProductService productService, AdminControlService adminControlService,  
                                CategoryService categoryService, ContentService contentService, AuthService authService,
                                IWebHostEnvironment appEnvironment)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.adminControlService = adminControlService;
            this.contentService = contentService;
            this.authService = authService;
            this.WebRootPath = appEnvironment.WebRootPath;
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


        public async Task<IActionResult> Product(int productId)
        {
            ProductModel productModel = await productService.GetEmptyModelAsync();

            ViewBag.Categories = categoryService.GetAll();

            ViewBag.Images = await contentService.GetAllImagesByProdIdAsync(productId);
            ViewBag.EmptyImage = ContentService.EmptyImageLink;

            if (productId == 0) ViewBag.EditMode = false;
            else
            {
                ViewBag.EditMode = true;
                productModel = productService.GetByIdAsync(productId).Result;
            }

            return View(productModel);
        }


        [HttpPost]
        public async Task<IActionResult> ProductAdd(string title, int categoryId, decimal price, string description,
                                                        IFormFile uploadedThumbnail, List<IFormFile> uploadedImages)
        {
            string message = "";

            ProductModel productModel = new ProductModel
            {
                Title = title,
                Category = categoryService.GetByIdAsync(categoryId).Result,
                Description = description,
                Price = price,
            };

            if (productService.IsValid(productModel, out message))
            {
                adminControlService.AddProduct(productModel);
                TempData["message"] = string.Format("Добавлено -> " + title);

                var createdProduct = await productService.GetLastCreated();
                if (contentService.IsImageValid(uploadedThumbnail, out message))
                {
                    await adminControlService.EditProductThumbnail(uploadedThumbnail, createdProduct.Id, WebRootPath);
                }
                else TempData["warn"] += $"\n Превью: " + message;

                int counter = 1;
                foreach (var image in uploadedImages)
                {
                    if (contentService.IsImageValid(image, out message))
                    {
                        await adminControlService.EditProductImage(image, createdProduct.Id, counter, WebRootPath);
                    }
                    else TempData["warn"] += $"\n Файл {counter}: " + message;
                    counter++;
                }

                return RedirectToAction("ProductList", "Admin");
            }
            else
            {
                TempData["error"] += message;
                return RedirectToAction("Product", "Admin", new { productId=0 });
            }
        }


        [HttpPost]
        public async Task<IActionResult> ProductEdit(int productId, string title, int categoryId, decimal price, string description,
                                                        IFormFile uploadedThumbnail, List<IFormFile> uploadedImages)
        {
            string message = "";

            ProductModel productModel = new ProductModel
            {
                Id = productId,
                Title = title,
                Category = categoryService.GetByIdAsync(categoryId).Result,
                Description = description,
                Price = price,
            };

            if (productService.IsValid(productModel, out message))
            {
                adminControlService.EditProduct(productModel);
                TempData["message"] = string.Format("Изменения сохранены");
            }
            else TempData["error"] += message;

            if (contentService.IsImageValid(uploadedThumbnail, out message))
            {
                await adminControlService.EditProductThumbnail(uploadedThumbnail, productId, WebRootPath);
            }
            else TempData["warn"] += $"\n Превью: " + message;

            int counter = 1;
            foreach(var image in uploadedImages)
            {
                if (contentService.IsImageValid(image, out message))
                {
                    await adminControlService.EditProductImage(image, productId, counter, WebRootPath);
                }
                else TempData["warn"] += $"\n Файл {counter}: " + message;
                counter++;
            }

            return RedirectToAction("Product", "Admin", new { productId });
        }


        [HttpPost]
        public async Task<IActionResult> ProductDelete(int productId, string title)
        {
            await adminControlService.DeleteProduct(productId, WebRootPath);

            TempData["message"] = string.Format("Удалено -> " + title);

            return RedirectToAction("ProductList");
        }


        public IActionResult CategoryList()
        {
            var model = categoryService.GetAll();
            return View(model);
        }


        public IActionResult Category(int categoryId)
        {
            var model = categoryService.GetByIdAsync(categoryId).Result;
            return View(model);
        }


        [HttpPost]
        public IActionResult CategoryAdd(string categoryName)
        {
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                adminControlService.AddCategory(categoryName);
                TempData["message"] = string.Format("Добавлено -> " + categoryName);
            }
            else TempData["error"] = string.Format("Поле \"Название\" не должно быть пустым");

            return RedirectToAction("CategoryList");
        }


        [HttpPost]
        public async Task<IActionResult> CategoryEdit(int categoryId, string categoryName, IFormFile uploadedFile)
        {
            string message;
            
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                adminControlService.EditCategoryName(categoryId, categoryName);
                TempData["message"] = string.Format($"{categoryName} -> сохранено");
            }
            else TempData["error"] = string.Format("Поле \"Название\" не должно быть пустым.");

            if (contentService.IsImageValid(uploadedFile, out message))
            {
                await adminControlService.EditCategoryImage(uploadedFile, categoryId, WebRootPath);
            }
            else TempData["warn"] = message;

            var model = await categoryService.GetByIdAsync(categoryId);
            return View("Category", model);
        }


        [HttpPost]
        public async Task<IActionResult> CategoryDelete(int categoryId, string categoryName)
        {
            await adminControlService.DeleteCategory(categoryId, WebRootPath);

            TempData["message"] = string.Format("Удалено -> " + categoryName);

            return RedirectToAction("CategoryList");
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
                else TempData["error"] = string.Format("Не правильный пароль!");
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
            else TempData["error"] = string.Format("Ошибка подтверждения!");

            var model = authService.GetAllAccounts();
            return View("AccountManagement", model);
        }
    }
}
