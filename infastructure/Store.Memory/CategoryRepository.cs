using System;
using System.Linq;

namespace Store.Memory
{
    public class CategoryRepository : ICategoryRepository
    {
        private Category[] categories = TemporaryData.categories;

        public Category[] GetAllCategories()
        {
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            return categories.First(category => category.Id == categoryId);
        }

        public bool AddNewItem(Category item)
        {
            throw new NotImplementedException();
        }

        public bool EditExistingItem(Category item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Category item)
        {
            throw new NotImplementedException();
        }
    }
}
