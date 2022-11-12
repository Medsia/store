using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Store.Data;
using Store.Data.Content;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.App
{
    public class AdminControlService
    {
        private readonly int resetId = 1;
        private readonly string fileType = ".png";

        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IInfoRepository infoRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductImgLinkRepository productImgLinkRepository;
        private readonly IImageRepository imageRepository;

        public string WebRootPath { private get; set; }

        public AdminControlService(IProductRepository productRepository, ICategoryRepository categoryRepository, 
                                    IInfoRepository infoRepository, IUserRepository userRepository,
                                    IProductImgLinkRepository productImgLinkRepository, IImageRepository imageRepository,
                                    IWebHostEnvironment appEnvironment)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.infoRepository = infoRepository;
            this.userRepository = userRepository;
            this.productImgLinkRepository = productImgLinkRepository;
            this.imageRepository = imageRepository;

            this.WebRootPath = appEnvironment.WebRootPath;
            infoRepository.WebRootPath = this.WebRootPath;
        }


        private ProductDto Map(ProductModel productModel)
        {
            return new ProductDto
            {
                Id = productModel.Id,
                Title = productModel.Title,
                Description = productModel.Description,
                Price = productModel.Price,
                CategoryId = productModel.Category.Id,
            };
        }


        public void AddProduct(ProductModel productModel)
        {
            productRepository.AddNewItem(Map(productModel));
        }


        public async Task EditProductThumbnail(IFormFile uploadedImage, int productId)
        {
            ProductImgLink productImgLink = await productImgLinkRepository.GetImageOrDefaultAsync(productId, true);
            ProductImgLinkDto dto;

            string fileName = "ProductThumbnail_" + productId.ToString() + fileType;
            string path = "/Img/Products/" + fileName;
            string fullPath = WebRootPath + path;

            if (productImgLink.Id != 0)
            {
                dto = ProductImgLink.Mapper.Map(productImgLink);
                dto.ImgLink = path;

                productImgLinkRepository.EditExistingItem(dto);
                await imageRepository.EditImageAsync(uploadedImage, fullPath);
            }
            else
            {
                dto = new ProductImgLinkDto
                {
                    ProductId = productId,
                    PersonalId = 0,
                    ImgLink = path,
                    IsThumbnail = true,
                };
                
                productImgLinkRepository.AddNewItem(dto);
                await imageRepository.SaveImageAsync(uploadedImage, fullPath);
            }
        }

        public async Task EditProductImage(IFormFile uploadedImage, int productId, int personalId)
        {
            ProductImgLink productImgLink = await productImgLinkRepository.GetImageOrDefaultAsync(productId, personalId);
            ProductImgLinkDto dto;

            string fileName = "ProductImage_" + productId.ToString() + "_" + personalId.ToString() + fileType;
            string path = "/Img/Products/" + fileName;
            string fullPath = WebRootPath + path;

            if (productImgLink.Id != 0)
            {
                dto = ProductImgLink.Mapper.Map(productImgLink);
                dto.ImgLink = path;

                productImgLinkRepository.EditExistingItem(dto);
                await imageRepository.EditImageAsync(uploadedImage, fullPath);
            }
            else
            {
                dto = new ProductImgLinkDto
                {
                    ProductId = productId,
                    PersonalId = personalId,
                    ImgLink = path,
                    IsThumbnail = false,
                };

                productImgLinkRepository.AddNewItem(dto);
                await imageRepository.SaveImageAsync(uploadedImage, fullPath);
            }
        }


        public void EditProduct(ProductModel productModel)
        {
            productRepository.EditExistingItem(Map(productModel));
        }


        public async Task DeleteAllProductImages(int productId)
        {
            ProductImgLink productThumbnail = await productImgLinkRepository.GetImageOrDefaultAsync(productId, true);
            IEnumerable<ProductImgLink> productImages = await productImgLinkRepository.GetAllOrDefaultByProductIdAsync(productId);

            if(productThumbnail.Id != 0)
            {
                imageRepository.DeleteImage(WebRootPath + productThumbnail.ImgLink);
                productImgLinkRepository.DeleteItem(ProductImgLink.Mapper.Map(productThumbnail));
            }
            
            foreach (var image in productImages)
            {
                imageRepository.DeleteImage(WebRootPath + image.ImgLink);
                productImgLinkRepository.DeleteItem(ProductImgLink.Mapper.Map(image));
            }

        }


        public async Task DeleteProduct(int productId)
        {
            ProductDto productDto = Product.Mapper.Map(productRepository.GetByIdAsync(productId).Result);
            productRepository.DeleteItem(productDto);
            await DeleteAllProductImages(productId);
        }


        public void AddCategory(string Name)
        {
            CategoryDto dto = new CategoryDto
            {
                Name = Name,
                ImgLink = ContentService.EmptyImageLink 
            };

            categoryRepository.AddNewItem(dto);
        }


        public void EditCategoryName(int categoryId, string categoryName)
        {
            var dto = Category.Mapper.Map(categoryRepository.GetCategoryByIdAsync(categoryId).Result);

            dto.Name = categoryName;

            categoryRepository.EditExistingItem(dto);
        }


        public async Task EditCategoryImage(IFormFile uploadedImage, int categoryId)
        {
            string fileName = "CategoryImg_" + categoryId.ToString() + fileType;
            string path = "/Img/Categories/" + fileName;
            string fullpath = WebRootPath + path;

            var dto = Category.Mapper.Map(await categoryRepository.GetCategoryByIdAsync(categoryId));

            if (string.IsNullOrWhiteSpace(dto.ImgLink))
                await imageRepository.SaveImageAsync(uploadedImage, fullpath);
            else await imageRepository.EditImageAsync(uploadedImage, fullpath);

            dto.ImgLink = path;
            categoryRepository.EditExistingItem(dto);
        }


        public void ResetCategoryIdInProducts(int categoryId)
        {
            var productsToEdit = productRepository.GetAllByCategoryIdAsync(categoryId).Result;

            foreach (var product in productsToEdit)
            {
                ProductDto productDto = Product.Mapper.Map(product);
                productDto.CategoryId = resetId;

                productRepository.EditExistingItem(productDto);
            }
        }


        public async Task DeleteCategory(int categoryId)
        {
            ResetCategoryIdInProducts(categoryId);

            CategoryDto categoryDto = Category.Mapper.Map(await categoryRepository.GetCategoryByIdAsync(categoryId));

            imageRepository.DeleteImage(WebRootPath + categoryDto.ImgLink);
            categoryRepository.DeleteItem(categoryDto);
        }


        public void EditContacts(string title, string location, string worktime, 
                                    List<string> numbers, string additional, IFormFile uploadedImage)
        {
            string fileName = "ContactsBanner" + fileType;
            string path = "/Img/Other/" + fileName;
            string fullPath = WebRootPath + path;

            ContactsSO contactsSO = new ContactsSO
            {
                Title = title,
                ImgLink = path,
                Location = location,
                Worktime = worktime,
                Numbers = numbers.Where(str => !string.IsNullOrEmpty(str)).ToArray(),
                Additional = additional
            };

            infoRepository.UpdateContactsData(contactsSO);
            imageRepository.EditImageAsync(uploadedImage, fullPath);
        }


        public void EditPayment(string title, List<string> options, string additional, IFormFile uploadedImage)
        {
            string fileName = "PaymentBanner" + fileType;
            string path = "/Img/Other/" + fileName;
            string fullPath = WebRootPath + path;

            PaymentSO paymentSO = new PaymentSO
            {
                Title = title,
                ImgLink = path,
                Options = options.Where(str => !string.IsNullOrEmpty(str)).ToArray(),
                Additional = additional
            };

            infoRepository.UpdatePaymentData(paymentSO);
            imageRepository.EditImageAsync(uploadedImage, fullPath);
        }


        public void EditDelivery(string title, List<string> options, string additional, IFormFile uploadedImage)
        {
            string fileName = "DeliveryBanner" + fileType;
            string path = "/Img/Other/" + fileName;
            string fullPath = WebRootPath + path;

            DeliverySO deliverySO = new DeliverySO
            {
                Title = title,
                ImgLink = path,
                Options = options.Where(str => !string.IsNullOrEmpty(str)).ToArray(),
                Additional = additional
            };

            infoRepository.UpdateDeliveryData(deliverySO);
            imageRepository.EditImageAsync(uploadedImage, fullPath);
        }


        public void EditAbout(string title, string description, IFormFile uploadedImage)
        {
            string fileName = "AboutBanner" + fileType;
            string path = "/Img/Other/" + fileName;
            string fullPath = WebRootPath + path;

            AboutSO aboutSO = new AboutSO
            {
                Title = title,
                ImgLink = path,
                Description = description
            };

            infoRepository.UpdateAboutData(aboutSO);
            imageRepository.EditImageAsync(uploadedImage, fullPath);
        }


        public void CreateAccount(string login, string password)
        {
            User user = userRepository.GetUserOrDedaultAsync(login).Result;

            if (user == null)
            {
                UserDto userDto = new UserDto
                {
                    Login = login,
                    Password = PasswordHasher.Hash(password)
                };
                userRepository.AddUser(userDto);
            }
        }


        public void ChangePassword(string login, string newPassword)
        {
            User user = userRepository.GetUserOrDedaultAsync(login).Result;

            UserDto userDto = User.Mapper.Map(user);
            userDto.Password = PasswordHasher.Hash(newPassword);

            userRepository.EditUser(userDto);
        }


        public void DeleteAccount(string login)
        {
            User user = userRepository.GetUserOrDedaultAsync(login).Result;

            UserDto userDto = User.Mapper.Map(user);

            userRepository.DeleteUser(userDto);
        }
    }
}
