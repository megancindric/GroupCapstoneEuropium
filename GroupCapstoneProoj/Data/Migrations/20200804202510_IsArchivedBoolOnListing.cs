using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCapstoneProoj.Data.Migrations
{
    public partial class IsArchivedBoolOnListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ccf173a-88d5-4b1d-9204-419511cc6f69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f38962ca-43fe-4eaa-9436-f04e7b7ea348");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "Listings");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Listings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "55d3a7aa-76b2-4e90-9ea3-4b9c34cec6a0", "59d3deb2-429f-44a6-bc9b-38707f2ae8a8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42f0461d-a154-4d7d-961b-150385cb3d02", "123c71e1-7fed-406c-a9a7-748ab55e10c9", "Trader", "TRADER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42f0461d-a154-4d7d-961b-150385cb3d02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55d3a7aa-76b2-4e90-9ea3-4b9c34cec6a0");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Listings");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4ccf173a-88d5-4b1d-9204-419511cc6f69", "f91ae186-d2b1-48a9-a790-970971ed4640", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f38962ca-43fe-4eaa-9436-f04e7b7ea348", "af37404b-25bf-4426-bd4b-296ec647413a", "Trader", "TRADER" });
        }
    }
}
