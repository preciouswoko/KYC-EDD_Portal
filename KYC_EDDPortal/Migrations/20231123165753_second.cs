using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KYC_EDDPortal.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRequests_CorporateAccount_CorporateId",
                table: "AccountRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRequests_IndividualAccount_IndividualId",
                table: "AccountRequests");

            migrationBuilder.DropTable(
                name: "CorporateAccount");

            migrationBuilder.DropTable(
                name: "IndividualAccount");

            migrationBuilder.DropIndex(
                name: "IX_AccountRequests_CorporateId",
                table: "AccountRequests");

            migrationBuilder.DropIndex(
                name: "IX_AccountRequests_IndividualId",
                table: "AccountRequests");

            migrationBuilder.DropColumn(
                name: "CorporateId",
                table: "AccountRequests");

            migrationBuilder.DropColumn(
                name: "IndividualId",
                table: "AccountRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AccountRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Employer",
                table: "AccountRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IdentificationExpiryDate",
                table: "AccountRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                table: "AccountRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentificationType",
                table: "AccountRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "AccountRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NatureOfBusiness",
                table: "AccountRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "AccountRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RCNo",
                table: "AccountRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AccountRequests");

            migrationBuilder.DropColumn(
                name: "Employer",
                table: "AccountRequests");

            migrationBuilder.DropColumn(
                name: "IdentificationExpiryDate",
                table: "AccountRequests");

            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                table: "AccountRequests");

            migrationBuilder.DropColumn(
                name: "IdentificationType",
                table: "AccountRequests");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AccountRequests");

            migrationBuilder.DropColumn(
                name: "NatureOfBusiness",
                table: "AccountRequests");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "AccountRequests");

            migrationBuilder.DropColumn(
                name: "RCNo",
                table: "AccountRequests");

            migrationBuilder.AddColumn<int>(
                name: "CorporateId",
                table: "AccountRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IndividualId",
                table: "AccountRequests",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CorporateAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NatureOfBusiness = table.Column<string>(nullable: true),
                    RCNo = table.Column<string>(nullable: true)
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
                    Employer = table.Column<string>(nullable: true),
                    IdentificationExpiryDate = table.Column<DateTime>(nullable: true),
                    IdentificationNumber = table.Column<string>(nullable: true),
                    IdentificationType = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualAccount", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRequests_CorporateId",
                table: "AccountRequests",
                column: "CorporateId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRequests_IndividualId",
                table: "AccountRequests",
                column: "IndividualId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRequests_CorporateAccount_CorporateId",
                table: "AccountRequests",
                column: "CorporateId",
                principalTable: "CorporateAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRequests_IndividualAccount_IndividualId",
                table: "AccountRequests",
                column: "IndividualId",
                principalTable: "IndividualAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
