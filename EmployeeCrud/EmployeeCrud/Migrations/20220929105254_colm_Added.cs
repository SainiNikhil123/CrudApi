using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeCrud.Migrations
{
    public partial class colm_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "UserClaimTable",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "UserClaimTable");
        }
    }
}
