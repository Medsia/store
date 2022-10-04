using Store.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Task<Category> GetCategoryByIdAsync(int id);
        void AddNewItem(CategoryDto item);
        void EditExistingItem(CategoryDto item);
        void DeleteItem(CategoryDto item);
    }
}
