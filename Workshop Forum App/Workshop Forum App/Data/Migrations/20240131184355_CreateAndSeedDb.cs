using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Workshop_Forum_App.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateAndSeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[,]
                {
                    { new Guid("0034ba7e-75d9-42d6-bda0-c563db1b937c"), "3 - Lorem ipsum dolor sit amet,", "My third post" },
                    { new Guid("0e3a1df3-48d4-47b0-abf7-b7ee263ac15f"), "Lorem ipsum dolor sit amet,", "My first post" },
                    { new Guid("f0661842-4f0a-46ad-b684-08f424e0ae9f"), "2 - Lorem ipsum dolor sit amet,", "My second post" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
