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
        IEnumerable<Product> GetAllByCategoryId(IEnumerable<int> categoryIds);
        Product GetById(int id);
        IEnumerable<Product> GetAllByIds(IEnumerable<int> productIds);
        IEnumerable<Product> GetAllProducts();
        bool AddNewItem(Product item);
        bool EditExistingItem(Product item);
        bool DeleteItem(Product item);
    }
}
