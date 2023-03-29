using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingSyllables.Migrations
{
    /// <inheritdoc />
    public partial class uploadedfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HashSum = table.Column<string>(type: "TEXT", nullable: false),
                    UploadedFileType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_HashSum",
                table: "UploadedFiles",
                column: "HashSum",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadedFiles");
        }
    }
}
