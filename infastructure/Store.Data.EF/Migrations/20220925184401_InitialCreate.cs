﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DeliveryUniqueCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DeliveryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryPrice = table.Column<decimal>(type: "money", nullable: false),
                    DeliveryParameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentServiceName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    PaymentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentParameters = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Материал - металл. Диаметр значка 58 мм", 1.5m, "Значок Тетрадь смерти" },
                    { 2, 1, "Материал - металл. Диаметр значка 44 мм", 1.2m, "Значок Наруто Шипуден" },
                    { 3, 1, "Материал - металл. Диаметр значка 58 мм", 1m, "Значок Один кусок" },
                    { 4, 2, "Формат А3(29,7см х42 см). плотность бумаги 150гр", 4m, "Плакат Ван пис" },
                    { 5, 2, "Формат А3(29,7см х42 см). плотность бумаги 150гр", 10m, "Плакат БТС" },
                    { 6, 2, "Формат А3(29,7см х42 см). плотность бумаги 150гр", 1m, "Плакат Геншын Инфаркт" },
                    { 7, 3, "Размер: 4х5.5 см. Материал: PVC", 2m, "Брелок Тетрадь смерти" },
                    { 8, 3, "Размер: 4х5.5 см. Материал: PVC", 2m, "Брелок Атака Гигантов" },
                    { 9, 3, "Размер: 4х5.5 см. Материал: PVC", 2m, "Брелок Девочки Волшебницы" },
                    { 10, 4, "Размер: 4х5.5 см. Материал: PVC", 2m, "Кольцо БТС" },
                    { 11, 4, "Размер: 4х5.5 см. Материал: PVC", 2m, "Браслет Кросс Фаер)" },
                    { 12, 4, "Размер: 4х5.5 см. Материал: PVC", 2m, "Очки \"Как у Двачера\"" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
