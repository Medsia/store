
using System.Collections.Generic;

namespace Store.Memory
{
    public static class TemporaryData
    {

        public static Category[] categories = new[]
        {
            new Category(1, "Значки"),
            new Category(2, "Плакаты"),
            new Category(3, "Брелки"),
            new Category(4, "Аксесуары"),
        };

        public static Product[] products = new[]
        {
            new Product(1, "Значок Тетрадь смерти", categories[0], "Материал - металл. " +
                "Диаметр значка 58 мм", 1.5m),
            new Product(2, "Значок Наруто Шипуден", categories[0], "Материал - металл. " +
                "Диаметр значка 44 мм", 1.2m),
            new Product(3, "Значок Один кусок", categories[0], "Материал - металл. " +
                "Диаметр значка 58 мм", 1m),

            new Product(4, "Плакат Ван пис", categories[1], "Формат А3(29,7см х42 см). " +
                "плотность бумаги 150гр", 4m),
            new Product(5, "Плакат БТС", categories[1], "Формат А3(29,7см х42 см). " +
                "плотность бумаги 150гр", 10m),
            new Product(6, "Плакат Геншын Инфаркт", categories[1], "Формат А3(29,7см х42 см). " +
                "плотность бумаги 150гр", 1m),

            new Product(7, "Брелок Тетрадь смерти", categories[2], "Размер: 4х5.5 см. " +
                "Материал: PVC", 2m),
            new Product(8, "Брелок Атака Гигантов", categories[2], "Размер: 4х5.5 см. " +
                "Материал: PVC", 2m),
            new Product(9, "Брелок Девочки Волшебницы", categories[2], "Размер: 4х5.5 см. " +
                "Материал: PVC", 2m),

            new Product(10, "Кольцо БТС", categories[3], "Размер: 4х5.5 см. " +
                "Материал: PVC", 2m),
            new Product(11, "Браслет Кросс Фаер)", categories[3], "Размер: 4х5.5 см. " +
                "Материал: PVC", 2m),
            new Product(12, "Очки \"Как у Двачера\"", categories[3], "Размер: 4х5.5 см. " +
                "Материал: PVC", 2m),
        };

        public static List<Info> infos = new List<Info>
        {
            new Info(1, "Контакты", "МТС: 8(800)555-35-35"),
            new Info(2, "Оплата", "Налом или картой? Калом"),
            new Info(3, "Доставка", "Доставляем Яндекс Едой"),
            new Info(4, "О магазине", "Самый кайфовый магаз"),
        };
    }
}
