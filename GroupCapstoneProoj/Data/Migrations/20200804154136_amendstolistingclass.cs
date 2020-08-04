using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupCapstoneProoj.Data.Migrations
{
    public partial class amendstolistingclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08a191b0-0220-41d2-8a54-6d9c240bfc86");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4bde32ba-fd29-42ca-a9e1-aa8bac62dfcd");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Traders",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUserId = table.Column<string>(nullable: true),
                    ListingName = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    InReturn = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    StreetName = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(maxLength: 5, nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listings_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4ccf173a-88d5-4b1d-9204-419511cc6f69", "f91ae186-d2b1-48a9-a790-970971ed4640", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f38962ca-43fe-4eaa-9436-f04e7b7ea348", "af37404b-25bf-4426-bd4b-296ec647413a", "Trader", "TRADER" });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_IdentityUserId",
                table: "Listings",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ccf173a-88d5-4b1d-9204-419511cc6f69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f38962ca-43fe-4eaa-9436-f04e7b7ea348");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Traders",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "08a191b0-0220-41d2-8a54-6d9c240bfc86", "43e05f82-0051-44f9-b594-4c282c8bcfb7", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4bde32ba-fd29-42ca-a9e1-aa8bac62dfcd", "b3025057-e247-479e-93e5-98d4b5f714ec", "Trader", "TRADER" });
        }
    }
}
