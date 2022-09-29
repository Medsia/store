using System.Collections.Generic;

namespace Store
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void AddNewItem(Category item);
        void EditExistingItem(Category item);
        void DeleteItem(Category item);
    }
}
