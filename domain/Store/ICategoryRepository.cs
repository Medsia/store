using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        bool AddNewItem(Category item);
        bool EditExistingItem(Category item);
        bool DeleteItem(Category item);
    }
}
