using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.App
{
    public class CategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetCategoryByIdAsync(id);

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
            return new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                ImgLink = category.ImgLink
            };
        }

        public bool IsValid(CategoryModel categoryModel)
        {
            if (categoryModel == null || string.IsNullOrWhiteSpace(categoryModel.Name))
                return false;

            return true;
        }
    }
}
