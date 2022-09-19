using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> products = TemporaryData.products;

        public IEnumerable<Product> GetAllByTitle(string query)
        {
            return products.Where(product => product.Title.Contains(query))
                           .ToArray();
        }

        public IEnumerable<Product> GetAllByCategoryId(int categoryId)
        {
            return products.Where(product => product.CategoryId == categoryId)
                           .ToArray();
        }

        public Product GetById(int id)
        {
            return products.Single(product => product.Id == id);
        }

        public IEnumerable<Product> GetAllByIds(IEnumerable<int> productIds)
        {
            var foundProducts = from product in products
                                join productId in productIds on product.Id equals productId
                                select product;

            return foundProducts.ToArray();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return products.ToArray();
        }

        public bool AddNewItem(Product item)
        {
            int itemDbId = products.FindIndex(categoriesItem => categoriesItem.Id == item.Id);

            if (itemDbId != -1)
                return false;

            products.Add(item);
            return true;
        }

        public bool EditExistingItem(Product item)
        {
            int itemDbId = products.FindIndex(categoriesItem => categoriesItem.Id == item.Id);

            if (itemDbId == -1)
                return false;

            products[itemDbId] = item;
            return true;
        }

        public bool DeleteItem(Product item)
        {
            int itemDbId = products.FindIndex(categoriesItem => categoriesItem.Id == item.Id);

            if (itemDbId == -1)
                return false;

            products.RemoveAt(itemDbId);
            return true;
        }
    }
}
