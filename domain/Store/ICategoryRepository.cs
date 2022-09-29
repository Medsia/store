using Store.Data;
using System.Collections.Generic;

namespace Store
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void AddNewItem(CategoryDto item);
        void EditExistingItem(CategoryDto item);
        void DeleteItem(CategoryDto item);
    }
}
