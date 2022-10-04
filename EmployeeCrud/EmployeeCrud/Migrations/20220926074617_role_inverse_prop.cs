using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeCrud.Migrations
{
    public partial class role_inverse_prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolesssId",
                table: "userRoleTables",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userRoleTables_RolesssId",
                table: "userRoleTables",
                column: "RolesssId");

            migrationBuilder.AddForeignKey(
                name: "FK_userRoleTables_Roles_RolesssId",
                table: "userRoleTables",
                column: "RolesssId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userRoleTables_Roles_RolesssId",
                table: "userRoleTables");

            migrationBuilder.DropIndex(
                name: "IX_userRoleTables_RolesssId",
                table: "userRoleTables");

            migrationBuilder.DropColumn(
                name: "RolesssId",
                table: "userRoleTables");
        }
    }
}
