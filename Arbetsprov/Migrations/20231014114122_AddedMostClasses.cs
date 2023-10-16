using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arbetsprov.Migrations
{
    public partial class AddedMostClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscountDaysPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    DiscountPercentage = table.Column<int>(nullable: false),
                    Service = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountDaysPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountDaysPeriod_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceOverride",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceAPrice = table.Column<float>(nullable: true),
                    ServiceBPrice = table.Column<float>(nullable: true),
                    ServiceCPrice = table.Column<float>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceOverride", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceOverride_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountDaysPeriod_UserId",
                table: "DiscountDaysPeriod",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceOverride_UserId",
                table: "PriceOverride",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountDaysPeriod");

            migrationBuilder.DropTable(
                name: "PriceOverride");
        }
    }
}
