using Store.Data;
using System.Linq;

namespace Store.Web.App
{
    public class AdminControlService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IInfoRepository infoRepository;
        private readonly int resetId = 1;

        public AdminControlService(IProductRepository productRepository, ICategoryRepository categoryRepository, IInfoRepository infoRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.infoRepository = infoRepository;
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


        // В ПРОЦЕССЕ
        public void EditInfo(int id)
        {
            //infoRepository.EditData(id);
        }
    }
}
