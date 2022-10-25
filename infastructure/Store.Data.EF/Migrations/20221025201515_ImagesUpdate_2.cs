using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.EF.Migrations
{
    public partial class ImagesUpdate_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImgLinkItemDto");

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsThumbnail = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImgLink", "IsThumbnail", "ProductId" },
                values: new object[] { 1, "/Img/Products/AMONG_ASS.jpg", true, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

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

            migrationBuilder.CreateIndex(
                name: "IX_ImgLinkItemDto_ProductDtoId",
                table: "ImgLinkItemDto",
                column: "ProductDtoId");
        }
    }
}
