using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFamilyMeetings.Migrations
{
    public partial class Initial_MembersMeetings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Member_Id = table.Column<int>(nullable: false),
                    Login_Date = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Mt_C1 = table.Column<string>(nullable: true),
                    Mt_C2 = table.Column<string>(nullable: true),
                    Mt_I1 = table.Column<int>(nullable: false),
                    Mt_I2 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Numbers = table.Column<int>(nullable: false),
                    Address_Home = table.Column<string>(nullable: true),
                    Mobile_No = table.Column<string>(nullable: true),
                    Phone_Home = table.Column<string>(nullable: true),
                    Address_Work = table.Column<string>(nullable: true),
                    Phone_Work = table.Column<string>(nullable: true),
                    E_Mail = table.Column<string>(nullable: true),
                    Login_Password = table.Column<string>(nullable: true),
                    Login_Password_Hint = table.Column<string>(nullable: true),
                    Date_Enter_Meetings = table.Column<string>(nullable: true),
                    Date_Exit_Meetings = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Saving_Cash = table.Column<long>(nullable: false),
                    Last_Loan = table.Column<long>(nullable: false),
                    Last_Loan_Date = table.Column<string>(nullable: true),
                    M_C1 = table.Column<string>(nullable: true),
                    M_C2 = table.Column<string>(nullable: true),
                    M_C3 = table.Column<string>(nullable: true),
                    M_C4 = table.Column<string>(nullable: true),
                    M_I1 = table.Column<int>(nullable: false),
                    M_I2 = table.Column<int>(nullable: false),
                    M_I3 = table.Column<int>(nullable: false),
                    M_I4 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meeting");

            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
