using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.InfraStructures.Database.SqlServer.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("02705c71-654f-4b48-99c0-bd0bf9fff578"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "FirstName", "HomeAddress", "IsActive", "LastName", "Password", "PictureFileId", "RoleId", "UserName" },
                values: new object[] { new Guid("7b699e70-38b7-44a8-97b8-4b7a0d397260"), "Javad", "Gilan", true, "HosseinAliZadeh", "1234", 0, 1, "javad.alizadeh" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { 2, "ExpertUser", "متخصص" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTitle" },
                values: new object[] { 3, "Customer", "مشتری" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "FirstName", "HomeAddress", "IsActive", "LastName", "Password", "PictureFileId", "RoleId", "UserName" },
                values: new object[] { new Guid("a954090b-d0a4-4972-b047-650f11913b6b"), "Ali", "Gilan", true, "AliZadeh", "1234", 0, 3, "Ali.alizadeh" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "FirstName", "HomeAddress", "IsActive", "LastName", "Password", "PictureFileId", "RoleId", "UserName" },
                values: new object[] { new Guid("b89ffd4c-b087-42e0-b106-cda180d3f46e"), "Meysam", "Tehran", true, "Hosseini", "1234", 0, 2, "meysam.hosseini" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "FirstName", "HomeAddress", "IsActive", "LastName", "Password", "PictureFileId", "RoleId", "UserName" },
                values: new object[] { new Guid("02705c71-654f-4b48-99c0-bd0bf9fff578"), "Javad", "Gilan", true, "HosseinAliZadeh", "1234", 0, 1, "javad.alizadeh" });
        }
    }
}
