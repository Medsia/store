using Store.Data.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data
{
    public static class TemporaryData
    {
        public static readonly List<CategoryDto> CategoryDtoList = new List<CategoryDto>
        {
             new CategoryDto{
                Id = 1,
                Name = "Default" },

            new CategoryDto{
                Id = 2,
                Name = "Значки" },

            new CategoryDto{
                Id = 3,
                Name = "Плакаты" },

            new CategoryDto{
                Id = 4,
                Name = "Брелки" },

            new CategoryDto{
                Id = 5,
                Name = "Аксесуары" },
        };

        public static List<ProductDto> ProductDtoList = new List<ProductDto>
        {
            new ProductDto{
                Id = 1,
                Title = "Значок Тетрадь смерти",
                CategoryId = CategoryDtoList[1].Id,
                Description = "Материал - металл. " +
                "Диаметр значка 58 мм",
                Price =  1.5m },

            new ProductDto{
                Id = 2,
                Title = "Значок Наруто Шипуден",
                CategoryId = CategoryDtoList[1].Id,
                Description = "Материал - металл. " +
                "Диаметр значка 44 мм",
                Price = 1.2m },

            new ProductDto{
                Id = 3,
                Title = "Значок Один кусок",
                CategoryId = CategoryDtoList[1].Id,
                Description = "Материал - металл. " +
                "Диаметр значка 58 мм",
                Price = 1m },

            new ProductDto{
                Id = 4,
                Title = "Плакат Ван пис",
                CategoryId = CategoryDtoList[2].Id,
                Description = "Формат А3(29,7см х42 см). " +
                "плотность бумаги 150гр",
                Price = 4m },

            new ProductDto{
                Id = 5,
                Title = "Плакат БТС",
                CategoryId = CategoryDtoList[2].Id,
                Description = "Формат А3(29,7см х42 см). " +
                "плотность бумаги 150гр",
                Price = 10m },

            new ProductDto{
                Id = 6,
                Title = "Плакат Геншын Инфаркт",
                CategoryId = CategoryDtoList[2].Id,
                Description = "Формат А3(29,7см х42 см). " +
                "плотность бумаги 150гр",
                Price = 1m },

            new ProductDto{
                Id = 7,
                Title = "Брелок Тетрадь смерти",
                CategoryId = CategoryDtoList[3].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m },

            new ProductDto{
                Id = 8,
                Title = "Брелок Атака Гигантов",
                CategoryId = CategoryDtoList[3].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m },

            new ProductDto{
                Id = 9,
                Title = "Брелок Девочки Волшебницы",
                CategoryId = CategoryDtoList[3].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m },

            new ProductDto{
                Id = 10,
                Title = "Кольцо БТС",
                CategoryId = CategoryDtoList[4].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m },

            new ProductDto{
                Id = 11,
                Title = "Браслет Кросс Фаер)",
                CategoryId = CategoryDtoList[4].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m },

            new ProductDto{
                Id = 12,
                Title = "Очки \"Как у Двачера\"",
                CategoryId = CategoryDtoList[4].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m }
        };

        public static List<UserDto> UserDtoList = new List<UserDto>
        {
            new UserDto
            {
                Id = 1,
                Login = "admin",
                Password = "$MYHASH$V1$10000$iSZbCJtBHeAXae7+tfKFjYMBn+ZhygDcDdytZ+e2uSm47Y5C"
            }
        };

        public static Dictionary<string, string> OrderStates = new Dictionary<string, string>
        {
            { "processing", "В обработке" }, // первый всегда тот, который устанавливается при создании заказа
            { "waiting", "Ожидание доставки" },
            { "finished", "Завершен" },
            { "canceled", "Отменен" }, // последний всегда тот, который обозначает отмену/сброс заказа
        };
    }
}
