using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeCrud.Migrations
{
    public partial class UserRoleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationUsers_Roles_RoleId",
                table: "applicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_applicationUsers_RoleId",
                table: "applicationUsers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "applicationUsers");

            migrationBuilder.CreateTable(
                name: "userRoleTables",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRoleTables", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_userRoleTables_applicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "applicationUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userRoleTables_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userRoleTables_UserId",
                table: "userRoleTables",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userRoleTables");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "applicationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_applicationUsers_RoleId",
                table: "applicationUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_applicationUsers_Roles_RoleId",
                table: "applicationUsers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
