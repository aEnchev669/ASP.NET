using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserBook_AspNetUsers_CollectorId",
                table: "IdentityUserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserBook_Books_BookId",
                table: "IdentityUserBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserBook",
                table: "IdentityUserBook");

            migrationBuilder.RenameTable(
                name: "IdentityUserBook",
                newName: "IdentityUsers");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityUserBook_CollectorId",
                table: "IdentityUsers",
                newName: "IX_IdentityUsers_CollectorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUsers",
                table: "IdentityUsers",
                columns: new[] { "BookId", "CollectorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUsers_AspNetUsers_CollectorId",
                table: "IdentityUsers",
                column: "CollectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUsers_Books_BookId",
                table: "IdentityUsers",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUsers_AspNetUsers_CollectorId",
                table: "IdentityUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUsers_Books_BookId",
                table: "IdentityUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUsers",
                table: "IdentityUsers");

            migrationBuilder.RenameTable(
                name: "IdentityUsers",
                newName: "IdentityUserBook");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityUsers_CollectorId",
                table: "IdentityUserBook",
                newName: "IX_IdentityUserBook_CollectorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserBook",
                table: "IdentityUserBook",
                columns: new[] { "BookId", "CollectorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserBook_AspNetUsers_CollectorId",
                table: "IdentityUserBook",
                column: "CollectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserBook_Books_BookId",
                table: "IdentityUserBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
