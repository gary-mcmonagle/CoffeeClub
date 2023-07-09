using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeClub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoffeeBeans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Roast = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeBeans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrinkOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoffeeBeanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Drink = table.Column<int>(type: "int", nullable: false),
                    MilkType = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrinkOrders_CoffeeBeans_CoffeeBeanId",
                        column: x => x.CoffeeBeanId,
                        principalTable: "CoffeeBeans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkOrders_CoffeeBeanId",
                table: "DrinkOrders",
                column: "CoffeeBeanId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkOrders_OrderId",
                table: "DrinkOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkOrders");

            migrationBuilder.DropTable(
                name: "CoffeeBeans");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
