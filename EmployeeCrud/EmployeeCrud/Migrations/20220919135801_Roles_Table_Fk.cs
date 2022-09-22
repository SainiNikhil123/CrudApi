using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeCrud.Migrations
{
    public partial class Roles_Table_Fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "applicationUsers");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "applicationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_applicationUsers_Roles_RoleId",
                table: "applicationUsers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_applicationUsers_RoleId",
                table: "applicationUsers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "applicationUsers");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "applicationUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
