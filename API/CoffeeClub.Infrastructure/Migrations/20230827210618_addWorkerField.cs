using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeClub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addWorkerField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWorker",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWorker",
                table: "Users");
        }
    }
}
