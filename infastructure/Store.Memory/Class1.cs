using System;
using System.Linq;

namespace Store.Memory
{
    public class ProductRepository : IProductRepository
    {
        private readonly Product[] products = new[]
        {
            new Product(1, "Значок Тетрадь смерти"),
            new Product(2, "Плакат Ван пис"),
            new Product(3, "Брелок Тетрадь смерти"),
        };

        public Product[] GetAllByTitle(string titlePart)
        {
            return products.Where(product => product.Title.Contains(titlePart))
                           .ToArray();
        }
    }
}
