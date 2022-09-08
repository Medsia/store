using System;
using System.Linq;

namespace Store.Memory
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Category[] categories = new[]
        {
            new Category(1, "Значки"),
            new Category(2, "Плакаты"),
            new Category(3, "Брелки"),
        };

        public Category[] GetAllCategories()
        {
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            return categories.First(category => category.Id == categoryId);
        }
    }
}
