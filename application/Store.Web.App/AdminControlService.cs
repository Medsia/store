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

        private CategoryDto Map(CategoryModel productModel)
        {
            return new CategoryDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
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


        public void AddCategory(CategoryModel categoryModel)
        {
            categoryRepository.AddNewItem(Map(categoryModel));
        }

        public void EditCategory(CategoryModel categoryModel)
        {
            categoryRepository.EditExistingItem(Map(categoryModel));
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


        public void ChangePassword(string login, string newPassword)
        {
            User user = userRepository.GetUserOrDedaultAsync(login).Result;

            UserDto userDto = User.Mapper.Map(user);
            userDto.Password = PasswordHasher.Hash(newPassword);

            userRepository.EditUser(userDto);
        }
    }
}
