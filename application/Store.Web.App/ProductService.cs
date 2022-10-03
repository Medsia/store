using System.Collections.Generic;
using System.Linq;

namespace Store.Web.App
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        private readonly CategoryService categoryService;

        public ProductService(IProductRepository productRepository, CategoryService categoryService)
        {
            this.productRepository = productRepository;
            this.categoryService = categoryService;
        }

        public ProductModel GetById(int id)
        {
            var product = productRepository.GetById(id);

            return Map(product);
        }

        public IReadOnlyCollection<ProductModel> GetAllByQuery(string query)
        {
            var products = productRepository.GetAllByTitle(query);

            return products.Select(Map)
                        .ToArray();
        }

        public IReadOnlyCollection<ProductModel> GetAllByQuery(int categoryId)
        {
            var products = productRepository.GetAllByCategoryId(categoryId);

            return products.Select(Map)
                        .ToArray();
        }

        public IReadOnlyCollection<ProductModel> GetAll()
        {
            var products = productRepository.GetAllProducts();

            return products.Select(Map)
                        .ToArray();
        }

        private ProductModel Map(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Title = product.Title,
                Category = categoryService.GetById(product.CategoryId),
                Description = product.Description,
                Price = product.Price,
            };
        }

        public bool IsValid(ProductModel productModel)
        {
            if (productModel == null || string.IsNullOrWhiteSpace(productModel.Title) || productModel.Price == 0)
                return false;

            return true;
        }
    }
}
