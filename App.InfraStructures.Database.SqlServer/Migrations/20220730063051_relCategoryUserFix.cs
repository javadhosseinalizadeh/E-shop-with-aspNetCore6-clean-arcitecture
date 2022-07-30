using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.InfraStructures.Database.SqlServer.Migrations
{
    public partial class relCategoryUserFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_AppUserId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AppUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Categories");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Categories",
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
                name: "IX_Categories_AppUserId",
                table: "Categories",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_AppUserId",
                table: "Categories",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
