using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.InfraStructures.Database.SqlServer.Migrations
{
    public partial class addIdentityFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3a8d464c-4e20-4474-a3fe-77daa6a5e8eb");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a0a674f1-4e69-4435-9851-84c2b5668a0f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ce925506-14f4-4656-8984-6299b6b9ae1c");

            migrationBuilder.DropColumn(
                name: "PictureFileId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PhotoPath" },
                values: new object[] { "18df9b0c-65d7-49da-ba23-7efcc608ad98", "" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "PictureFileId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9803349b-c872-4729-aede-29f7100d7d05");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3a8d464c-4e20-4474-a3fe-77daa6a5e8eb", "3c265435-bc45-4ae1-a74e-8ba5f53d15ae", "Admin", "admin" },
                    { "a0a674f1-4e69-4435-9851-84c2b5668a0f", "992b9a5d-081c-4393-8807-9936812c22f5", "Customer", "customer" },
                    { "ce925506-14f4-4656-8984-6299b6b9ae1c", "251bdcc1-81a2-44bd-885b-de97890678a3", "ExpertUser", "admin" }
                });
        }
    }
}
