using Store.Data;
using System.Linq;

namespace Store.Web.App
{
    public class AdminControlService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly int resetId = 0;

        public AdminControlService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
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

        public void DeleteProduct(ProductModel productModel)
        {
            productRepository.DeleteItem(Map(productModel));
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
            var productsToEdit = productRepository.GetAllByCategoryId(categoryId);

            foreach (var product in productsToEdit)
            {
                ProductDto productDto = Product.Mapper.Map(product);
                productDto.Id = resetId;

                productRepository.EditExistingItem(productDto);
            }
        }

        public void DeleteCategory(CategoryModel categoryModel)
        {
            ResetCategoryIdInProducts(categoryModel.Id);

            categoryRepository.DeleteItem(Map(categoryModel));
        }
    }
}
