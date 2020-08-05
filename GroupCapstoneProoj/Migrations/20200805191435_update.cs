using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCapstoneProoj.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0806662a-180a-47b6-844e-41ef810756df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "864120d6-a2c9-48c1-b1d8-7e33f01d56b4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "39b089b8-d46e-4d44-8f9b-5263a0f376a4", "aa83f142-f350-403f-a1ce-7ad2e6f9b73d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "782bd606-f57c-42b0-ad4b-733b56d29a4c", "d9e9e9b2-8072-40b1-9141-b4df6223f3af", "Trader", "TRADER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39b089b8-d46e-4d44-8f9b-5263a0f376a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "782bd606-f57c-42b0-ad4b-733b56d29a4c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "864120d6-a2c9-48c1-b1d8-7e33f01d56b4", "c5672856-d009-4588-bca7-1047d0c90ce9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0806662a-180a-47b6-844e-41ef810756df", "27183b47-5c9c-48c1-b268-503017ca9b59", "Trader", "TRADER" });
        }
    }
}
