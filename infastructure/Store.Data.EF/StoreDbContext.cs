using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF
{
    public class StoreDbContext : DbContext
    {
        public DbSet<ProductDto> Products { get; set; }

        public DbSet<OrderDto> Orders { get; set; }

        public DbSet<OrderItemDto> OrderItems { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildProducts(modelBuilder);
            BuildOrders(modelBuilder);
            BuildOrderItems(modelBuilder);
        }

        private void BuildOrderItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItemDto>(action =>
            {
                action.Property(dto => dto.Price)
                      .HasColumnType("money");

                action.HasOne(dto => dto.Order)
                      .WithMany(dto => dto.Items)
                      .IsRequired();
            });
        }

        private static void BuildOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDto>(action =>
            {
                action.Property(dto => dto.CellPhone)
                      .HasMaxLength(20);

                action.Property(dto => dto.DeliveryUniqueCode)
                      .HasMaxLength(40);

                action.Property(dto => dto.DeliveryPrice)
                      .HasColumnType("money");

                action.Property(dto => dto.PaymentServiceName)
                      .HasMaxLength(40);

                action.Property(dto => dto.DeliveryParameters)
                      .HasConversion(
                          value => JsonConvert.SerializeObject(value),
                          value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                      .Metadata.SetValueComparer(DictionaryComparer);

                action.Property(dto => dto.PaymentParameters)
                      .HasConversion(
                          value => JsonConvert.SerializeObject(value),
                          value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                      .Metadata.SetValueComparer(DictionaryComparer);
            });
        }

        private static readonly ValueComparer DictionaryComparer =
            new ValueComparer<Dictionary<string, string>>(
                (dictionary1, dictionary2) => dictionary1.SequenceEqual(dictionary2),
                dictionary => dictionary.Aggregate(
                    0,
                    (a, p) => HashCode.Combine(HashCode.Combine(a, p.Key.GetHashCode()), p.Value.GetHashCode())
                )
            );

        private static void BuildProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDto>(action =>
            {

                action.Property(dto => dto.Title)
                      .IsRequired();

                action.Property(dto => dto.Price)
                      .HasColumnType("money");


                action.HasData(
        
            new ProductDto{
                        Id = 1,
                        Title = "Значок Тетрадь смерти",
                        CategoryId = TemporaryData.categories[0].Id,
                        Description = "Материал - металл. " +
                "Диаметр значка 58 мм", 
                        Price =  1.5m },
            new ProductDto{
                Id = 2, 
                Title = "Значок Наруто Шипуден",
                CategoryId = TemporaryData.categories[0].Id,
                Description = "Материал - металл. " +
                "Диаметр значка 44 мм",
                Price = 1.2m },
            new ProductDto{
                Id = 3,
                Title = "Значок Один кусок",
                CategoryId = TemporaryData.categories[0].Id,
                Description = "Материал - металл. " +
                "Диаметр значка 58 мм",
                Price = 1m },

            new ProductDto{
                Id = 4,
                Title = "Плакат Ван пис",
                CategoryId = TemporaryData.categories[1].Id,
                Description = "Формат А3(29,7см х42 см). " +
                "плотность бумаги 150гр",
                Price = 4m },
            new ProductDto{
                Id = 5,
                Title = "Плакат БТС",
                CategoryId = TemporaryData.categories[1].Id,
                Description = "Формат А3(29,7см х42 см). " +
                "плотность бумаги 150гр",
                Price = 10m },
            new ProductDto{
                Id = 6,
                Title = "Плакат Геншын Инфаркт",
                CategoryId = TemporaryData.categories[1].Id,
                Description = "Формат А3(29,7см х42 см). " +
                "плотность бумаги 150гр",
                Price = 1m },

            new ProductDto{
                Id = 7,
                Title = "Брелок Тетрадь смерти",
                CategoryId = TemporaryData.categories[2].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m },
            new ProductDto{
                Id = 8,
                Title = "Брелок Атака Гигантов",
                CategoryId = TemporaryData.categories[2].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m },
            new ProductDto{
                Id = 9,
                Title = "Брелок Девочки Волшебницы",
                CategoryId = TemporaryData.categories[2].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m },

            new ProductDto{
                Id = 10,
                Title = "Кольцо БТС",
                CategoryId = TemporaryData.categories[3].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m },
            new ProductDto{
                Id = 11,
                Title = "Браслет Кросс Фаер)",
                CategoryId = TemporaryData.categories[3].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m },
            new ProductDto{
                Id = 12,
                Title = "Очки \"Как у Двачера\"",
                CategoryId = TemporaryData.categories[3].Id,
                Description = "Размер: 4х5.5 см. " +
                "Материал: PVC",
                Price = 2m }
                );
            });
        }
    }
}

