using Store.Data;
using Store.Data.Content;
using System.Collections.Generic;
using System.Linq;

namespace Store.Web.App
{
    public class AdminControlService
    {
        private readonly int resetId = 1;

        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IInfoRepository infoRepository;
        private readonly IUserRepository userRepository;

        public AdminControlService(IProductRepository productRepository, ICategoryRepository categoryRepository, 
                                    IInfoRepository infoRepository, IUserRepository userRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.infoRepository = infoRepository;
            this.userRepository = userRepository;
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

        public void EditProduct(ProductModel productModel)
        {
            productRepository.EditExistingItem(Map(productModel));
        }

        public void DeleteProduct(int productId)
        {
            ProductDto productDto = Product.Mapper.Map(productRepository.GetByIdAsync(productId).Result);
            productRepository.DeleteItem(productDto);
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
