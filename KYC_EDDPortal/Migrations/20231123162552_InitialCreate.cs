using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KYC_EDDPortal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminAuditLogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    ServiceName = table.Column<string>(nullable: true),
                    MethodName = table.Column<string>(nullable: true),
                    Parameters = table.Column<string>(nullable: true),
                    ExecutionTime = table.Column<DateTime>(nullable: false),
                    ClientIpAddress = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true),
                    BrowserInfo = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    AuditStatusId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminAuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestID = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Action = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    CommentDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    CommentBy = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CorporateAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RCNo = table.Column<string>(nullable: true),
                    NatureOfBusiness = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndividualAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    Employer = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    IdentificationType = table.Column<string>(nullable: true),
                    IdentificationNumber = table.Column<string>(nullable: true),
                    IdentificationExpiryDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestByUsername = table.Column<string>(maxLength: 50, nullable: true),
                    RequestByName = table.Column<string>(maxLength: 100, nullable: true),
                    RequestByemail = table.Column<string>(maxLength: 150, nullable: true),
                    ToBeAuthroiziedBy = table.Column<string>(maxLength: 50, nullable: true),
                    AuthorizedDate = table.Column<DateTime>(nullable: true),
                    AuthorizedByUsername = table.Column<string>(maxLength: 50, nullable: true),
                    AuthorizedByName = table.Column<string>(maxLength: 100, nullable: true),
                    AuthorizedByEmail = table.Column<string>(maxLength: 150, nullable: true),
                    RequestId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ReviewerUsername = table.Column<string>(maxLength: 50, nullable: true),
                    ReviewerName = table.Column<string>(maxLength: 100, nullable: true),
                    ReviewerEmail = table.Column<string>(maxLength: 150, nullable: true),
                    ReviewDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestType = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    CustomerID = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    AccountType = table.Column<string>(nullable: true),
                    AccountProduct = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    BranchCode = table.Column<string>(nullable: true),
                    CustomerType = table.Column<string>(nullable: true),
                    IndividualId = table.Column<int>(nullable: true),
                    CorporateId = table.Column<int>(nullable: true),
                    SanctionList = table.Column<bool>(nullable: false),
                    PEPList = table.Column<bool>(nullable: false),
                    BlackList = table.Column<bool>(nullable: false),
                    PurposeOfAccount = table.Column<string>(nullable: true),
                    Sourceoffunds = table.Column<string>(nullable: true),
                    AddressVerified = table.Column<string>(nullable: true),
                    DateOfAddress = table.Column<DateTime>(nullable: false),
                    AnticipatedVolume = table.Column<string>(nullable: true),
                    Typeofactivity = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    RequestId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountRequests_CorporateAccount_CorporateId",
                        column: x => x.CorporateId,
                        principalTable: "CorporateAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountRequests_IndividualAccount_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "IndividualAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRequests_CorporateId",
                table: "AccountRequests",
                column: "CorporateId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRequests_IndividualId",
                table: "AccountRequests",
                column: "IndividualId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRequests");

            migrationBuilder.DropTable(
                name: "AdminAuditLogs");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ReviewTables");

            migrationBuilder.DropTable(
                name: "CorporateAccount");

            migrationBuilder.DropTable(
                name: "IndividualAccount");
        }
    }
}
