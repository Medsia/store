
using Store.Web.App;
using System.Collections.Generic;
using System.Linq;

namespace Store.Web.App
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
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

        private ProductModel Map(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Title = product.Title,
                Category = categoryRepository.GetCategoryById(product.CategoryId),
                Description = product.Description,
                Price = product.Price,
            };
      

        }
    }
}
