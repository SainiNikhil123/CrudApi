using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeCrud.Migrations
{
    public partial class inverse_Prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeesDepartmentId",
                table: "employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeesEmployeeId",
                table: "employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_EmployeesDepartmentId_EmployeesEmployeeId",
                table: "employees",
                columns: new[] { "EmployeesDepartmentId", "EmployeesEmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_employees_empDepTbls_EmployeesDepartmentId_EmployeesEmployeeId",
                table: "employees",
                columns: new[] { "EmployeesDepartmentId", "EmployeesEmployeeId" },
                principalTable: "empDepTbls",
                principalColumns: new[] { "DepartmentId", "EmployeeId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_empDepTbls_EmployeesDepartmentId_EmployeesEmployeeId",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "IX_employees_EmployeesDepartmentId_EmployeesEmployeeId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "EmployeesDepartmentId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "EmployeesEmployeeId",
                table: "employees");
        }
    }
}
