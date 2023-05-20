using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class foreignKeys4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LIT_TEXT_SVATEK_SVATEK_ID",
                table: "LIT_TEXT");

            migrationBuilder.DropIndex(
                name: "IX_LIT_TEXT_SVATEK_ID",
                table: "LIT_TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SVATEK_DOBA_ID",
                table: "SVATEK",
                column: "DOBA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LIT_TEXT_SVATEK_ID",
                table: "LIT_TEXT",
                column: "SVATEK_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LIT_TEXT_SVATEK_SVATEK_ID",
                table: "LIT_TEXT",
                column: "SVATEK_ID",
                principalTable: "SVATEK",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SVATEK_DOBA_DOBA_ID",
                table: "SVATEK",
                column: "DOBA_ID",
                principalTable: "DOBA",
                principalColumn: "ID");
        }
    }
}
