using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.App
{
    public class CategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        private readonly int defaultCategoryId = 1;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetCategoryByIdAsync(id);

            return Map(category);
        }

        public async Task<CategoryModel> GetDefaultAsync()
        {
            var category = await categoryRepository.GetCategoryByIdAsync(defaultCategoryId);

            return Map(category);
        }

        public IReadOnlyCollection<CategoryModel> GetAll()
        {
            var categories = categoryRepository.GetAllCategories();

            return categories.Select(Map)
                        .ToArray();
        }

        private CategoryModel Map(Category category)
        {
            string img = category.ImgLink;
            if(string.IsNullOrWhiteSpace(category.ImgLink)) img = ContentService.EmptyImageLink;

            return new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                ImgLink = img
            };
        }
    }
}
