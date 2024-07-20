using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileSyncDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UnionOfSpainFormation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReplicaFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "txt"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplicaFiles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplicaFiles");
        }
    }
}
