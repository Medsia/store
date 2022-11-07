using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.App
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        private readonly CategoryService categoryService;
        private readonly ContentService contentService;

        public ProductService(IProductRepository productRepository, CategoryService categoryService, ContentService contentService)
        {
            this.productRepository = productRepository;
            this.categoryService = categoryService;
            this.contentService = contentService;
        }

        public async Task<ProductModel> GetEmptyModelAsync()
        {
            return new ProductModel
            {
                Id = 0,
                Title = "",
                Category = await categoryService.GetDefaultAsync(),
                Description = "",
                Price = 0,
                ThumbnailLink = ContentService.EmptyImageLink
            };
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            return Map(product);
        }

        public async Task<IReadOnlyCollection<ProductModel>> GetAllByQueryAsync(string query)
        {
            var products = await productRepository.GetAllByTitleAsync(query);                 
                      
            return products.Select(Map)
                        .ToArray();
        }

        public async Task<IReadOnlyCollection<ProductModel>> GetAllByQueryAsync(int categoryId)
        {
            var products = await productRepository.GetAllByCategoryIdAsync(categoryId);

            return products.Select(Map)
                        .ToArray();
        }

        public IReadOnlyCollection<ProductModel> GetAll()
        {
            var products = productRepository.GetAllProducts();

            return products.Select(Map)
                        .ToArray();
        }

        public async Task<ProductModel> GetLastCreated()
        {
            var product = await productRepository.GetLastCreatedAsync();

            return Map(product);
        }

        private ProductModel Map(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Title = product.Title,
                Category = categoryService.GetByIdAsync(product.CategoryId).Result,
                Description = product.Description,
                Price = product.Price,
                ThumbnailLink = contentService.GetThumbnailByProdIdAsync(product.Id).Result
            };
        }

        public bool IsValid(ProductModel productModel, out string message)
        {
            message = "";

            if (productModel == null)
            {
                message = "Ошибка отправки данных";
                return false;
            }

            if (string.IsNullOrWhiteSpace(productModel.Title))
            {
                message = "Поле \"Название\" не должно быть пустым";
                return false;
            }

            if (productModel.Price == 0)
            {
                message = "Цена -> 0";
                return false;
            }

            return true;
        }
    }
}
