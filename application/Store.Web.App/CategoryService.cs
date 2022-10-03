using System.Collections.Generic;
using System.Linq;

namespace Store.Web.App
{
    public class CategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public CategoryModel GetById(int id)
        {
            var category = categoryRepository.GetCategoryById(id);

            return Map(category);
        }

        public IReadOnlyCollection<CategoryModel> GetAll()
        {
            var category = categoryRepository.GetAllCategories();

            return category.Select(Map)
                        .ToArray();
        }

        private CategoryModel Map(Category category)
        {
            return new CategoryModel
            {
                Id = category.Id,
                Name = category.Name
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
