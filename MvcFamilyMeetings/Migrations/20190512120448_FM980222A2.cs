using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFamilyMeetings.Migrations
{
    public partial class FM980222A2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Last_Loan_Remainig",
                table: "Member",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Last_Loan_Remainig",
                table: "Member");
        }
    }
}
