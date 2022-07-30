using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.InfraStructures.Database.SqlServer.Migrations
{
    public partial class AddCommentProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastEditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4572,
                column: "ConcurrencyStamp",
                value: "89fd597a-73d1-4557-8a1b-c370af0cf0ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5234,
                column: "ConcurrencyStamp",
                value: "a746fadc-fb4c-4bb0-a791-c1c7d7822723");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 42242345,
                column: "ConcurrencyStamp",
                value: "ff696498-c99e-4fcd-a90d-887355f17bf2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 16455435,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8cde9583-b978-4ff5-9aa9-22b83aa8c697", "AQAAAAEAACcQAAAAEGQYQ49ZeiRb4eh/3CssoXJnu7NX3wOdau5r3zFeqcljqU3XXoZYbSLdfsBbrh/rUw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AppUserId",
                table: "Comment",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4572,
                column: "ConcurrencyStamp",
                value: "00caccdd-ec10-4fe3-b35f-6d47e0b5dd75");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5234,
                column: "ConcurrencyStamp",
                value: "f3aa7837-6f33-4fd2-ad10-a1bcb45ee263");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 42242345,
                column: "ConcurrencyStamp",
                value: "97918237-d22c-458a-8a91-134dd94ad325");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 16455435,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cd9dca6b-a047-4d0e-b6cf-53fd26bebffb", "AQAAAAEAACcQAAAAENqKtttmjbLAxfme+hKnSbIn1UDibXQKO++h2s0rHic8Cz14J/wNMfjHHuMfdDywvA==" });
        }
    }
}
