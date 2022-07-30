using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.InfraStructures.Database.SqlServer.Migrations
{
    public partial class relFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4572,
                column: "ConcurrencyStamp",
                value: "d3051108-744f-4f0f-aad5-74f45e121eb6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5234,
                column: "ConcurrencyStamp",
                value: "f1874fb2-d818-4389-a59e-f0ae90a849d5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 42242345,
                column: "ConcurrencyStamp",
                value: "430e9b2a-7a0e-4845-96d5-ca7009395230");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 16455435,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9ed0696f-30ed-4a46-904d-bf64d06f6927", "AQAAAAEAACcQAAAAEPI6pDmCTzC5pyzywxS0K8w11/c5wdzItgDKXcWxky2+9+AN9F575IxCWkIu7VvYow==" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AppUserId",
                table: "Orders",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AppUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4572,
                column: "ConcurrencyStamp",
                value: "cd7a021d-9e8e-40cf-9084-0c8c2a12fea1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5234,
                column: "ConcurrencyStamp",
                value: "51a27f74-890c-46f8-b0aa-325d884f864d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 42242345,
                column: "ConcurrencyStamp",
                value: "b41b7551-a8e9-45a5-8fdf-05ca2c615d1b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 16455435,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4004586b-f4e1-43f3-b707-c09a3cdcfa2a", "AQAAAAEAACcQAAAAEGKOGHEGRWAYILfAm9uKUfJZrfebuSg9ylYhvzhdInjkeNKb4wGRjunphrW0NpImEQ==" });
        }
    }
}
