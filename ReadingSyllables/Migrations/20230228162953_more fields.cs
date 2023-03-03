using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingSyllables.Migrations
{
    /// <inheritdoc />
    public partial class morefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Syllables",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextShow",
                table: "Syllables",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Syllables",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Show",
                table: "Syllables",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Syllables");

            migrationBuilder.DropColumn(
                name: "NextShow",
                table: "Syllables");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Syllables");

            migrationBuilder.DropColumn(
                name: "Show",
                table: "Syllables");
        }
    }
}
