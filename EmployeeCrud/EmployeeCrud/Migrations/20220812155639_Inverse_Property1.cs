using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeCrud.Migrations
{
    public partial class Inverse_Property1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_empDepTbls_EmpDepTblDepartmentId_EmpDepTblEmployeeId",
                table: "employees");

            migrationBuilder.RenameColumn(
                name: "EmpDepTblEmployeeId",
                table: "employees",
                newName: "EmployeesEmployeeId");

            migrationBuilder.RenameColumn(
                name: "EmpDepTblDepartmentId",
                table: "employees",
                newName: "EmployeesDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_employees_EmpDepTblDepartmentId_EmpDepTblEmployeeId",
                table: "employees",
                newName: "IX_employees_EmployeesDepartmentId_EmployeesEmployeeId");

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

            migrationBuilder.RenameColumn(
                name: "EmployeesEmployeeId",
                table: "employees",
                newName: "EmpDepTblEmployeeId");

            migrationBuilder.RenameColumn(
                name: "EmployeesDepartmentId",
                table: "employees",
                newName: "EmpDepTblDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_employees_EmployeesDepartmentId_EmployeesEmployeeId",
                table: "employees",
                newName: "IX_employees_EmpDepTblDepartmentId_EmpDepTblEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_empDepTbls_EmpDepTblDepartmentId_EmpDepTblEmployeeId",
                table: "employees",
                columns: new[] { "EmpDepTblDepartmentId", "EmpDepTblEmployeeId" },
                principalTable: "empDepTbls",
                principalColumns: new[] { "DepartmentId", "EmployeeId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
