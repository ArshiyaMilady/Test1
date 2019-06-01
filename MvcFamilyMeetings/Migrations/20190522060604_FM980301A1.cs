using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFamilyMeetings.Migrations
{
    public partial class FM980301A1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Saving",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Member_Id = table.Column<int>(nullable: false),
                    Payment = table.Column<long>(nullable: false),
                    Date = table.Column<string>(nullable: false),
                    Remaining = table.Column<long>(nullable: false),
                    Loan_Amount = table.Column<long>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    I_C1 = table.Column<string>(nullable: true),
                    I_C2 = table.Column<string>(nullable: true),
                    I_I1 = table.Column<int>(nullable: false),
                    I_I2 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Saving",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
