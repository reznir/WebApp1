using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class foreignKeys3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LIT_TEXT_SVATEK_SVATEK_ID",
                table: "LIT_TEXT");

            migrationBuilder.DropIndex(
                name: "IX_LIT_TEXT_SVATEK_ID",
                table: "LIT_TEXT");

            migrationBuilder.AddColumn<int>(
                name: "NavSvatekId",
                table: "LIT_TEXT",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LIT_TEXT_NavSvatekId",
                table: "LIT_TEXT",
                column: "NavSvatekId");

            migrationBuilder.AddForeignKey(
                name: "FK_LIT_TEXT_SVATEK_NavSvatekId",
                table: "LIT_TEXT",
                column: "NavSvatekId",
                principalTable: "SVATEK",
                principalColumn: "ID");
        }
    }
}
