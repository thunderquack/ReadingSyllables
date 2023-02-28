using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingSyllables.Migrations
{
    /// <inheritdoc />
    public partial class counter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShowCounter",
                table: "Syllables",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowCounter",
                table: "Syllables");
        }
    }
}
