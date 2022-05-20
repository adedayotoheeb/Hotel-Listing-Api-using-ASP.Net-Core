using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListing.Migrations
{
    public partial class correctedTheAdminROle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55cd703b-05b8-4233-9e7b-3d88bc12ed4b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7f8cc4c-f640-4068-afb3-742f804be0d7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0bdebea3-230a-46eb-a26d-07329b9d2351", "d9ee72e4-acbb-4afe-844a-f5ca2637d3cf", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85de8376-f972-4fe5-b9ee-b47032c4782b", "9d9f396e-2551-4ee4-a87b-59d67d14dcb4", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bdebea3-230a-46eb-a26d-07329b9d2351");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85de8376-f972-4fe5-b9ee-b47032c4782b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "55cd703b-05b8-4233-9e7b-3d88bc12ed4b", "f5157e30-f2ad-4ed9-aaa0-caf21bdeaa8b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b7f8cc4c-f640-4068-afb3-742f804be0d7", "47c79e14-c3d4-4ad8-847a-cfdca300c05a", "ADMINISTRATOR", "ADMINISTRATOR" });
        }
    }
}
