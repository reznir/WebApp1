using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DOBA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DOBA_POPIS = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CELY_NAZEV = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOBA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SVATEK",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAZEV_DNE = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    CELY_NAZEV = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    CTENI = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    BARVA = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CYKLY = table.Column<bool>(type: "bit", nullable: true),
                    DOBA_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SVATEK", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SVATEK_DOBA_DOBA_ID",
                        column: x => x.DOBA_ID,
                        principalTable: "DOBA",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LIT_TEXT",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAZEV_DNE = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    CYKLUS = table.Column<string>(type: "NVARCHAR(1)", nullable: false),
                    TEXT = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    SVATEK_ID = table.Column<int>(type: "int", nullable: false),
                    NavSvatekId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LIT_TEXT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LIT_TEXT_SVATEK_NavSvatekId",
                        column: x => x.NavSvatekId,
                        principalTable: "SVATEK",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LIT_TEXT_NavSvatekId",
                table: "LIT_TEXT",
                column: "NavSvatekId");

            migrationBuilder.CreateIndex(
                name: "IX_SVATEK_DOBA_ID",
                table: "SVATEK",
                column: "DOBA_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LIT_TEXT");

            migrationBuilder.DropTable(
                name: "SVATEK");

            migrationBuilder.DropTable(
                name: "DOBA");
        }
    }
}
