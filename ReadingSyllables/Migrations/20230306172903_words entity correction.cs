using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingSyllables.Migrations
{
    /// <inheritdoc />
    public partial class wordsentitycorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SyllableWord_Syllable_SyllablesId",
                table: "SyllableWord");

            migrationBuilder.DropForeignKey(
                name: "FK_SyllableWord_Word_WordsId",
                table: "SyllableWord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Word",
                table: "Word");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Syllable",
                table: "Syllable");

            migrationBuilder.RenameTable(
                name: "Word",
                newName: "Words");

            migrationBuilder.RenameTable(
                name: "Syllable",
                newName: "Syllables");

            migrationBuilder.RenameIndex(
                name: "IX_Word_Name",
                table: "Words",
                newName: "IX_Words_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Syllable_Name",
                table: "Syllables",
                newName: "IX_Syllables_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Words",
                table: "Words",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Syllables",
                table: "Syllables",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SyllableWord_Syllables_SyllablesId",
                table: "SyllableWord",
                column: "SyllablesId",
                principalTable: "Syllables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SyllableWord_Words_WordsId",
                table: "SyllableWord",
                column: "WordsId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SyllableWord_Syllables_SyllablesId",
                table: "SyllableWord");

            migrationBuilder.DropForeignKey(
                name: "FK_SyllableWord_Words_WordsId",
                table: "SyllableWord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Words",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Syllables",
                table: "Syllables");

            migrationBuilder.RenameTable(
                name: "Words",
                newName: "Word");

            migrationBuilder.RenameTable(
                name: "Syllables",
                newName: "Syllable");

            migrationBuilder.RenameIndex(
                name: "IX_Words_Name",
                table: "Word",
                newName: "IX_Word_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Syllables_Name",
                table: "Syllable",
                newName: "IX_Syllable_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Word",
                table: "Word",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Syllable",
                table: "Syllable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SyllableWord_Syllable_SyllablesId",
                table: "SyllableWord",
                column: "SyllablesId",
                principalTable: "Syllable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SyllableWord_Word_WordsId",
                table: "SyllableWord",
                column: "WordsId",
                principalTable: "Word",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
