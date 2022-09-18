
namespace Store.Web.App
{
    public class AdminControlService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IInfoRepository infoRepository;

        public AdminControlService(IProductRepository productRepository, ICategoryRepository categoryRepository, IInfoRepository infoRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.infoRepository = infoRepository;
        }

        public void AddProduct(Product product)
        {
            productRepository.AddNewItem(product);
        }

        public void EditProduct(Product product)
        {
            productRepository.EditExistingItem(product);
        }

        public void DeleteProduct(Product product)
        {
            productRepository.DeleteItem(product);
        }


        public void AddCategory(Category category)
        {
            categoryRepository.AddNewItem(category);
        }

        public void EditCategory(Category category)
        {
            categoryRepository.EditExistingItem(category);
        }

        public void DeleteCategory(Category category)
        {
            categoryRepository.DeleteItem(category);
        }

        public void EditInfo(Info info)
        {
            infoRepository.EditExistingItem(info);
        }
    }
}
