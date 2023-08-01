using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeClub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssignedToId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AssignedToId",
                table: "Orders",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_AssignedToId",
                table: "Orders",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_AssignedToId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AssignedToId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "Orders");
        }
    }
}
