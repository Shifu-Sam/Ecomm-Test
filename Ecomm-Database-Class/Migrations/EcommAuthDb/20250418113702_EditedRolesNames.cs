using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecomm_Database_Class.Migrations.EcommAuthDb
{
    /// <inheritdoc />
    public partial class EditedRolesNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d60b1b08-6b2b-4601-a20c-81db0320f1fa",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da72a912-b9e3-4336-8cb8-ca2d4d3cad54",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Customer", "CUSTOMER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d60b1b08-6b2b-4601-a20c-81db0320f1fa",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "adminUser", "ADMINUSER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da72a912-b9e3-4336-8cb8-ca2d4d3cad54",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "customerUser", "CUSTOMERUSER" });
        }
    }
}
