using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IProductRepository
    {
        Product[] GetAllByTitle(string titlePart);
        Product[] GetAllByCategoryId(int categoryId);
        Product GetById(int id);
        Product[] GetAllByIds(IEnumerable<int> productIds);
        Product[] GetAllProducts();
        bool AddNewItem(Product item);
        bool EditExistingItem(Product item);
        bool DeleteItem(Product item);
    }
}
