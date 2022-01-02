using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCartAPI.Migrations
{
    public partial class updateheaderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartHeaders",
                newName: "CartHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartHeaderId",
                table: "CartHeaders",
                newName: "Id");
        }
    }
}
