using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public interface ICategoryRepository
    {
        Category[] GetAllCategories();
        Category GetCategoryById(int id);
    }
}
