﻿// <auto-generated />
using System;
using KYC_EDDPortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KYC_EDDPortal.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231123162552_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KYC_EDDPortal.Models.AccountRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName");

                    b.Property<string>("AccountNumber");

                    b.Property<string>("AccountProduct");

                    b.Property<string>("AccountType");

                    b.Property<string>("Address");

                    b.Property<string>("AddressVerified");

                    b.Property<string>("AnticipatedVolume");

                    b.Property<bool>("BlackList");

                    b.Property<string>("BranchCode");

                    b.Property<string>("Comment");

                    b.Property<int?>("CorporateId");

                    b.Property<string>("CustomerID");

                    b.Property<string>("CustomerName");

                    b.Property<string>("CustomerType");

                    b.Property<DateTime>("DateOfAddress");

                    b.Property<int?>("IndividualId");

                    b.Property<bool>("PEPList");

                    b.Property<string>("PurposeOfAccount");

                    b.Property<string>("RequestId");

                    b.Property<string>("RequestType");

                    b.Property<bool>("SanctionList");

                    b.Property<string>("Sourceoffunds");

                    b.Property<string>("Typeofactivity");

                    b.HasKey("Id");

                    b.HasIndex("CorporateId");

                    b.HasIndex("IndividualId");

                    b.ToTable("AccountRequests");
                });

            modelBuilder.Entity("KYC_EDDPortal.Models.AdminAuditLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("AuditStatusId");

                    b.Property<string>("BrowserInfo");

                    b.Property<string>("ClientIpAddress");

                    b.Property<string>("Exception");

                    b.Property<DateTime>("ExecutionTime");

                    b.Property<string>("MethodName");

                    b.Property<string>("Parameters");

                    b.Property<string>("ServiceName");

                    b.Property<bool>("Status");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("AdminAuditLogs");
                });

            modelBuilder.Entity("KYC_EDDPortal.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("CommentBy")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("CommentDate");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasColumnType("varchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("RequestID")
                        .IsRequired()
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Status");

                    b.HasKey("ID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("KYC_EDDPortal.Models.CorporateAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NatureOfBusiness");

                    b.Property<string>("RCNo");

                    b.HasKey("Id");

                    b.ToTable("CorporateAccount");
                });

            modelBuilder.Entity("KYC_EDDPortal.Models.IndividualAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Employer");

                    b.Property<DateTime?>("IdentificationExpiryDate");

                    b.Property<string>("IdentificationNumber");

                    b.Property<string>("IdentificationType");

                    b.Property<string>("Nationality");

                    b.Property<string>("Occupation");

                    b.HasKey("Id");

                    b.ToTable("IndividualAccount");
                });

            modelBuilder.Entity("KYC_EDDPortal.Models.ReviewTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorizedByEmail")
                        .HasMaxLength(150);

                    b.Property<string>("AuthorizedByName")
                        .HasMaxLength(100);

                    b.Property<string>("AuthorizedByUsername")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("AuthorizedDate");

                    b.Property<string>("RequestByName")
                        .HasMaxLength(100);

                    b.Property<string>("RequestByUsername")
                        .HasMaxLength(50);

                    b.Property<string>("RequestByemail")
                        .HasMaxLength(150);

                    b.Property<string>("RequestId");

                    b.Property<DateTime?>("ReviewDate");

                    b.Property<string>("ReviewerEmail")
                        .HasMaxLength(150);

                    b.Property<string>("ReviewerName")
                        .HasMaxLength(100);

                    b.Property<string>("ReviewerUsername")
                        .HasMaxLength(50);

                    b.Property<string>("Status");

                    b.Property<string>("ToBeAuthroiziedBy")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ReviewTables");
                });

            modelBuilder.Entity("KYC_EDDPortal.Models.AccountRequest", b =>
                {
                    b.HasOne("KYC_EDDPortal.Models.CorporateAccount", "Corporate")
                        .WithMany()
                        .HasForeignKey("CorporateId");

                    b.HasOne("KYC_EDDPortal.Models.IndividualAccount", "Individual")
                        .WithMany()
                        .HasForeignKey("IndividualId");
                });
#pragma warning restore 612, 618
        }
    }
}