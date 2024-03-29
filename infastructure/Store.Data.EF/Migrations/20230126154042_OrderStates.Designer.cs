﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Data.EF;

namespace Store.Data.EF.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20230126154042_OrderStates")]
    partial class OrderStates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Store.Data.CategoryDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImgLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Default"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Значки"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Плакаты"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Брелки"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Аксесуары"
                        });
                });

            modelBuilder.Entity("Store.Data.OrderDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CellPhone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("DeliveryDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryParameters")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DeliveryPrice")
                        .HasColumnType("money");

                    b.Property<string>("DeliveryUniqueCode")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool>("FullOrder")
                        .HasColumnType("bit");

                    b.Property<string>("OrderState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentParameters")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentServiceName")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("ShippingAddress")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("ShippingCity")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("ShippingCountry")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("ShippingUserName")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Store.Data.OrderItemDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Store.Data.ProductDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 2,
                            Description = "Материал - металл. Диаметр значка 58 мм",
                            Price = 1.5m,
                            Title = "Значок Тетрадь смерти"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "Материал - металл. Диаметр значка 44 мм",
                            Price = 1.2m,
                            Title = "Значок Наруто Шипуден"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            Description = "Материал - металл. Диаметр значка 58 мм",
                            Price = 1m,
                            Title = "Значок Один кусок"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 3,
                            Description = "Формат А3(29,7см х42 см). плотность бумаги 150гр",
                            Price = 4m,
                            Title = "Плакат Ван пис"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 3,
                            Description = "Формат А3(29,7см х42 см). плотность бумаги 150гр",
                            Price = 10m,
                            Title = "Плакат БТС"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 3,
                            Description = "Формат А3(29,7см х42 см). плотность бумаги 150гр",
                            Price = 1m,
                            Title = "Плакат Геншын Инфаркт"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 4,
                            Description = "Размер: 4х5.5 см. Материал: PVC",
                            Price = 2m,
                            Title = "Брелок Тетрадь смерти"
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 4,
                            Description = "Размер: 4х5.5 см. Материал: PVC",
                            Price = 2m,
                            Title = "Брелок Атака Гигантов"
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 4,
                            Description = "Размер: 4х5.5 см. Материал: PVC",
                            Price = 2m,
                            Title = "Брелок Девочки Волшебницы"
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 5,
                            Description = "Размер: 4х5.5 см. Материал: PVC",
                            Price = 2m,
                            Title = "Кольцо БТС"
                        },
                        new
                        {
                            Id = 11,
                            CategoryId = 5,
                            Description = "Размер: 4х5.5 см. Материал: PVC",
                            Price = 2m,
                            Title = "Браслет Кросс Фаер)"
                        },
                        new
                        {
                            Id = 12,
                            CategoryId = 5,
                            Description = "Размер: 4х5.5 см. Материал: PVC",
                            Price = 2m,
                            Title = "Очки \"Как у Двачера\""
                        });
                });

            modelBuilder.Entity("Store.Data.ProductImgLinkDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImgLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsThumbnail")
                        .HasColumnType("bit");

                    b.Property<int>("PersonalId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Store.Data.UserDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "admin",
                            Password = "$MYHASH$V1$10000$iSZbCJtBHeAXae7+tfKFjYMBn+ZhygDcDdytZ+e2uSm47Y5C"
                        });
                });

            modelBuilder.Entity("Store.Data.OrderItemDto", b =>
                {
                    b.HasOne("Store.Data.OrderDto", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Store.Data.OrderDto", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
