using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileSyncDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SourceRepositoryDb_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCreated",
                table: "SourceFiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCreated",
                table: "SourceFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
