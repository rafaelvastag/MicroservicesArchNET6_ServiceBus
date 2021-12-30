using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCartAPI.Migrations
{
    public partial class AddProductAndCartModels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartDetailsId",
                table: "CartDetails",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartDetails",
                newName: "CartDetailsId");
        }
    }
}
