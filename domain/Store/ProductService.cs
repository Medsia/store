
namespace Store
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public Product[] GetAllByQuery(string query)
        {
            return productRepository.GetAllByTitle(query);
        }

        public Product[] GetAllByQuery(int query)
        {
            return productRepository.GetAllByCategoryId(query);
        }
    }
}
