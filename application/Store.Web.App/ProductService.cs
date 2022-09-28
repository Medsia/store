
using Store.Web.App;
using System.Collections.Generic;
using System.Linq;

namespace Store.Web.App
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
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

        public IReadOnlyCollection<ProductModel> GetAllByQuery(IEnumerable<int> categoryId)
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
                CategoryId = product.CategoryId,
                Description = product.Description,
                Price = product.Price,
            };
      

        }
    }
}
