using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Argent.Api.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Customer_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mfi");

            migrationBuilder.CreateTable(
                name: "audit_logs",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BranchId = table.Column<long>(type: "bigint", nullable: true),
                    BranchName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    IpAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Module = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    EntityName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EntityId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ActionType = table.Column<int>(type: "integer", nullable: false),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    Succeeded = table.Column<bool>(type: "boolean", nullable: false),
                    FailureReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    OccurredOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DurationMs = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_customer_filters",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Scope = table.Column<int>(type: "integer", nullable: false),
                    SlotNumber = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_customer_filters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_education_levels",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_education_levels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_file_attachments",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Series = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FileType = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_file_attachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_general_reasons",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_general_reasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_group_positions",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Series = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Designation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_group_positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_identification_types",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    Sufficient = table.Column<bool>(type: "boolean", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    LocalFolder = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    FtpFolder = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_identification_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_income_types",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_income_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_issuer_authorities",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_issuer_authorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_nationalities",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_professions",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_reject_reasons",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_reject_reasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_titles",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_titles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_villages",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Parish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SubCounty = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    County = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_villages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegisteredName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BusinessLine = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ContactEmail = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Module = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role_groups",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    IsSystemRole = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_configs",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Module = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DataType = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_configs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_policies",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Module = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DefaultValue = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DataType = table.Column<int>(type: "integer", nullable: false),
                    IsOverridable = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_policies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kyc_customer_exits",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    ExitedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReasonId = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_customer_exits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_customer_exits_kyc_general_reasons_ReasonId",
                        column: x => x.ReasonId,
                        principalSchema: "mfi",
                        principalTable: "kyc_general_reasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kyc_guarantors",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    IsClient = table.Column<bool>(type: "boolean", nullable: false),
                    Photo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Signature = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PermanentAddress = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Telephone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Mobile = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Town = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    TitleId = table.Column<long>(type: "bigint", nullable: true),
                    NationalityId = table.Column<long>(type: "bigint", nullable: true),
                    VillageId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_guarantors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_guarantors_kyc_nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalSchema: "mfi",
                        principalTable: "kyc_nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_kyc_guarantors_kyc_professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalSchema: "mfi",
                        principalTable: "kyc_professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_kyc_guarantors_kyc_titles_TitleId",
                        column: x => x.TitleId,
                        principalSchema: "mfi",
                        principalTable: "kyc_titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_kyc_guarantors_kyc_villages_VillageId",
                        column: x => x.VillageId,
                        principalSchema: "mfi",
                        principalTable: "kyc_villages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "branches",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    BranchCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    BranchName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PostalAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_branches_organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "mfi",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "role_group_members",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleGroupId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_group_members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_group_members_role_groups_RoleGroupId",
                        column: x => x.RoleGroupId,
                        principalSchema: "mfi",
                        principalTable: "role_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_group_members_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "mfi",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_permissions_permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "mfi",
                        principalTable: "permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_role_permissions_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "mfi",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_group_policy_overrides",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleGroupId = table.Column<long>(type: "bigint", nullable: false),
                    SystemPolicyId = table.Column<long>(type: "bigint", nullable: false),
                    OverrideValue = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Reason = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_group_policy_overrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_group_policy_overrides_role_groups_RoleGroupId",
                        column: x => x.RoleGroupId,
                        principalSchema: "mfi",
                        principalTable: "role_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_group_policy_overrides_system_policies_SystemPolicyId",
                        column: x => x.SystemPolicyId,
                        principalSchema: "mfi",
                        principalTable: "system_policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "branch_holidays",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    HolidayDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Recurrence = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branch_holidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_branch_holidays_branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customers_base",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Statistic = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    PermanentAddress = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    MailAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PrimaryLine = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    SecondaryLine = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Mobile = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Fax = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Town = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    WhatsApp = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Facebook = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Instagram = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Twitter = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ClientType = table.Column<int>(type: "integer", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Approved = table.Column<bool>(type: "boolean", nullable: false),
                    ApprovedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Exited = table.Column<bool>(type: "boolean", nullable: false),
                    ExitedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CanTransact = table.Column<bool>(type: "boolean", nullable: false),
                    HoldShares = table.Column<bool>(type: "boolean", nullable: false),
                    Filter1Id = table.Column<long>(type: "bigint", nullable: true),
                    Filter2Id = table.Column<long>(type: "bigint", nullable: true),
                    Filter3Id = table.Column<long>(type: "bigint", nullable: true),
                    VillageId = table.Column<long>(type: "bigint", nullable: true),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ExitId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers_base", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customers_base_branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_customers_base_kyc_customer_exits_ExitId",
                        column: x => x.ExitId,
                        principalSchema: "mfi",
                        principalTable: "kyc_customer_exits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_customers_base_kyc_customer_filters_Filter1Id",
                        column: x => x.Filter1Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_customer_filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_base_kyc_customer_filters_Filter2Id",
                        column: x => x.Filter2Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_customer_filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_base_kyc_customer_filters_Filter3Id",
                        column: x => x.Filter3Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_customer_filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_base_kyc_villages_VillageId",
                        column: x => x.VillageId,
                        principalSchema: "mfi",
                        principalTable: "kyc_villages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    PasswordHash = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    LastLoginOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FailedLoginAttempts = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    LockedUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DefaultBranchId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_branches_DefaultBranchId",
                        column: x => x.DefaultBranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customers_businesses",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LegalName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BusinessFilter1Id = table.Column<long>(type: "bigint", nullable: true),
                    BusinessFilter2Id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers_businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customers_businesses_customers_base_Id",
                        column: x => x.Id,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customers_businesses_kyc_customer_filters_BusinessFilter1Id",
                        column: x => x.BusinessFilter1Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_customer_filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_businesses_kyc_customer_filters_BusinessFilter2Id",
                        column: x => x.BusinessFilter2Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_customer_filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "customers_groups",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RegisteredName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    GroupFilter1Id = table.Column<long>(type: "bigint", nullable: true),
                    GroupFilter2Id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers_groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customers_groups_customers_base_Id",
                        column: x => x.Id,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customers_groups_kyc_customer_filters_GroupFilter1Id",
                        column: x => x.GroupFilter1Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_customer_filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_groups_kyc_customer_filters_GroupFilter2Id",
                        column: x => x.GroupFilter2Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_customer_filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "customers_individuals",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BirthPlace = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    MaritalStatus = table.Column<int>(type: "integer", nullable: true),
                    SpouseName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Children = table.Column<int>(type: "integer", nullable: false),
                    Dependents = table.Column<int>(type: "integer", nullable: false),
                    Mother = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Father = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Literate = table.Column<bool>(type: "boolean", nullable: false),
                    Photo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Signature = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RightThumbPrint = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LeftThumbPrint = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TitleId = table.Column<long>(type: "bigint", nullable: true),
                    NationalityId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionId = table.Column<long>(type: "bigint", nullable: true),
                    EducationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers_individuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customers_individuals_customers_base_Id",
                        column: x => x.Id,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customers_individuals_kyc_education_levels_EducationId",
                        column: x => x.EducationId,
                        principalSchema: "mfi",
                        principalTable: "kyc_education_levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_individuals_kyc_nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalSchema: "mfi",
                        principalTable: "kyc_nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_individuals_kyc_professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalSchema: "mfi",
                        principalTable: "kyc_professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_individuals_kyc_titles_TitleId",
                        column: x => x.TitleId,
                        principalSchema: "mfi",
                        principalTable: "kyc_titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "kyc_customer_agreements",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Document = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CustomerBaseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_customer_agreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_customer_agreements_customers_base_CustomerBaseId",
                        column: x => x.CustomerBaseId,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_customer_agreements_kyc_file_attachments_Id",
                        column: x => x.Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_file_attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kyc_customer_approvals",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ActionedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActionedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Comments = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CustomerBaseId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_customer_approvals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_customer_approvals_customers_base_CustomerBaseId",
                        column: x => x.CustomerBaseId,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "kyc_customer_blacklists",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    ListedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UnListedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReasonId = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CustomerBaseId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_customer_blacklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_customer_blacklists_customers_base_CustomerBaseId",
                        column: x => x.CustomerBaseId,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_customer_blacklists_kyc_general_reasons_ReasonId",
                        column: x => x.ReasonId,
                        principalSchema: "mfi",
                        principalTable: "kyc_general_reasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kyc_customer_contacts",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    Series = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ContactName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Telephone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Mobile = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Relationship = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CustomerBaseId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_customer_contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_customer_contacts_customers_base_CustomerBaseId",
                        column: x => x.CustomerBaseId,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "kyc_customer_contracts",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CustomerBaseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_customer_contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_customer_contracts_customers_base_CustomerBaseId",
                        column: x => x.CustomerBaseId,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_customer_contracts_kyc_file_attachments_Id",
                        column: x => x.Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_file_attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kyc_rejected_customers",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    RejectDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RejectedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ReasonId = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CustomerBaseId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_rejected_customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_rejected_customers_customers_base_CustomerBaseId",
                        column: x => x.CustomerBaseId,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_rejected_customers_kyc_reject_reasons_ReasonId",
                        column: x => x.ReasonId,
                        principalSchema: "mfi",
                        principalTable: "kyc_reject_reasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kyc_title_deeds",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PlotNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Block = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CustomerBaseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_title_deeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_title_deeds_customers_base_CustomerBaseId",
                        column: x => x.CustomerBaseId,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_title_deeds_kyc_file_attachments_Id",
                        column: x => x.Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_file_attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kyc_unlocked_customers",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    UnlockedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UnlockedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ReasonId = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CustomerBaseId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_unlocked_customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_unlocked_customers_customers_base_CustomerBaseId",
                        column: x => x.CustomerBaseId,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_unlocked_customers_kyc_general_reasons_ReasonId",
                        column: x => x.ReasonId,
                        principalSchema: "mfi",
                        principalTable: "kyc_general_reasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "password_history",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ChangedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_password_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_password_history_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "mfi",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    ReplacedByToken = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    CreatedByIp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "mfi",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_branch_access",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    CanPost = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_branch_access", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_branch_access_branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_branch_access_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "mfi",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_roles_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "mfi",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_roles_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "mfi",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kyc_signatories",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BusinessId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Photo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Signature = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Telephone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Mobile = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Position = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CanSignAlone = table.Column<bool>(type: "boolean", nullable: false),
                    Suspended = table.Column<bool>(type: "boolean", nullable: false),
                    PassCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_signatories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_signatories_customers_businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalSchema: "mfi",
                        principalTable: "customers_businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customers_members",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    MemberNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BirthPlace = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    MaritalStatus = table.Column<int>(type: "integer", nullable: true),
                    SpouseName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Children = table.Column<int>(type: "integer", nullable: false),
                    Dependents = table.Column<int>(type: "integer", nullable: false),
                    Mother = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Father = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Literate = table.Column<bool>(type: "boolean", nullable: false),
                    Photo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Signature = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RightThumbPrint = table.Column<string>(type: "text", nullable: true),
                    LeftThumbPrint = table.Column<string>(type: "text", nullable: true),
                    JoinedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TitleId = table.Column<long>(type: "bigint", nullable: true),
                    NationalityId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionId = table.Column<long>(type: "bigint", nullable: true),
                    EducationId = table.Column<long>(type: "bigint", nullable: true),
                    MemberFilter1Id = table.Column<long>(type: "bigint", nullable: true),
                    MemberFilter2Id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers_members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customers_members_customers_base_Id",
                        column: x => x.Id,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customers_members_customers_groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "mfi",
                        principalTable: "customers_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_customers_members_kyc_customer_filters_MemberFilter1Id",
                        column: x => x.MemberFilter1Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_customer_filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_members_kyc_customer_filters_MemberFilter2Id",
                        column: x => x.MemberFilter2Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_customer_filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_members_kyc_education_levels_EducationId",
                        column: x => x.EducationId,
                        principalSchema: "mfi",
                        principalTable: "kyc_education_levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_members_kyc_nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalSchema: "mfi",
                        principalTable: "kyc_nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_members_kyc_professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalSchema: "mfi",
                        principalTable: "kyc_professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_customers_members_kyc_titles_TitleId",
                        column: x => x.TitleId,
                        principalSchema: "mfi",
                        principalTable: "kyc_titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "kyc_clusters",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Series = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ClusterName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    AddedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClosedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Area = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreditOfficer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Merged = table.Column<bool>(type: "boolean", nullable: false),
                    MergedToCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_clusters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_clusters_customers_groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "mfi",
                        principalTable: "customers_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kyc_meetings",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    MeetingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Venue = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Agenda = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Minutes = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_meetings_customers_groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "mfi",
                        principalTable: "customers_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kyc_other_files",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SignatoryId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerBaseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_other_files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_other_files_customers_base_CustomerBaseId",
                        column: x => x.CustomerBaseId,
                        principalSchema: "mfi",
                        principalTable: "customers_base",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_other_files_kyc_file_attachments_Id",
                        column: x => x.Id,
                        principalSchema: "mfi",
                        principalTable: "kyc_file_attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kyc_other_files_kyc_signatories_SignatoryId",
                        column: x => x.SignatoryId,
                        principalSchema: "mfi",
                        principalTable: "kyc_signatories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "kyc_employment_histories",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    Employer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PositionHeld = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsCurrentEmployer = table.Column<bool>(type: "boolean", nullable: false),
                    Earning = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IndividualId = table.Column<long>(type: "bigint", nullable: true),
                    MemberId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_employment_histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_employment_histories_customers_individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalSchema: "mfi",
                        principalTable: "customers_individuals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_employment_histories_customers_members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "mfi",
                        principalTable: "customers_members",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "kyc_income_histories",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    Employer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PositionHeld = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    HiredOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Salary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IsCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    Ended = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IncomeTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IndividualId = table.Column<long>(type: "bigint", nullable: true),
                    MemberId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_income_histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_income_histories_customers_individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalSchema: "mfi",
                        principalTable: "customers_individuals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_income_histories_customers_members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "mfi",
                        principalTable: "customers_members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_income_histories_kyc_income_types_IncomeTypeId",
                        column: x => x.IncomeTypeId,
                        principalSchema: "mfi",
                        principalTable: "kyc_income_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kyc_member_positions",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    PositionId = table.Column<long>(type: "bigint", nullable: false),
                    Started = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ended = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_member_positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_member_positions_customers_members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "mfi",
                        principalTable: "customers_members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kyc_member_positions_kyc_group_positions_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "mfi",
                        principalTable: "kyc_group_positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kyc_member_transfers",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    IsClusterTransfer = table.Column<bool>(type: "boolean", nullable: false),
                    FromGroupCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FromMemberCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ToGroupCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ToMemberCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TransferDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Approved = table.Column<bool>(type: "boolean", nullable: false),
                    ReasonId = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_member_transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_member_transfers_customers_members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "mfi",
                        principalTable: "customers_members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_kyc_member_transfers_kyc_general_reasons_ReasonId",
                        column: x => x.ReasonId,
                        principalSchema: "mfi",
                        principalTable: "kyc_general_reasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kyc_saving_partners",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IndividualId = table.Column<long>(type: "bigint", nullable: true),
                    MemberId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_saving_partners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_saving_partners_customers_individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalSchema: "mfi",
                        principalTable: "customers_individuals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_saving_partners_customers_members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "mfi",
                        principalTable: "customers_members",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "kyc_cluster_members",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClusterId = table.Column<long>(type: "bigint", nullable: false),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    JoinedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExitedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_cluster_members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_cluster_members_customers_members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "mfi",
                        principalTable: "customers_members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_kyc_cluster_members_kyc_clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalSchema: "mfi",
                        principalTable: "kyc_clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kyc_identifications",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IssuedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IdentityTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IssuerAuthorityId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerType = table.Column<int>(type: "integer", nullable: true),
                    SignatoryId = table.Column<long>(type: "bigint", nullable: true),
                    GuarantorId = table.Column<long>(type: "bigint", nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IndividualId = table.Column<long>(type: "bigint", nullable: true),
                    MemberId = table.Column<long>(type: "bigint", nullable: true),
                    SavingPartnerId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_identifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_identifications_customers_individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalSchema: "mfi",
                        principalTable: "customers_individuals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_identifications_customers_members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "mfi",
                        principalTable: "customers_members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_identifications_kyc_guarantors_GuarantorId",
                        column: x => x.GuarantorId,
                        principalSchema: "mfi",
                        principalTable: "kyc_guarantors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_kyc_identifications_kyc_identification_types_IdentityTypeId",
                        column: x => x.IdentityTypeId,
                        principalSchema: "mfi",
                        principalTable: "kyc_identification_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_kyc_identifications_kyc_issuer_authorities_IssuerAuthorityId",
                        column: x => x.IssuerAuthorityId,
                        principalSchema: "mfi",
                        principalTable: "kyc_issuer_authorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_kyc_identifications_kyc_saving_partners_SavingPartnerId",
                        column: x => x.SavingPartnerId,
                        principalSchema: "mfi",
                        principalTable: "kyc_saving_partners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_identifications_kyc_signatories_SignatoryId",
                        column: x => x.SignatoryId,
                        principalSchema: "mfi",
                        principalTable: "kyc_signatories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "kyc_image_files",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Notes = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    OtherFileId = table.Column<long>(type: "bigint", nullable: true),
                    TitleDeedId = table.Column<long>(type: "bigint", nullable: true),
                    IdentificationId = table.Column<long>(type: "bigint", nullable: true),
                    SavingPartnerId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kyc_image_files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kyc_image_files_kyc_identifications_IdentificationId",
                        column: x => x.IdentificationId,
                        principalSchema: "mfi",
                        principalTable: "kyc_identifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kyc_image_files_kyc_other_files_OtherFileId",
                        column: x => x.OtherFileId,
                        principalSchema: "mfi",
                        principalTable: "kyc_other_files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kyc_image_files_kyc_saving_partners_SavingPartnerId",
                        column: x => x.SavingPartnerId,
                        principalSchema: "mfi",
                        principalTable: "kyc_saving_partners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_kyc_image_files_kyc_title_deeds_TitleDeedId",
                        column: x => x.TitleDeedId,
                        principalSchema: "mfi",
                        principalTable: "kyc_title_deeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_branch",
                schema: "mfi",
                table: "audit_logs",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_entity",
                schema: "mfi",
                table: "audit_logs",
                columns: new[] { "EntityName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_module_action",
                schema: "mfi",
                table: "audit_logs",
                columns: new[] { "Module", "Action" });

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_occurred_at",
                schema: "mfi",
                table: "audit_logs",
                column: "OccurredOn");

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_user",
                schema: "mfi",
                table: "audit_logs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "ix_branch_holidays_date",
                schema: "mfi",
                table: "branch_holidays",
                column: "HolidayDate");

            migrationBuilder.CreateIndex(
                name: "ux_branch_holidays_branch_date",
                schema: "mfi",
                table: "branch_holidays",
                columns: new[] { "BranchId", "HolidayDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_branches_organization_id",
                schema: "mfi",
                table: "branches",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "ux_branches_org_default",
                schema: "mfi",
                table: "branches",
                columns: new[] { "OrganizationId", "IsDefault" },
                unique: true,
                filter: "\"IsDefault\" = true AND \"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_branches_sol_id",
                schema: "mfi",
                table: "branches",
                column: "BranchCode",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ix_customers_active",
                schema: "mfi",
                table: "customers_base",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_customers_base_ExitId",
                schema: "mfi",
                table: "customers_base",
                column: "ExitId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_base_Filter1Id",
                schema: "mfi",
                table: "customers_base",
                column: "Filter1Id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_base_Filter2Id",
                schema: "mfi",
                table: "customers_base",
                column: "Filter2Id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_base_Filter3Id",
                schema: "mfi",
                table: "customers_base",
                column: "Filter3Id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_base_VillageId",
                schema: "mfi",
                table: "customers_base",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "ix_customers_branch_id",
                schema: "mfi",
                table: "customers_base",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "ix_customers_client_code",
                schema: "mfi",
                table: "customers_base",
                column: "ClientCode");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_name",
                schema: "mfi",
                table: "customers_businesses",
                column: "LegalName");

            migrationBuilder.CreateIndex(
                name: "IX_customers_businesses_BusinessFilter1Id",
                schema: "mfi",
                table: "customers_businesses",
                column: "BusinessFilter1Id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_businesses_BusinessFilter2Id",
                schema: "mfi",
                table: "customers_businesses",
                column: "BusinessFilter2Id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_groups_GroupFilter1Id",
                schema: "mfi",
                table: "customers_groups",
                column: "GroupFilter1Id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_groups_GroupFilter2Id",
                schema: "mfi",
                table: "customers_groups",
                column: "GroupFilter2Id");

            migrationBuilder.CreateIndex(
                name: "ix_groups_name",
                schema: "mfi",
                table: "customers_groups",
                column: "RegisteredName");

            migrationBuilder.CreateIndex(
                name: "IX_customers_individuals_EducationId",
                schema: "mfi",
                table: "customers_individuals",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_individuals_NationalityId",
                schema: "mfi",
                table: "customers_individuals",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_individuals_ProfessionId",
                schema: "mfi",
                table: "customers_individuals",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_individuals_TitleId",
                schema: "mfi",
                table: "customers_individuals",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "ix_individuals_name",
                schema: "mfi",
                table: "customers_individuals",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_customers_members_EducationId",
                schema: "mfi",
                table: "customers_members",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_members_MemberFilter1Id",
                schema: "mfi",
                table: "customers_members",
                column: "MemberFilter1Id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_members_MemberFilter2Id",
                schema: "mfi",
                table: "customers_members",
                column: "MemberFilter2Id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_members_NationalityId",
                schema: "mfi",
                table: "customers_members",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_members_ProfessionId",
                schema: "mfi",
                table: "customers_members",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_members_TitleId",
                schema: "mfi",
                table: "customers_members",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "ix_members_group_id",
                schema: "mfi",
                table: "customers_members",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "ix_members_name",
                schema: "mfi",
                table: "customers_members",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_kyc_cluster_members_MemberId",
                schema: "mfi",
                table: "kyc_cluster_members",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "ux_cluster_members_cluster_member",
                schema: "mfi",
                table: "kyc_cluster_members",
                columns: new[] { "ClusterId", "MemberId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ix_clusters_group_id",
                schema: "mfi",
                table: "kyc_clusters",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_customer_agreements_CustomerBaseId",
                schema: "mfi",
                table: "kyc_customer_agreements",
                column: "CustomerBaseId");

            migrationBuilder.CreateIndex(
                name: "ix_customer_approvals_customer",
                schema: "mfi",
                table: "kyc_customer_approvals",
                columns: new[] { "CustomerId", "CustomerType" });

            migrationBuilder.CreateIndex(
                name: "IX_kyc_customer_approvals_CustomerBaseId",
                schema: "mfi",
                table: "kyc_customer_approvals",
                column: "CustomerBaseId");

            migrationBuilder.CreateIndex(
                name: "ix_customer_blacklists_customer",
                schema: "mfi",
                table: "kyc_customer_blacklists",
                columns: new[] { "CustomerId", "CustomerType" });

            migrationBuilder.CreateIndex(
                name: "IX_kyc_customer_blacklists_CustomerBaseId",
                schema: "mfi",
                table: "kyc_customer_blacklists",
                column: "CustomerBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_customer_blacklists_ReasonId",
                schema: "mfi",
                table: "kyc_customer_blacklists",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "ix_customer_contacts_customer",
                schema: "mfi",
                table: "kyc_customer_contacts",
                columns: new[] { "CustomerId", "CustomerType" });

            migrationBuilder.CreateIndex(
                name: "IX_kyc_customer_contacts_CustomerBaseId",
                schema: "mfi",
                table: "kyc_customer_contacts",
                column: "CustomerBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_customer_contracts_CustomerBaseId",
                schema: "mfi",
                table: "kyc_customer_contracts",
                column: "CustomerBaseId");

            migrationBuilder.CreateIndex(
                name: "ix_customer_exits_customer",
                schema: "mfi",
                table: "kyc_customer_exits",
                columns: new[] { "CustomerId", "CustomerType" });

            migrationBuilder.CreateIndex(
                name: "IX_kyc_customer_exits_ReasonId",
                schema: "mfi",
                table: "kyc_customer_exits",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "ux_customer_filters_scope_slot_code",
                schema: "mfi",
                table: "kyc_customer_filters",
                columns: new[] { "Scope", "SlotNumber", "Code" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_education_code",
                schema: "mfi",
                table: "kyc_education_levels",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ix_employment_histories_customer",
                schema: "mfi",
                table: "kyc_employment_histories",
                columns: new[] { "CustomerId", "CustomerType" });

            migrationBuilder.CreateIndex(
                name: "IX_kyc_employment_histories_IndividualId",
                schema: "mfi",
                table: "kyc_employment_histories",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_employment_histories_MemberId",
                schema: "mfi",
                table: "kyc_employment_histories",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "ix_file_attachments_customer",
                schema: "mfi",
                table: "kyc_file_attachments",
                columns: new[] { "CustomerId", "CustomerType" });

            migrationBuilder.CreateIndex(
                name: "ux_general_reasons_category_code",
                schema: "mfi",
                table: "kyc_general_reasons",
                columns: new[] { "Category", "Code" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_guarantors_NationalityId",
                schema: "mfi",
                table: "kyc_guarantors",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_guarantors_ProfessionId",
                schema: "mfi",
                table: "kyc_guarantors",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_guarantors_TitleId",
                schema: "mfi",
                table: "kyc_guarantors",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_guarantors_VillageId",
                schema: "mfi",
                table: "kyc_guarantors",
                column: "VillageId");

            migrationBuilder.CreateIndex(
                name: "ix_identifications_customer",
                schema: "mfi",
                table: "kyc_identifications",
                columns: new[] { "CustomerId", "CustomerType" });

            migrationBuilder.CreateIndex(
                name: "IX_kyc_identifications_GuarantorId",
                schema: "mfi",
                table: "kyc_identifications",
                column: "GuarantorId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_identifications_IdentityTypeId",
                schema: "mfi",
                table: "kyc_identifications",
                column: "IdentityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_identifications_IndividualId",
                schema: "mfi",
                table: "kyc_identifications",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_identifications_IssuerAuthorityId",
                schema: "mfi",
                table: "kyc_identifications",
                column: "IssuerAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_identifications_MemberId",
                schema: "mfi",
                table: "kyc_identifications",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_identifications_SavingPartnerId",
                schema: "mfi",
                table: "kyc_identifications",
                column: "SavingPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_identifications_SignatoryId",
                schema: "mfi",
                table: "kyc_identifications",
                column: "SignatoryId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_image_files_IdentificationId",
                schema: "mfi",
                table: "kyc_image_files",
                column: "IdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_image_files_OtherFileId",
                schema: "mfi",
                table: "kyc_image_files",
                column: "OtherFileId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_image_files_SavingPartnerId",
                schema: "mfi",
                table: "kyc_image_files",
                column: "SavingPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_image_files_TitleDeedId",
                schema: "mfi",
                table: "kyc_image_files",
                column: "TitleDeedId");

            migrationBuilder.CreateIndex(
                name: "ix_income_histories_customer",
                schema: "mfi",
                table: "kyc_income_histories",
                columns: new[] { "CustomerId", "CustomerType" });

            migrationBuilder.CreateIndex(
                name: "IX_kyc_income_histories_IncomeTypeId",
                schema: "mfi",
                table: "kyc_income_histories",
                column: "IncomeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_income_histories_IndividualId",
                schema: "mfi",
                table: "kyc_income_histories",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_income_histories_MemberId",
                schema: "mfi",
                table: "kyc_income_histories",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "ux_income_types_code",
                schema: "mfi",
                table: "kyc_income_types",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_meetings_GroupId",
                schema: "mfi",
                table: "kyc_meetings",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_member_positions_MemberId",
                schema: "mfi",
                table: "kyc_member_positions",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_member_positions_PositionId",
                schema: "mfi",
                table: "kyc_member_positions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_member_transfers_MemberId",
                schema: "mfi",
                table: "kyc_member_transfers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_member_transfers_ReasonId",
                schema: "mfi",
                table: "kyc_member_transfers",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "ux_nationalities_code",
                schema: "mfi",
                table: "kyc_nationalities",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_other_files_CustomerBaseId",
                schema: "mfi",
                table: "kyc_other_files",
                column: "CustomerBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_other_files_SignatoryId",
                schema: "mfi",
                table: "kyc_other_files",
                column: "SignatoryId");

            migrationBuilder.CreateIndex(
                name: "ux_professions_code",
                schema: "mfi",
                table: "kyc_professions",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_reject_reasons_code",
                schema: "mfi",
                table: "kyc_reject_reasons",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_rejected_customers_CustomerBaseId",
                schema: "mfi",
                table: "kyc_rejected_customers",
                column: "CustomerBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_rejected_customers_ReasonId",
                schema: "mfi",
                table: "kyc_rejected_customers",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "ix_rejected_customers_customer",
                schema: "mfi",
                table: "kyc_rejected_customers",
                columns: new[] { "CustomerId", "CustomerType" });

            migrationBuilder.CreateIndex(
                name: "IX_kyc_saving_partners_IndividualId",
                schema: "mfi",
                table: "kyc_saving_partners",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_saving_partners_MemberId",
                schema: "mfi",
                table: "kyc_saving_partners",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_signatories_BusinessId",
                schema: "mfi",
                table: "kyc_signatories",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_title_deeds_CustomerBaseId",
                schema: "mfi",
                table: "kyc_title_deeds",
                column: "CustomerBaseId");

            migrationBuilder.CreateIndex(
                name: "ux_titles_name",
                schema: "mfi",
                table: "kyc_titles",
                column: "Name",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_unlocked_customers_CustomerBaseId",
                schema: "mfi",
                table: "kyc_unlocked_customers",
                column: "CustomerBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_kyc_unlocked_customers_ReasonId",
                schema: "mfi",
                table: "kyc_unlocked_customers",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_organizations_RegistrationNumber",
                schema: "mfi",
                table: "organizations",
                column: "RegistrationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_password_history_user_changed",
                schema: "mfi",
                table: "password_history",
                columns: new[] { "UserId", "ChangedOn" });

            migrationBuilder.CreateIndex(
                name: "ix_permissions_module_action",
                schema: "mfi",
                table: "permissions",
                columns: new[] { "Module", "Action" });

            migrationBuilder.CreateIndex(
                name: "ux_permissions_name",
                schema: "mfi",
                table: "permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_token",
                schema: "mfi",
                table: "refresh_tokens",
                column: "Token");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_user",
                schema: "mfi",
                table: "refresh_tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_role_group_members_RoleId",
                schema: "mfi",
                table: "role_group_members",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "ux_role_group_members_group_role",
                schema: "mfi",
                table: "role_group_members",
                columns: new[] { "RoleGroupId", "RoleId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_role_group_policy_overrides_SystemPolicyId",
                schema: "mfi",
                table: "role_group_policy_overrides",
                column: "SystemPolicyId");

            migrationBuilder.CreateIndex(
                name: "ux_role_group_policy_overrides_group_policy",
                schema: "mfi",
                table: "role_group_policy_overrides",
                columns: new[] { "RoleGroupId", "SystemPolicyId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_role_groups_name",
                schema: "mfi",
                table: "role_groups",
                column: "Name",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_PermissionId",
                schema: "mfi",
                table: "role_permissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "ux_role_permissions_role_permission",
                schema: "mfi",
                table: "role_permissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_roles_name",
                schema: "mfi",
                table: "roles",
                column: "Name",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_system_configs_module_key",
                schema: "mfi",
                table: "system_configs",
                columns: new[] { "Module", "Key" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_system_policies_module_name",
                schema: "mfi",
                table: "system_policies",
                columns: new[] { "Module", "Name" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ix_user_branch_access_branch",
                schema: "mfi",
                table: "user_branch_access",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "ix_user_branch_access_user",
                schema: "mfi",
                table: "user_branch_access",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "ux_user_branch_access_user_branch",
                schema: "mfi",
                table: "user_branch_access",
                columns: new[] { "UserId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_RoleId",
                schema: "mfi",
                table: "user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "ux_user_roles_user_role",
                schema: "mfi",
                table: "user_roles",
                columns: new[] { "UserId", "RoleId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ix_users_home_branch",
                schema: "mfi",
                table: "users",
                column: "DefaultBranchId");

            migrationBuilder.CreateIndex(
                name: "ux_users_email",
                schema: "mfi",
                table: "users",
                column: "Email",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_users_username",
                schema: "mfi",
                table: "users",
                column: "Username",
                unique: true,
                filter: "\"IsDeleted\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_logs",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "branch_holidays",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_cluster_members",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_customer_agreements",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_customer_approvals",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_customer_blacklists",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_customer_contacts",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_customer_contracts",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_employment_histories",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_image_files",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_income_histories",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_meetings",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_member_positions",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_member_transfers",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_rejected_customers",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_unlocked_customers",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "password_history",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "refresh_tokens",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "role_group_members",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "role_group_policy_overrides",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "role_permissions",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "system_configs",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "user_branch_access",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_clusters",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_identifications",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_other_files",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_title_deeds",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_income_types",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_group_positions",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_reject_reasons",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "role_groups",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "system_policies",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "users",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_guarantors",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_identification_types",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_issuer_authorities",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_saving_partners",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_signatories",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_file_attachments",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "customers_individuals",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "customers_members",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "customers_businesses",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "customers_groups",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_education_levels",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_nationalities",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_professions",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_titles",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "customers_base",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "branches",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_customer_exits",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_customer_filters",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_villages",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "organizations",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "kyc_general_reasons",
                schema: "mfi");
        }
    }
}
