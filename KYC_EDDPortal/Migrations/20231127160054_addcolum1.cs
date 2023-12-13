using Microsoft.EntityFrameworkCore.Migrations;

namespace KYC_EDDPortal.Migrations
{
    public partial class addcolum1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewStatus",
                table: "ReviewTables",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewStatus",
                table: "ReviewTables");
        }
    }
}
