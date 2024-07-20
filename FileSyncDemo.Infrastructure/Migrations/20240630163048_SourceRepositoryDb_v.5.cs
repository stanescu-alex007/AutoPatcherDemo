using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileSyncDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SourceRepositoryDb_v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "SourceFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "SourceFiles");
        }
    }
}
