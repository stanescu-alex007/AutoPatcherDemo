using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileSyncDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SourceFileId",
                table: "ReplicaFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ReplicaFiles_SourceFileId",
                table: "ReplicaFiles",
                column: "SourceFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplicaFiles_SourceFiles_SourceFileId",
                table: "ReplicaFiles",
                column: "SourceFileId",
                principalTable: "SourceFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplicaFiles_SourceFiles_SourceFileId",
                table: "ReplicaFiles");

            migrationBuilder.DropIndex(
                name: "IX_ReplicaFiles_SourceFileId",
                table: "ReplicaFiles");

            migrationBuilder.DropColumn(
                name: "SourceFileId",
                table: "ReplicaFiles");
        }
    }
}
