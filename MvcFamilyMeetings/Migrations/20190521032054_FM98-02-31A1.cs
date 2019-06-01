using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFamilyMeetings.Migrations
{
    public partial class FM980231A1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Saving",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Member_Id = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: true),
                    Payment = table.Column<long>(nullable: false),
                    S_C1 = table.Column<string>(nullable: true),
                    S_C2 = table.Column<string>(nullable: true),
                    S_I1 = table.Column<int>(nullable: false),
                    S_I2 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saving", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Saving");
        }
    }
}
