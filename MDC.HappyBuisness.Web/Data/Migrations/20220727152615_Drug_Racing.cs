using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDC.HappyBuisness.Web.Data.Migrations
{
    public partial class Drug_Racing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Drugs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Drugs");
        }
    }
}
