using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.InfraStructures.Database.SqlServer.Migrations
{
    public partial class RelFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b699e70-38b7-44a8-97b8-4b7a0d397260"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("a954090b-d0a4-4972-b047-650f11913b6b"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("b89ffd4c-b087-42e0-b106-cda180d3f46e"));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "FirstName", "HomeAddress", "IsActive", "LastName", "Password", "PictureFileId", "RoleId", "UserName" },
                values: new object[] { new Guid("1ccd728e-214b-4064-a02b-0e7f6fefa111"), "Javad", "Gilan", true, "HosseinAliZadeh", "1234", 0, 1, "javad.alizadeh" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "FirstName", "HomeAddress", "IsActive", "LastName", "Password", "PictureFileId", "RoleId", "UserName" },
                values: new object[] { new Guid("299f89a1-a458-4a26-af6b-4559f847044e"), "Meysam", "Tehran", true, "Hosseini", "1234", 0, 2, "meysam.hosseini" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "FirstName", "HomeAddress", "IsActive", "LastName", "Password", "PictureFileId", "RoleId", "UserName" },
                values: new object[] { new Guid("38c90368-f90b-490a-bbac-5eeb77549154"), "Ali", "Gilan", true, "AliZadeh", "1234", 0, 3, "Ali.alizadeh" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId1",
                table: "Categories",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AppUsers_UserId1",
                table: "Categories",
                column: "UserId1",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AppUsers_UserId1",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserId1",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("1ccd728e-214b-4064-a02b-0e7f6fefa111"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("299f89a1-a458-4a26-af6b-4559f847044e"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("38c90368-f90b-490a-bbac-5eeb77549154"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "FirstName", "HomeAddress", "IsActive", "LastName", "Password", "PictureFileId", "RoleId", "UserName" },
                values: new object[] { new Guid("7b699e70-38b7-44a8-97b8-4b7a0d397260"), "Javad", "Gilan", true, "HosseinAliZadeh", "1234", 0, 1, "javad.alizadeh" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "FirstName", "HomeAddress", "IsActive", "LastName", "Password", "PictureFileId", "RoleId", "UserName" },
                values: new object[] { new Guid("a954090b-d0a4-4972-b047-650f11913b6b"), "Ali", "Gilan", true, "AliZadeh", "1234", 0, 3, "Ali.alizadeh" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "FirstName", "HomeAddress", "IsActive", "LastName", "Password", "PictureFileId", "RoleId", "UserName" },
                values: new object[] { new Guid("b89ffd4c-b087-42e0-b106-cda180d3f46e"), "Meysam", "Tehran", true, "Hosseini", "1234", 0, 2, "meysam.hosseini" });
        }
    }
}
