using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.EF.Migrations
{
    public partial class ImagesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgLink",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImgLinkItemDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDtoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImgLinkItemDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImgLinkItemDto_Products_ProductDtoId",
                        column: x => x.ProductDtoId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImgLink",
                value: "../wwwroot/Img/Empty.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImgLink",
                value: "../wwwroot/Img/Empty.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImgLink",
                value: "../wwwroot/Img/Empty.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImgLink",
                value: "../wwwroot/Img/Empty.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImgLink",
                value: "../wwwroot/Img/Empty.jpg");

            migrationBuilder.CreateIndex(
                name: "IX_ImgLinkItemDto_ProductDtoId",
                table: "ImgLinkItemDto",
                column: "ProductDtoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImgLinkItemDto");

            migrationBuilder.DropColumn(
                name: "ImgLink",
                table: "Categories");
        }
    }
}
