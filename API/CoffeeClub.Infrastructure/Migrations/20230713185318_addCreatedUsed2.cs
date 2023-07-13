using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeClub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCreatedUsed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "Users");
        }
    }
}
