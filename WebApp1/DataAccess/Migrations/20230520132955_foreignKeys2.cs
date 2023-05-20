using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class foreignKeys2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "NavSvatekId",
                table: "LIT_TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NavSvatekId",
                table: "LIT_TEXT",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SVATEK_DOBA_ID",
                table: "SVATEK",
                column: "DOBA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LIT_TEXT_NavSvatekId",
                table: "LIT_TEXT",
                column: "NavSvatekId");

            migrationBuilder.AddForeignKey(
                name: "FK_LIT_TEXT_SVATEK_NavSvatekId",
                table: "LIT_TEXT",
                column: "NavSvatekId",
                principalTable: "SVATEK",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SVATEK_DOBA_DOBA_ID",
                table: "SVATEK",
                column: "DOBA_ID",
                principalTable: "DOBA",
                principalColumn: "ID");
        }
    }
}
