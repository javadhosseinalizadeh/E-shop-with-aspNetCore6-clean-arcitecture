using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.InfraStructures.Database.SqlServer.Migrations
{
    public partial class buildNewDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a46b6984-69cb-41ff-b401-5e01a8214672");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "de9e0eb4-79d3-4438-b8d6-4f767d46c96b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fe8dd509-00ef-4635-aaae-9af5045b5e35");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ec007b40-bf36-42ce-989c-e8f735821b36");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9d2870ab-ae7f-4d7d-a787-5757f63cf738", "cf0026e1-e98d-403c-94af-32b1fdf8af80", "ExpertUser", "admin" },
                    { "a241d544-c3f3-4e62-a464-24804dcb8fd8", "4c27efdf-ff83-44d8-80b4-4efbbe4b5849", "Customer", "customer" },
                    { "a88a7aaf-8854-4d3f-b92a-5f488c906332", "60626eb6-9215-4fbf-bf73-30c12a5d51f1", "Admin", "admin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9d2870ab-ae7f-4d7d-a787-5757f63cf738");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a241d544-c3f3-4e62-a464-24804dcb8fd8");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a88a7aaf-8854-4d3f-b92a-5f488c906332");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "18df9b0c-65d7-49da-ba23-7efcc608ad98");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a46b6984-69cb-41ff-b401-5e01a8214672", "bb3de2a9-6069-4dde-b301-f87755be1700", "Customer", "customer" },
                    { "de9e0eb4-79d3-4438-b8d6-4f767d46c96b", "d5828172-a53b-4b59-be8c-0ff9655365b1", "Admin", "admin" },
                    { "fe8dd509-00ef-4635-aaae-9af5045b5e35", "5a0355c0-feae-4b37-b0f3-9c638b441f70", "ExpertUser", "admin" }
                });
        }
    }
}
