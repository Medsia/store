using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.EF.Migrations
{
    public partial class ImagesUpdate_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "PersonalId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalId",
                table: "ProductImages");

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImgLink", "IsThumbnail", "ProductId" },
                values: new object[] { 1, "/Img/Products/AMONG_ASS.jpg", true, 1 });
        }
    }
}
