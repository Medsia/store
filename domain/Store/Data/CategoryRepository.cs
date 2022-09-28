using Store.Data;
using System;
using System.Linq;

namespace Store.Memory
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Category[] categories = TemporaryData.categories;

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
