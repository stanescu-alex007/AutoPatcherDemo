using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileSyncDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SourceRepositoryDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "SourceFiles");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "SourceFiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "SourceFiles");

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "SourceFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
