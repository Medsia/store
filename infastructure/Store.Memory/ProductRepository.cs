using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class ProductRepository : IProductRepository
    {
        private readonly Product[] products = new[]
        {
            new Product(1, "Значок Тетрадь смерти", new Category(1, "Значки"), "Материал - металл. " +
                "Диаметр значка 58 мм", 1.5m),
            new Product(2, "Плакат Ван пис", new Category(2, "Плакаты"), "Формат А3(29,7см х42 см). " +
                "плотность бумаги 150гр", 4m),
            new Product(3, "Брелок Тетрадь смерти", new Category(3, "Брелки"), "Размер: 4х5.5 см. " +
                "Материал: PVC", 2m),
        };

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
    }
}
