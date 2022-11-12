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

        public DbSet<CategoryDto> Categories { get; set; }

        public DbSet<UserDto> Users { get; set; }

        public DbSet<ProductImgLinkDto> ProductImages { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildProducts(modelBuilder);
            BuildOrders(modelBuilder);
            BuildOrderItems(modelBuilder);
            BuildCategories(modelBuilder);
            BuildUsers(modelBuilder);
            BuildProductImages(modelBuilder);
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

                action.Property(dto => dto.ShippingUserName)
                      .HasMaxLength(40);
                action.Property(dto => dto.ShippingAddress)
                      .HasMaxLength(40);
                action.Property(dto => dto.ShippingCity)
                      .HasMaxLength(40);
                action.Property(dto => dto.ShippingCountry)
                      .HasMaxLength(40);

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

                action.HasData( TemporaryData.ProductDtoList );
            });
        }

        private void BuildCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryDto>(action =>
            {
                action.Property(dto => dto.Name)
                      .IsRequired();

                action.HasData( TemporaryData.CategoryDtoList );
            });
        }

        private void BuildUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDto>(action =>
            {
                action.Property(dto => dto.Login)
                      .IsRequired();

                action.Property(dto => dto.Password)
                      .IsRequired();

                action.HasData( TemporaryData.UserDtoList );
            });
        }

        private void BuildProductImages(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductImgLinkDto>(action =>
            {
                action.Property(dto => dto.ImgLink)
                      .IsRequired();

                action.Property(dto => dto.ProductId)
                      .IsRequired();

                action.Property(dto => dto.PersonalId)
                      .IsRequired();

                action.Property(dto => dto.IsThumbnail)
                      .IsRequired();
            });
        }
    }
}

