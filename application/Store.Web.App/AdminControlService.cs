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

        public void AddProduct(ProductModel productModel)
        {
            //productRepository.AddNewItem(Map(productModel));
        }

        public void EditProduct(ProductModel productModel)
        {
            //productRepository.EditExistingItem(Map(productModel));
        }

        public void DeleteProduct(ProductModel productModel)
        {
            //productRepository.DeleteItem(Map(productModel));
        }


        public void AddCategory(CategoryModel categoryModel)
        {
            //categoryRepository.AddNewItem(category);
        }

        public void EditCategory(CategoryModel category)
        {
            //categoryRepository.EditExistingItem(category);
        }

        public void ResetCategoryIdInProducts(int categoryId)
        {
            //var productsToEdit = productRepository.GetAllByCategoryId(categoryId);

            //foreach (var product in productsToEdit)
            //{
            //    Product newProduct = new Product(product.Id, product.Title, resetId, product.Description, product.Price);
            //    productRepository.EditExistingItem(product);
            //}
        }

        public void DeleteCategory(CategoryModel category)
        {
            //ResetCategoryIdInProducts(category.Id);
            //categoryRepository.DeleteItem(category);
        }
    }
}
