using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshop__ASP.NET_Core_Identity.Data.Migrations
{
    public partial class DataSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Board identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Board name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Task identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Task title"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Task description"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Task creation"),
                    BoardId = table.Column<int>(type: "int", nullable: true, comment: "Bord identifier"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Application user identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Board tasks");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "79182b70-eb25-44ed-bc01-37abf1c08707", 0, "b1a2946e-1f7d-4281-ae09-b782b77bd038", null, false, false, null, null, "TEST@SORTUNI.BG", "AQAAAAEAACcQAAAAEOKzXgiHPZZ51OqgkfNwo0KPm/iT05Al5osN/E2jMxYus2cLaUIX5/Do+Uil5GzRtw==", null, false, "7d508a5c-c94d-4c2e-b6a6-c546f9c028a8", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 26, 17, 23, 41, 217, DateTimeKind.Local).AddTicks(2080), "Implement better styling for all public pages", "79182b70-eb25-44ed-bc01-37abf1c08707", "Improve CSS Styles" },
                    { 2, 1, new DateTime(2023, 9, 11, 17, 23, 41, 217, DateTimeKind.Local).AddTicks(2110), "Create Android client app for the RESTful API", "79182b70-eb25-44ed-bc01-37abf1c08707", "Android Client App" },
                    { 3, 2, new DateTime(2024, 1, 11, 17, 23, 41, 217, DateTimeKind.Local).AddTicks(2113), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "79182b70-eb25-44ed-bc01-37abf1c08707", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 2, 11, 17, 23, 41, 217, DateTimeKind.Local).AddTicks(2114), "Implement [Create Task] page for adding new tasks", "79182b70-eb25-44ed-bc01-37abf1c08707", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79182b70-eb25-44ed-bc01-37abf1c08707");
        }
    }
}
