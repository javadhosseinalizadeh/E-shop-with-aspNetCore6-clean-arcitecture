using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.InfraStructures.Database.SqlServer.Migrations
{
    public partial class CommentFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "ServiceComments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4572,
                column: "ConcurrencyStamp",
                value: "6c7aa2b3-72e3-4369-ba9b-798f562441c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5234,
                column: "ConcurrencyStamp",
                value: "80690d6a-f5f7-4aa3-9724-8957ecece13e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 42242345,
                column: "ConcurrencyStamp",
                value: "b8a646df-9e3c-4977-b1dd-ce5db74b16ab");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 16455435,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aab5fd6b-203c-4dbe-8d59-fd70c3cf69f5", "AQAAAAEAACcQAAAAEI97aee1JNJzkmB9wciGvVHkhkfJPi3eCq8aWHhBx1jbBm6pc3JKeT2P6/3D0GE8Ag==" });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceComments_AppUserId",
                table: "ServiceComments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceComments_AspNetUsers_AppUserId",
                table: "ServiceComments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceComments_AspNetUsers_AppUserId",
                table: "ServiceComments");

            migrationBuilder.DropIndex(
                name: "IX_ServiceComments_AppUserId",
                table: "ServiceComments");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ServiceComments");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastEditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
    }
}
