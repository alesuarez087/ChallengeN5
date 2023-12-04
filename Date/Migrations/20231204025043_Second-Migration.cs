using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_PermissionTypes_IdPermissionType",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_IdPermissionType",
                table: "Permissions");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_IdPermissionType",
                table: "Permissions",
                column: "IdPermissionType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_PermissionTypes_PermissionTypeId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_PermissionTypeId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "PermissionTypeId",
                table: "Permissions");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_IdPermissionType",
                table: "Permissions",
                column: "IdPermissionType");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_PermissionTypes_IdPermissionType",
                table: "Permissions",
                column: "IdPermissionType",
                principalTable: "PermissionTypes",
                principalColumn: "Id");
        }
    }
}
