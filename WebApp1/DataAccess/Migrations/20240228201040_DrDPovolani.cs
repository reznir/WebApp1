using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DrDPovolani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HRDINA_POVOLANI",
                columns: table => new
                {
                    HRDINA_ID = table.Column<int>(type: "int", nullable: false),
                    POVOLANI_ID = table.Column<int>(type: "int", nullable: false),
                    LEVEL = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRDINA_POVOLANI", x => new { x.HRDINA_ID, x.POVOLANI_ID });
                    table.ForeignKey(
                        name: "FK_HRDINA_POVOLANI_HRDINA_HRDINA_ID",
                        column: x => x.HRDINA_ID,
                        principalTable: "HRDINA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HRDINA_POVOLANI_POVOLANI_POVOLANI_ID",
                        column: x => x.POVOLANI_ID,
                        principalTable: "POVOLANI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HRDINA_POVOLANI_POVOLANI_ID",
                table: "HRDINA_POVOLANI",
                column: "POVOLANI_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HRDINA_POVOLANI");
        }
    }
}
