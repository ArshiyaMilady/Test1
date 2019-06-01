using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFamilyMeetings.Migrations
{
    public partial class FM980308A1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Meeting_Turn",
                table: "Member",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meeting_Turn",
                table: "Member");
        }
    }
}
