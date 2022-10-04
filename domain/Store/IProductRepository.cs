using Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IProductRepository
    {
        Task <IEnumerable<Product>> GetAllByTitleAsync(string titlePart);
        Task <IEnumerable<Product>> GetAllByCategoryIdAsync(int categoryId);
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllByIdsAsync(IEnumerable<int> productIds);
        IEnumerable<Product> GetAllProducts();
        void AddNewItem(ProductDto item);
        void EditExistingItem(ProductDto item);
        void DeleteItem(ProductDto item);
    }
}
