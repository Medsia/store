using System;
using System.Linq;

namespace Store.Memory
{
    public class ProductRepository : IProductRepository
    {
        private readonly Product[] products = new[]
        {
            new Product(1, "Значок Тетрадь смерти", new Category(1, "Значок")),
            new Product(2, "Плакат Ван пис", new Category(2, "Плакат")),
            new Product(3, "Брелок Тетрадь смерти", new Category(3, "Брелок")),
        };

        public Product[] GetAllByTitle(string titlePart)
        {
            return products.Where(product => product.Title.Contains(titlePart))
                           .ToArray();
        }

        public Product[] GetAllByCategoryId(int categoryId)
        {
            return products.Where(product => product.CategoryId == categoryId)
                           .ToArray();
        }
    }
}
