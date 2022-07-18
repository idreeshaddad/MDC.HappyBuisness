using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MDC.HappyBuisness.Web.Data.Migrations
{
    public partial class Deal_TransactionCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TransactionCode",
                table: "Deals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionCode",
                table: "Deals");
        }
    }
}
