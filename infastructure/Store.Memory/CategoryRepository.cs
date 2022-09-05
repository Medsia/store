using System;
using System.Linq;

namespace Store.Memory
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Category[] categories = new[]
        {
            new Category(1, "Значок"),
            new Category(2, "Плакат"),
            new Category(3, "Брелок"),
        };

        public Category[] GetAllCategories()
        {
            return categories;
        }
    }
}
