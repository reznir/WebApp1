using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _20240227160315_InitialDrd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HRDINA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    OHROZENI = table.Column<int>(type: "int", nullable: false),
                    VYHODA = table.Column<int>(type: "int", nullable: false),
                    TELO_LIMIT = table.Column<int>(type: "int", nullable: false),
                    TELO = table.Column<int>(type: "int", nullable: false),
                    TELO_JIZVA = table.Column<int>(type: "int", nullable: false),
                    DUSE_LIMIT = table.Column<int>(type: "int", nullable: false),
                    DUSE = table.Column<int>(type: "int", nullable: false),
                    DUSE_JIZVA = table.Column<int>(type: "int", nullable: false),
                    VLIV_LIMIT = table.Column<int>(type: "int", nullable: false),
                    VLIV = table.Column<int>(type: "int", nullable: false),
                    VLIV_JIZVA = table.Column<int>(type: "int", nullable: false),
                    PENIZE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SUROVINY = table.Column<int>(type: "int", nullable: false),
                    VYBAVENI = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    RASA = table.Column<string>(type: "NVARCHAR(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRDINA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "POVOLANI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POVOLANI", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SCHOPNOST",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR(500)", nullable: false),
                    POVOLANI_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHOPNOST", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SCHOPNOST_POVOLANI_POVOLANI_ID",
                        column: x => x.POVOLANI_ID,
                        principalTable: "POVOLANI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HRDINA_SCHOPNOST",
                columns: table => new
                {
                    HRDINA_ID = table.Column<int>(type: "int", nullable: false),
                    SCHOPNOST_ID = table.Column<int>(type: "int", nullable: false),
                    LEVEL = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRDINA_SCHOPNOST", x => new { x.HRDINA_ID, x.SCHOPNOST_ID });
                    table.ForeignKey(
                        name: "FK_HRDINA_SCHOPNOST_HRDINA_HRDINA_ID",
                        column: x => x.HRDINA_ID,
                        principalTable: "HRDINA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HRDINA_SCHOPNOST_SCHOPNOST_SCHOPNOST_ID",
                        column: x => x.SCHOPNOST_ID,
                        principalTable: "SCHOPNOST",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HRDINA_SCHOPNOST_SCHOPNOST_ID",
                table: "HRDINA_SCHOPNOST",
                column: "SCHOPNOST_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHOPNOST_POVOLANI_ID",
                table: "SCHOPNOST",
                column: "POVOLANI_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HRDINA_SCHOPNOST");

            migrationBuilder.DropTable(
                name: "HRDINA");

            migrationBuilder.DropTable(
                name: "SCHOPNOST");

            migrationBuilder.DropTable(
                name: "POVOLANI");
        }
    }
}
