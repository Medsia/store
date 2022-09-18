using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class ProductRepository : IProductRepository
    {
        private Product[] products = TemporaryData.products;

        public Product[] GetAllByTitle(string query)
        {
            return products.Where(product => product.Title.Contains(query))
                           .ToArray();
        }

        public Product[] GetAllByCategoryId(int categoryId)
        {
            return products.Where(product => product.CategoryId == categoryId)
                           .ToArray();
        }

        public Product GetById(int id)
        {
            return products.Single(product => product.Id == id);
        }

        public Product[] GetAllByIds(IEnumerable<int> productIds)
        {
            var foundProducts = from product in products
                                join productId in productIds on product.Id equals productId
                                select product;

            return foundProducts.ToArray();
        }

        public bool AddNewItem(Product item)
        {
            throw new System.NotImplementedException();
        }

        public bool EditExistingItem(Product item)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteItem(Product item)
        {
            throw new System.NotImplementedException();
        }
    }
}
