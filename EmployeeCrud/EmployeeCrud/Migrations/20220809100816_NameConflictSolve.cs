using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeCrud.Migrations
{
    public partial class NameConflictSolve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "employees",
                newName: "EmpName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "designations",
                newName: "DesName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "departments",
                newName: "DepName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmpName",
                table: "employees",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DesName",
                table: "designations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DepName",
                table: "departments",
                newName: "Name");
        }
    }
}
