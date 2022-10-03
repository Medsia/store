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
        IEnumerable<Product> GetAllByTitle(string titlePart);
        IEnumerable<Product> GetAllByCategoryId(int categoryId);
        Product GetById(int id);
        IEnumerable<Product> GetAllByIds(IEnumerable<int> productIds);
        IEnumerable<Product> GetAllProducts();
        void AddNewItem(ProductDto item);
        void EditExistingItem(ProductDto item);
        void DeleteItem(ProductDto item);
    }
}
