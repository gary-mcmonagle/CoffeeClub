using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeClub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCreatedUsed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "CoffeeBeans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeBeans_CreatedById",
                table: "CoffeeBeans",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeBeans_Users_CreatedById",
                table: "CoffeeBeans",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeBeans_Users_CreatedById",
                table: "CoffeeBeans");

            migrationBuilder.DropIndex(
                name: "IX_CoffeeBeans_CreatedById",
                table: "CoffeeBeans");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CoffeeBeans");
        }
    }
}
