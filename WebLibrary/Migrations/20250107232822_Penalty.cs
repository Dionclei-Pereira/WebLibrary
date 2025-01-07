using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Penalty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Penalty",
                table: "AspNetUsers",
                type: "double",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Penalty",
                table: "AspNetUsers");
        }
    }
}
