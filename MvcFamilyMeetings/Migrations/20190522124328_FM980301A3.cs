using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFamilyMeetings.Migrations
{
    public partial class FM980301A3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Loan_Date",
                table: "Loan",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Loan_Date",
                table: "Loan",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
