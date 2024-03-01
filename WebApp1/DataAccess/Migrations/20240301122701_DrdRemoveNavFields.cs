using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DrdRemoveNavFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRDINA_HRDINA_HrdinaID",
                table: "HRDINA");

            migrationBuilder.DropForeignKey(
                name: "FK_HRDINA_SCHOPNOST_SchopnostID",
                table: "HRDINA");

            migrationBuilder.DropIndex(
                name: "IX_HRDINA_HrdinaID",
                table: "HRDINA");

            migrationBuilder.DropIndex(
                name: "IX_HRDINA_SchopnostID",
                table: "HRDINA");

            migrationBuilder.DropColumn(
                name: "HrdinaID",
                table: "HRDINA");

            migrationBuilder.DropColumn(
                name: "SchopnostID",
                table: "HRDINA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HrdinaID",
                table: "HRDINA",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchopnostID",
                table: "HRDINA",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HRDINA_HrdinaID",
                table: "HRDINA",
                column: "HrdinaID");

            migrationBuilder.CreateIndex(
                name: "IX_HRDINA_SchopnostID",
                table: "HRDINA",
                column: "SchopnostID");

            migrationBuilder.AddForeignKey(
                name: "FK_HRDINA_HRDINA_HrdinaID",
                table: "HRDINA",
                column: "HrdinaID",
                principalTable: "HRDINA",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_HRDINA_SCHOPNOST_SchopnostID",
                table: "HRDINA",
                column: "SchopnostID",
                principalTable: "SCHOPNOST",
                principalColumn: "ID");
        }
    }
}
