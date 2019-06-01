using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFamilyMeetings.Migrations
{
    public partial class FM980230A1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Mobile_No",
                table: "Member",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login_Password",
                table: "Member",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Member",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Member_Name",
                table: "Meeting",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Member_Name",
                table: "Meeting");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile_No",
                table: "Member",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Login_Password",
                table: "Member",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Member",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
