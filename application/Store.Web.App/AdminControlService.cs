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
        string fileType = ".png";

        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IInfoRepository infoRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductImgLinkRepository productImgLinkRepository;
        private readonly IImageRepository imageRepository;

        public AdminControlService(IProductRepository productRepository, ICategoryRepository categoryRepository, 
                                    IInfoRepository infoRepository, IUserRepository userRepository,
                                    IProductImgLinkRepository productImgLinkRepository, IImageRepository imageRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.infoRepository = infoRepository;
            this.userRepository = userRepository;
            this.productImgLinkRepository = productImgLinkRepository;
            this.imageRepository = imageRepository;
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


        public async Task EditProductThumbnail(IFormFile uploadedImage, int productId, string webRootPath)
        {
            ProductImgLink productImgLink = await productImgLinkRepository.GetImageOrDefaultAsync(productId, true);
            ProductImgLinkDto dto;

            string fileName = "ProductThumbnail_" + productId.ToString() + fileType;
            string path = "/Img/Products/" + fileName;
            string fullPath = webRootPath + path;

            if (productImgLink.Id != 0)
            {
                dto = ProductImgLink.Mapper.Map(productImgLink);
                dto.ImgLink = path;

                productImgLinkRepository.EditExistingItem(dto);
                imageRepository.EditImageAsync(uploadedImage, fullPath);
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
                imageRepository.SaveImageAsync(uploadedImage, fullPath);
            }
        }

        public async Task EditProductImage(IFormFile uploadedImage, int productId, int personalId, string webRootPath)
        {
            ProductImgLink productImgLink = await productImgLinkRepository.GetImageOrDefaultAsync(productId, personalId);
            ProductImgLinkDto dto;

            string fileName = "ProductImage_" + productId.ToString() + "_" + personalId.ToString() + fileType;
            string path = "/Img/Products/" + fileName;
            string fullPath = webRootPath + path;

            if (productImgLink.Id != 0)
            {
                dto = ProductImgLink.Mapper.Map(productImgLink);
                dto.ImgLink = path;

                productImgLinkRepository.EditExistingItem(dto);
                imageRepository.EditImageAsync(uploadedImage, fullPath);
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
                imageRepository.SaveImageAsync(uploadedImage, fullPath);
            }
        }


        public void EditProduct(ProductModel productModel)
        {
            productRepository.EditExistingItem(Map(productModel));
        }


        public async Task DeleteAllProductImages(int productId, string webRootPath)
        {
            ProductImgLink productThumbnail = await productImgLinkRepository.GetImageOrDefaultAsync(productId, true);
            IEnumerable<ProductImgLink> productImages = await productImgLinkRepository.GetAllOrDefaultByProductIdAsync(productId);

            if(productThumbnail.Id != 0)
            {
                imageRepository.DeleteImage(webRootPath + productThumbnail.ImgLink);
                productImgLinkRepository.DeleteItem(ProductImgLink.Mapper.Map(productThumbnail));
            }
            
            foreach (var image in productImages)
            {
                imageRepository.DeleteImage(webRootPath + image.ImgLink);
                productImgLinkRepository.DeleteItem(ProductImgLink.Mapper.Map(image));
            }

        }


        public async Task DeleteProduct(int productId, string webRootPath)
        {
            ProductDto productDto = Product.Mapper.Map(productRepository.GetByIdAsync(productId).Result);
            productRepository.DeleteItem(productDto);
            await DeleteAllProductImages(productId, webRootPath);
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


        public void EditCategoryImage(int categoryId, string imgLink, out string oldImgLink)
        {
            var dto = Category.Mapper.Map(categoryRepository.GetCategoryByIdAsync(categoryId).Result);
            oldImgLink = dto.ImgLink;

            dto.ImgLink = imgLink;

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


        public void DeleteCategory(int id)
        {
            ResetCategoryIdInProducts(id);

            CategoryDto categoryDto = Category.Mapper.Map(categoryRepository.GetCategoryByIdAsync(id).Result);
            categoryRepository.DeleteItem(categoryDto);
        }


        public void EditContacts(string title, string location, string worktime, List<string> numbers, string additional)
        {
            ContactsSO contactsSO = new ContactsSO
            {
                Title = title,
                Location = location,
                Worktime = worktime,
                Numbers = numbers.Where(str => !string.IsNullOrEmpty(str)).ToArray(),
                Additional = additional
            };
            infoRepository.UpdateContactsData(contactsSO);
        }


        public void EditPayment(string title, List<string> options, string additional)
        {
            PaymentSO paymentSO = new PaymentSO
            {
                Title = title,
                Options = options.Where(str => !string.IsNullOrEmpty(str)).ToArray(),
                Additional = additional
            };
            infoRepository.UpdatePaymentData(paymentSO);
        }


        public void EditDelivery(string title, List<string> options, string additional)
        {
            DeliverySO deliverySO = new DeliverySO
            {
                Title = title,
                Options = options.Where(str => !string.IsNullOrEmpty(str)).ToArray(),
                Additional = additional
            };
            infoRepository.UpdateDeliveryData(deliverySO);
        }


        public void EditAbout(string title, string description)
        {
            AboutSO aboutSO = new AboutSO
            {
                Title = title,
                Description = description
            };
            infoRepository.UpdateAboutData(aboutSO);
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
