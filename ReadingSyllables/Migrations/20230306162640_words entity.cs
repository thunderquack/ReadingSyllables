using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingSyllables.Migrations
{
    /// <inheritdoc />
    public partial class wordsentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Syllables",
                table: "Syllables");

            migrationBuilder.RenameTable(
                name: "Syllables",
                newName: "Syllable");

            migrationBuilder.RenameIndex(
                name: "IX_Syllables_Name",
                table: "Syllable",
                newName: "IX_Syllable_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Syllable",
                table: "Syllable",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NextShow = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Show = table.Column<int>(type: "INTEGER", nullable: false),
                    ShowCounter = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyllableWord",
                columns: table => new
                {
                    SyllablesId = table.Column<int>(type: "INTEGER", nullable: false),
                    WordsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyllableWord", x => new { x.SyllablesId, x.WordsId });
                    table.ForeignKey(
                        name: "FK_SyllableWord_Syllable_SyllablesId",
                        column: x => x.SyllablesId,
                        principalTable: "Syllable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SyllableWord_Word_WordsId",
                        column: x => x.WordsId,
                        principalTable: "Word",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SyllableWord_WordsId",
                table: "SyllableWord",
                column: "WordsId");

            migrationBuilder.CreateIndex(
                name: "IX_Word_Name",
                table: "Word",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SyllableWord");

            migrationBuilder.DropTable(
                name: "Word");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Syllable",
                table: "Syllable");

            migrationBuilder.RenameTable(
                name: "Syllable",
                newName: "Syllables");

            migrationBuilder.RenameIndex(
                name: "IX_Syllable_Name",
                table: "Syllables",
                newName: "IX_Syllables_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Syllables",
                table: "Syllables",
                column: "Id");
        }
    }
}