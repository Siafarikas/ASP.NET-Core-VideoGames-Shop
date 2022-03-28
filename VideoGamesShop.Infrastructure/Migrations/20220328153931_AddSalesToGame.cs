using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGamesShop.Infrastructure.Data.Migrations
{
    public partial class AddSalesToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sales",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sales",
                table: "Games");
        }
    }
}
