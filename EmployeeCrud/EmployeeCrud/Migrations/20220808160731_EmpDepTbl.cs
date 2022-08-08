using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeCrud.Migrations
{
    public partial class EmpDepTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_departments_DepartmentId",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "IX_employees_DepartmentId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "employees");

            migrationBuilder.CreateTable(
                name: "empDepTbls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empDepTbls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_empDepTbls_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_empDepTbls_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_empDepTbls_DepartmentId",
                table: "empDepTbls",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_empDepTbls_EmployeeId",
                table: "empDepTbls",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "empDepTbls");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_employees_DepartmentId",
                table: "employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_departments_DepartmentId",
                table: "employees",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
