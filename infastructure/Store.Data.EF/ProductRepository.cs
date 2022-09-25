using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF
{
    class ProductRepository : IProductRepository
    {
        public Product[] GetAllByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Product[] GetAllByIds(IEnumerable<int> productIds)
        {
            throw new NotImplementedException();
        }

        public Product[] GetAllByTitle(string titlePart)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
