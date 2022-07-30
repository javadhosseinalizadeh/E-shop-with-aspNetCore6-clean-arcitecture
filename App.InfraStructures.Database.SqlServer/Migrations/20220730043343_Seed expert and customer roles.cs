using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.InfraStructures.Database.SqlServer.Migrations
{
    public partial class Seedexpertandcustomerroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 42242345,
                column: "ConcurrencyStamp",
                value: "b41b7551-a8e9-45a5-8fdf-05ca2c615d1b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 4572, "cd7a021d-9e8e-40cf-9084-0c8c2a12fea1", "Expert", "EXPERT" },
                    { 5234, "51a27f74-890c-46f8-b0aa-325d884f864d", "Customer", "CUSTOMER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 16455435,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4004586b-f4e1-43f3-b707-c09a3cdcfa2a", "AQAAAAEAACcQAAAAEGKOGHEGRWAYILfAm9uKUfJZrfebuSg9ylYhvzhdInjkeNKb4wGRjunphrW0NpImEQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4572);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5234);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 42242345,
                column: "ConcurrencyStamp",
                value: "7346ce66-ab7a-4eed-a889-a4148d46914f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 16455435,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d69e6d80-28e6-4161-a747-26416b35a93e", "AQAAAAEAACcQAAAAEJHMgw9diVx95FCnNZd3IrS6qEsh6alI8SumRejah8YXyoYc0ewvd+oDy3MplhlE0w==" });
        }
    }
}
