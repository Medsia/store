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
        public IActionResult Product(int productId)
        {
            var productModel = productService.GetByIdAsync(productId).Result;

            ViewBag.Categories = categoryService.GetAll();

            if(productId == 0) ViewBag.EditMode = false;
            else ViewBag.EditMode = true;

            return View(productModel);
        }












        [HttpPost]
        public async Task<IActionResult> ProductAdd(int productId, string title, int categoryId, decimal price, string description,
                                            IFormFile uploadedFile)
        {
            ProductModel productModel = new ProductModel
            {
                Id = productId,
                Title = title,
                Category = categoryService.GetByIdAsync(categoryId).Result,
                Description = description,
                Price = price,
            };

            string message;
            if (contentService.IsImageValid(uploadedFile, out message))
            {
                string fileName = "ProductThumbnail_" + productId.ToString() + "_";
                string path = "/Img/Products/" + fileName + uploadedFile.FileName;

                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                // adminControlService.AddProductThumbnail(productId, path);
            }
            else TempData["warn"] = message;

            if (productService.IsValid(productModel, out message))
            {
                adminControlService.AddProduct(productModel);
                TempData["message"] = string.Format("Добавлено -> title");

                return RedirectToAction("ProductList");
            }
            else TempData["error"] = message;

            ViewBag.Categories = categoryService.GetAll();
            ViewBag.EditMode = false;

            return View("Product", productModel);
        }









        [HttpPost]
        public async Task<IActionResult> ProductEdit(int productId, string title, int categoryId, decimal price, string description,
                                                        IFormFile uploadedFile)
        {
            ProductModel productModel = new ProductModel
            {
                Id = productId,
                Title = title,
                Category = categoryService.GetByIdAsync(categoryId).Result,
                Description = description,
                Price = price,
            };

            string message;
            if (contentService.IsImageValid(uploadedFile, out message))
            {
                string fileName = "ProductThumbnail_" + productId.ToString() + "_";
                string path = "/Img/Products/" + fileName + uploadedFile.FileName;

                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                string oldImgLink = contentService.GetThumbnailByProdIdAsync(productId).Result.ImgLink;
                adminControlService.EditProductThumbnail(productId, path);

                if(!string.IsNullOrEmpty(oldImgLink) && !oldImgLink.Equals("/Img/Empty.jpg"))
                {
                    FileInfo fileInf = new FileInfo(appEnvironment.WebRootPath + oldImgLink);
                    fileInf.Delete();
                }
            }
            else TempData["warn"] = message;

            if (productService.IsValid(productModel, out message))
            {
                adminControlService.EditProduct(productModel);
                TempData["message"] = string.Format("Изменения сохранены");

                return RedirectToAction("ProductList");
            }
            else TempData["error"] = message;

            ViewBag.Categories = categoryService.GetAll();
            ViewBag.EditMode = true;

            var model = productService.GetByIdAsync(productId).Result;

            return View("Product", model);
        }








        [HttpPost]
        public IActionResult ProductDelete(int productId, string title)
        {
            adminControlService.DeleteProduct(productId);

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
            if (contentService.IsImageValid(uploadedFile, out message))
            {
                string fileName = "CategoryImg_" + categoryId.ToString() + "_";
                string path = "/Img/Categories/" + fileName + uploadedFile.FileName;

                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                string oldImgLink;
                adminControlService.EditCategoryImage(categoryId, path, out oldImgLink);

                if (!oldImgLink.Equals("/Img/Empty.jpg"))
                {
                    FileInfo fileInf = new FileInfo(appEnvironment.WebRootPath + oldImgLink);
                    fileInf.Delete();
                }
            }
            else TempData["warn"] = message;

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                adminControlService.EditCategoryName(categoryId, categoryName);
                TempData["message"] = string.Format($"{categoryName} -> сохранено");
            }
            else TempData["error"] = string.Format("Поле \"Название\" не должно быть пустым.");

            var model = categoryService.GetByIdAsync(categoryId).Result;
            return View("Category", model);
        }











        [HttpPost]
        public IActionResult CategoryDelete(int categoryId, string categoryName, string oldImgLink)
        {
            adminControlService.DeleteCategory(categoryId);

            FileInfo fileInf = new FileInfo(appEnvironment.WebRootPath + oldImgLink);
            fileInf.Delete();

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
