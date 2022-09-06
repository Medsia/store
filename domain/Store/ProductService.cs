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
            //if (IsCategory(query))
            //    return productRepository.GetAllByCategoryId(query);
            return productRepository.GetAllByTitle(query);
        }
    }
}
