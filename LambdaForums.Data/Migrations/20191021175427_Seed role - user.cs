using Microsoft.EntityFrameworkCore.Migrations;

namespace LambdaForums.Data.Migrations
{
    public partial class Seedroleuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a170527e-15e1-4ead-858f-b5fb3440d894");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fddcc30e-fee0-4586-a4a6-bb9499b2d727", "efb7589c-41a6-41f5-8f5e-2a57025fb029", "User", "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fddcc30e-fee0-4586-a4a6-bb9499b2d727");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a170527e-15e1-4ead-858f-b5fb3440d894", "14d65099-2fcd-46ea-b8dc-b7c8dd7953ea", "User", "user" });
        }
    }
}
