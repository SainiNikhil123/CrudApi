using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeCrud.Migrations
{
    public partial class InverseProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersRoleId",
                table: "applicationUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "applicationUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_applicationUsers_UsersRoleId_UsersUserId",
                table: "applicationUsers",
                columns: new[] { "UsersRoleId", "UsersUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_applicationUsers_userRoleTables_UsersRoleId_UsersUserId",
                table: "applicationUsers",
                columns: new[] { "UsersRoleId", "UsersUserId" },
                principalTable: "userRoleTables",
                principalColumns: new[] { "RoleId", "UserId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationUsers_userRoleTables_UsersRoleId_UsersUserId",
                table: "applicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_applicationUsers_UsersRoleId_UsersUserId",
                table: "applicationUsers");

            migrationBuilder.DropColumn(
                name: "UsersRoleId",
                table: "applicationUsers");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "applicationUsers");
        }
    }
}
