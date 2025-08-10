using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommerce.Data.Migrations
{
    public partial class AddProductEmbedding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Embedding",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Embedding",
                table: "Products");
        }
    }
}
