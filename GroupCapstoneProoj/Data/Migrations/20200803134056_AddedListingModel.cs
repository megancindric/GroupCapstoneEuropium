using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCapstoneProoj.Data.Migrations
{
    public partial class AddedListingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b58546ac-4bc2-4e7e-aba9-ba8cd2842798");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc86b0-f61e-40fc-814a-0e5635aee8c1");

            migrationBuilder.DropColumn(
                name: "PickupDay",
                table: "Traders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "08a191b0-0220-41d2-8a54-6d9c240bfc86", "43e05f82-0051-44f9-b594-4c282c8bcfb7", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4bde32ba-fd29-42ca-a9e1-aa8bac62dfcd", "b3025057-e247-479e-93e5-98d4b5f714ec", "Trader", "TRADER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08a191b0-0220-41d2-8a54-6d9c240bfc86");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4bde32ba-fd29-42ca-a9e1-aa8bac62dfcd");

            migrationBuilder.AddColumn<string>(
                name: "PickupDay",
                table: "Traders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dffc86b0-f61e-40fc-814a-0e5635aee8c1", "9d5cc199-90cb-4a63-b197-58e15be030de", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b58546ac-4bc2-4e7e-aba9-ba8cd2842798", "11de2095-3943-4534-9d2f-06da1ee28c06", "Trader", "TRADER" });
        }
    }
}
