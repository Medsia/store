using System.Linq;

namespace Store.Web.App
{
    public class AdminControlService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IInfoRepository infoRepository;
        private readonly int resetId = 0;

        public AdminControlService(IProductRepository productRepository, ICategoryRepository categoryRepository, IInfoRepository infoRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.infoRepository = infoRepository;
        }

        private Product Map(ProductModel productModel)
        {
            return new Product(productModel.Id, productModel.Title, productModel.Category.Id, productModel.Description, productModel.Price);
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


        public void AddCategory(Category category)
        {
            categoryRepository.AddNewItem(category);
        }

        public void EditCategory(Category category)
        {
            categoryRepository.EditExistingItem(category);
        }

        public void ResetCategoryIdInProducts(int categoryId)
        {
            var productsToEdit = productRepository.GetAllByCategoryId(categoryId);

            foreach (var product in productsToEdit)
            {
                Product newProduct = new Product(product.Id, product.Title, resetId, product.Description, product.Price);
                productRepository.EditExistingItem(product);
            }
        }

        public void DeleteCategory(Category category)
        {
            ResetCategoryIdInProducts(category.Id);
            categoryRepository.DeleteItem(category);
        }


        public void EditInfo(Info info)
        {
            infoRepository.EditExistingItem(info);
        }
    }
}
