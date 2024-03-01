using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DrDSchopnostVlastnost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VLASTNOST",
                table: "SCHOPNOST",
                type: "NVARCHAR(5)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VLASTNOST",
                table: "SCHOPNOST");
        }
    }
}
