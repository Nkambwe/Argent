using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Argent.Api.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mfi");

            migrationBuilder.CreateTable(
                name: "acc_branch_posting_groups",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ReceivablesAccountNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    PayablesAccountNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
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
                    table.PrimaryKey("PK_acc_branch_posting_groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_business_posting_groups",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BusinessType = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ReceivablesAccountNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    PayablesAccountNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    PrepaymentAccountNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
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
                    table.PrimaryKey("PK_acc_business_posting_groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_charge_groups",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeriesIdentifier = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SeriesPrefix = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    GroupName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    LastSeries = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_acc_charge_groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_charges",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Series = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ChargeName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IsRated = table.Column<bool>(type: "boolean", nullable: false),
                    AppliesToRegistration = table.Column<bool>(type: "boolean", nullable: false),
                    AppliesToSavings = table.Column<bool>(type: "boolean", nullable: false),
                    AppliesToTimeDeposits = table.Column<bool>(type: "boolean", nullable: false),
                    AppliesToShares = table.Column<bool>(type: "boolean", nullable: false),
                    AppliesToInsurance = table.Column<bool>(type: "boolean", nullable: false),
                    AppliesToLoans = table.Column<bool>(type: "boolean", nullable: false),
                    LastCount = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_acc_charges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_charts",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChartName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ChartType = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_acc_charts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_currencies",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SmallUnit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Symbol = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Precision = table.Column<int>(type: "integer", nullable: false),
                    Round = table.Column<int>(type: "integer", nullable: false),
                    IsBaseCurrency = table.Column<bool>(type: "boolean", nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_acc_currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_document_types",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TypeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_acc_document_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_folio_types",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TypeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_acc_folio_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_general_posting_groups",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    SalesAccountNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    PurchasesAccountNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    DiscountAccountNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CostOfGoodsAccountNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
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
                    table.PrimaryKey("PK_acc_general_posting_groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_ibans",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Narration = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
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
                    table.PrimaryKey("PK_acc_ibans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_ledger_headers",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentHeader = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LedgerNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LedgerName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AccountClassification = table.Column<int>(type: "integer", nullable: false),
                    AccountCategory = table.Column<int>(type: "integer", nullable: false),
                    AccountNature = table.Column<int>(type: "integer", nullable: false),
                    GroupIndex = table.Column<long>(type: "bigint", nullable: false),
                    LedgerIndex = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acc_ledger_headers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_references",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Series = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_acc_references", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_swifts",
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
                    table.PrimaryKey("PK_acc_swifts", x => x.Id);
                });

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
                name: "delivery_mode",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
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
                    table.PrimaryKey("PK_delivery_mode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "discount_groups",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    GroupName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
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
                    table.PrimaryKey("PK_discount_groups", x => x.Id);
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
                name: "payment_terms",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Terms = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
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
                    table.PrimaryKey("PK_payment_terms", x => x.Id);
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
                name: "price_groups",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    GroupName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
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
                    table.PrimaryKey("PK_price_groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Series = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order_classifications",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Notes = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
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
                    table.PrimaryKey("PK_purchase_order_classifications", x => x.Id);
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
                name: "TaxGroups",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SerieIdentifier = table.Column<string>(type: "text", nullable: false),
                    SeriePrefix = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    LastSeries = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vendor_delivery_terms",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
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
                    table.PrimaryKey("PK_vendor_delivery_terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vendor_groups",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    GroupName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
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
                    table.PrimaryKey("PK_vendor_groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vendor_item_groups",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ItemGroup = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
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
                    table.PrimaryKey("PK_vendor_item_groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "acc_charge_group_items",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ChargeName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IsRated = table.Column<bool>(type: "boolean", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric(10,4)", nullable: false),
                    FlatAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ChargeGroupId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_charge_group_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_charge_group_items_acc_charge_groups_ChargeGroupId",
                        column: x => x.ChargeGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_denominations",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Value = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
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
                    table.PrimaryKey("PK_acc_denominations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_denominations_acc_currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "mfi",
                        principalTable: "acc_currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_exchange_rates",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    Against = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Buy = table.Column<decimal>(type: "numeric(18,6)", nullable: false),
                    Sale = table.Column<decimal>(type: "numeric(18,6)", nullable: false),
                    Average = table.Column<decimal>(type: "numeric(18,6)", nullable: false),
                    IsRunning = table.Column<bool>(type: "boolean", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                    table.PrimaryKey("PK_acc_exchange_rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_exchange_rates_acc_currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "mfi",
                        principalTable: "acc_currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RevolvingFund",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Started = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ended = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SavingsBased = table.Column<bool>(type: "boolean", nullable: false),
                    LoanablePercentage = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    DonorId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevolvingFund", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RevolvingFund_acc_currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "mfi",
                        principalTable: "acc_currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_transaction_documents",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DocumentNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DocumentName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_transaction_documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_transaction_documents_acc_document_types_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "mfi",
                        principalTable: "acc_document_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acc_folios",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Particulars = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FolioTypeId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_folios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_folios_acc_folio_types_FolioTypeId",
                        column: x => x.FolioTypeId,
                        principalSchema: "mfi",
                        principalTable: "acc_folio_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acc_journal_types",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeriesIdentifier = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    JournalName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DefaultLedgerNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    AccountClassification = table.Column<int>(type: "integer", nullable: false),
                    AllowTaxDifference = table.Column<bool>(type: "boolean", nullable: false),
                    RequireVoucher = table.Column<bool>(type: "boolean", nullable: false),
                    MultiCurrency = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    GeneralPostingGroupId = table.Column<long>(type: "bigint", nullable: true),
                    BranchPostingGroupId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessPostingGroupId = table.Column<long>(type: "bigint", nullable: true),
                    ReferenceValue1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ReferenceValue2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ReferenceValue3 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ReferenceValue4 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ReferenceValue5 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ReferenceValue6 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ReasonId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_acc_journal_types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_journal_types_acc_branch_posting_groups_BranchPostingGr~",
                        column: x => x.BranchPostingGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_branch_posting_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_acc_journal_types_acc_business_posting_groups_BusinessPosti~",
                        column: x => x.BusinessPostingGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_business_posting_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_acc_journal_types_acc_general_posting_groups_GeneralPosting~",
                        column: x => x.GeneralPostingGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_general_posting_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "acc_voucher_types",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeriesIdentifier = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    VoucherName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DefaultLedgerNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Posting = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    GeneralPostingGroupId = table.Column<long>(type: "bigint", nullable: true),
                    BranchPostingGroupId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessPostingGroupId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_acc_voucher_types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_voucher_types_acc_branch_posting_groups_BranchPostingGr~",
                        column: x => x.BranchPostingGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_branch_posting_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_acc_voucher_types_acc_business_posting_groups_BusinessPosti~",
                        column: x => x.BusinessPostingGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_business_posting_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_acc_voucher_types_acc_general_posting_groups_GeneralPosting~",
                        column: x => x.GeneralPostingGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_general_posting_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "acc_ledger_totals",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LedgerAccountHeaderId = table.Column<long>(type: "bigint", nullable: false),
                    TotalRange = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LedgerNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LedgerName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AccountClassification = table.Column<int>(type: "integer", nullable: false),
                    AccountCategory = table.Column<int>(type: "integer", nullable: false),
                    AccountNature = table.Column<int>(type: "integer", nullable: false),
                    GroupIndex = table.Column<long>(type: "bigint", nullable: false),
                    LedgerIndex = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acc_ledger_totals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_ledger_totals_acc_ledger_headers_LedgerAccountHeaderId",
                        column: x => x.LedgerAccountHeaderId,
                        principalSchema: "mfi",
                        principalTable: "acc_ledger_headers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acc_reference_values",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Suspended = table.Column<bool>(type: "boolean", nullable: false),
                    Start = table.Column<DateOnly>(type: "date", nullable: true),
                    End = table.Column<DateOnly>(type: "date", nullable: true),
                    AllowManualEntry = table.Column<bool>(type: "boolean", nullable: false),
                    ReferenceId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_reference_values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_reference_values_acc_references_ReferenceId",
                        column: x => x.ReferenceId,
                        principalSchema: "mfi",
                        principalTable: "acc_references",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_banks",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Contact = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Telephone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Fax = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    IbanId = table.Column<long>(type: "bigint", nullable: true),
                    SwiftId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_acc_banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_banks_acc_ibans_IbanId",
                        column: x => x.IbanId,
                        principalSchema: "mfi",
                        principalTable: "acc_ibans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_acc_banks_acc_swifts_SwiftId",
                        column: x => x.SwiftId,
                        principalSchema: "mfi",
                        principalTable: "acc_swifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    AccountsChartId = table.Column<long>(type: "bigint", nullable: true),
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
                        name: "FK_branches_acc_charts_AccountsChartId",
                        column: x => x.AccountsChartId,
                        principalSchema: "mfi",
                        principalTable: "acc_charts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_branches_organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "mfi",
                        principalTable: "organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceProduct",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CoverageId = table.Column<long>(type: "bigint", nullable: false),
                    Period = table.Column<int>(type: "integer", nullable: false),
                    AllowPremiumModification = table.Column<bool>(type: "boolean", nullable: false),
                    ChargeMonthlyPremium = table.Column<bool>(type: "boolean", nullable: false),
                    PercentageAdministrativeAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    AdministrativeCostLedgerAccount = table.Column<string>(type: "text", nullable: false),
                    PercentageClaimAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    ClaimLedgerAccount = table.Column<string>(type: "text", nullable: false),
                    MinimumInsuredPersons = table.Column<int>(type: "integer", nullable: false),
                    MaximumInsuredPersons = table.Column<int>(type: "integer", nullable: false),
                    MinimumInsuredAge = table.Column<int>(type: "integer", nullable: false),
                    MaximumInsuredAge = table.Column<int>(type: "integer", nullable: false),
                    Fees = table.Column<decimal>(type: "numeric", nullable: false),
                    FeesLedgerAccount = table.Column<string>(type: "text", nullable: false),
                    ProductTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ChargeGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    VatInclusive = table.Column<bool>(type: "boolean", nullable: false),
                    UseChargeGroups = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceProduct_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalSchema: "mfi",
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsuranceProduct_acc_charge_groups_ChargeGroupId",
                        column: x => x.ChargeGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SavingProduct",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LimitWithdraw = table.Column<bool>(type: "boolean", nullable: false),
                    MaximumWithdraws = table.Column<int>(type: "integer", nullable: false),
                    WithdrawPenalty = table.Column<decimal>(type: "numeric", nullable: false),
                    ChargeWithdraws = table.Column<bool>(type: "boolean", nullable: false),
                    AllowOverdraft = table.Column<bool>(type: "boolean", nullable: false),
                    OverdraftInterest = table.Column<decimal>(type: "numeric", nullable: false),
                    MinimumBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    OfferInterest = table.Column<bool>(type: "boolean", nullable: false),
                    InterestRate = table.Column<decimal>(type: "numeric", nullable: false),
                    MinimumInterestOffered = table.Column<decimal>(type: "numeric", nullable: false),
                    ProductTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ChargeGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    VatInclusive = table.Column<bool>(type: "boolean", nullable: false),
                    UseChargeGroups = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingProduct_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalSchema: "mfi",
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavingProduct_acc_charge_groups_ChargeGroupId",
                        column: x => x.ChargeGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShareProduct",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ChargeGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    VatInclusive = table.Column<bool>(type: "boolean", nullable: false),
                    UseChargeGroups = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareProduct_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalSchema: "mfi",
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareProduct_acc_charge_groups_ChargeGroupId",
                        column: x => x.ChargeGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimedepositProduct",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WithdrawMode = table.Column<int>(type: "integer", nullable: false),
                    CapitalizeInterest = table.Column<bool>(type: "boolean", nullable: false),
                    ForfeitInterestForPrematureWithdraw = table.Column<bool>(type: "boolean", nullable: false),
                    PrematureWithdrawsPenalty = table.Column<decimal>(type: "numeric", nullable: false),
                    Period = table.Column<int>(type: "integer", nullable: false),
                    PeriodType = table.Column<int>(type: "integer", nullable: false),
                    MinimumAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    MaximumAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TierInterest = table.Column<bool>(type: "boolean", nullable: false),
                    ProductTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ChargeGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    VatInclusive = table.Column<bool>(type: "boolean", nullable: false),
                    UseChargeGroups = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedepositProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimedepositProduct_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalSchema: "mfi",
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimedepositProduct_acc_charge_groups_ChargeGroupId",
                        column: x => x.ChargeGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_groups",
                        principalColumn: "Id");
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
                name: "acc_taxes",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IsRated = table.Column<bool>(type: "boolean", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: false),
                    FlatAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Suspend = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    TaxGroupId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_taxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_taxes_TaxGroups_TaxGroupId",
                        column: x => x.TaxGroupId,
                        principalSchema: "mfi",
                        principalTable: "TaxGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vendor",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Series = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Alias = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Language = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LedgerAccount = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    VendorGroupId = table.Column<long>(type: "bigint", nullable: true),
                    DeliverTermsId = table.Column<long>(type: "bigint", nullable: true),
                    DeliveryModeId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_vendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vendor_delivery_mode_DeliveryModeId",
                        column: x => x.DeliveryModeId,
                        principalSchema: "mfi",
                        principalTable: "delivery_mode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vendor_vendor_delivery_terms_DeliverTermsId",
                        column: x => x.DeliverTermsId,
                        principalSchema: "mfi",
                        principalTable: "vendor_delivery_terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vendor_vendor_groups_VendorGroupId",
                        column: x => x.VendorGroupId,
                        principalSchema: "mfi",
                        principalTable: "vendor_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanProduct",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UseClasses = table.Column<bool>(type: "boolean", nullable: false),
                    SectorId = table.Column<long>(type: "bigint", nullable: true),
                    FundId = table.Column<long>(type: "bigint", nullable: true),
                    ProductTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ChargeGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    VatInclusive = table.Column<bool>(type: "boolean", nullable: false),
                    UseChargeGroups = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanProduct_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalSchema: "mfi",
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanProduct_RevolvingFund_FundId",
                        column: x => x.FundId,
                        principalSchema: "mfi",
                        principalTable: "RevolvingFund",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanProduct_acc_charge_groups_ChargeGroupId",
                        column: x => x.ChargeGroupId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "acc_ledger_accounts",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NormalBalance = table.Column<int>(type: "integer", nullable: false),
                    PostingType = table.Column<int>(type: "integer", nullable: false),
                    AllowManualPosting = table.Column<bool>(type: "boolean", nullable: false),
                    ShowParticulars = table.Column<bool>(type: "boolean", nullable: false),
                    Suspended = table.Column<bool>(type: "boolean", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LedgerAccountHeaderId = table.Column<long>(type: "bigint", nullable: false),
                    AccountsChartId = table.Column<long>(type: "bigint", nullable: false),
                    FolioId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LedgerNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LedgerName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AccountClassification = table.Column<int>(type: "integer", nullable: false),
                    AccountCategory = table.Column<int>(type: "integer", nullable: false),
                    AccountNature = table.Column<int>(type: "integer", nullable: false),
                    GroupIndex = table.Column<long>(type: "bigint", nullable: false),
                    LedgerIndex = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acc_ledger_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_ledger_accounts_acc_charts_AccountsChartId",
                        column: x => x.AccountsChartId,
                        principalSchema: "mfi",
                        principalTable: "acc_charts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_ledger_accounts_acc_currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "mfi",
                        principalTable: "acc_currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_ledger_accounts_acc_folios_FolioId",
                        column: x => x.FolioId,
                        principalSchema: "mfi",
                        principalTable: "acc_folios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_acc_ledger_accounts_acc_ledger_headers_LedgerAccountHeaderId",
                        column: x => x.LedgerAccountHeaderId,
                        principalSchema: "mfi",
                        principalTable: "acc_ledger_headers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acc_journal_type_tax_group",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JournalTypeId = table.Column<long>(type: "bigint", nullable: false),
                    TaxGroupId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_journal_type_tax_group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_journal_type_tax_group_TaxGroups_TaxGroupId",
                        column: x => x.TaxGroupId,
                        principalSchema: "mfi",
                        principalTable: "TaxGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_journal_type_tax_group_acc_journal_types_JournalTypeId",
                        column: x => x.JournalTypeId,
                        principalSchema: "mfi",
                        principalTable: "acc_journal_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_bank_branches",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BranchName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    BranchAddress = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    BranchContact = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ContactDesignation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ContactEmail = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    PrimaryLine = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    SecondaryLine = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    BranchFax = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    BankId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_bank_branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_bank_branches_acc_banks_BankId",
                        column: x => x.BankId,
                        principalSchema: "mfi",
                        principalTable: "acc_banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acc_branch_revenue_center",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CenterName = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Suspend = table.Column<bool>(type: "boolean", nullable: false),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BranchId = table.Column<long>(type: "bigint", maxLength: 80, nullable: true),
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
                    table.PrimaryKey("PK_acc_branch_revenue_center", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_branch_revenue_center_branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "acc_cost_center",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CenterName = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Suspend = table.Column<bool>(type: "boolean", nullable: false),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BranchId = table.Column<long>(type: "bigint", maxLength: 80, nullable: true),
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
                    table.PrimaryKey("PK_acc_cost_center", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_cost_center_branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "acc_financial_years",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    YearName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Period = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Closed = table.Column<bool>(type: "boolean", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_acc_financial_years", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_financial_years_branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "acc_ledger_recurring_items",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    StartsOn = table.Column<DateOnly>(type: "date", nullable: false),
                    EndsOn = table.Column<DateOnly>(type: "date", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PostDay = table.Column<int>(type: "integer", nullable: false),
                    PostingType = table.Column<int>(type: "integer", nullable: false),
                    AutoPost = table.Column<bool>(type: "boolean", nullable: false),
                    DebitLedger = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CreditLedger = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_acc_ledger_recurring_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_ledger_recurring_items_branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "acc_series_numbers",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Identifier = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CustomSeries = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    StartNumber = table.Column<long>(type: "bigint", nullable: false),
                    EndNumber = table.Column<long>(type: "bigint", nullable: false),
                    StartsOn = table.Column<DateOnly>(type: "date", nullable: false),
                    LastUsedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastNumber = table.Column<long>(type: "bigint", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    AllowManualOverride = table.Column<bool>(type: "boolean", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: true),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_series_numbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_series_numbers_acc_document_types_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "mfi",
                        principalTable: "acc_document_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_series_numbers_branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    Notes = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
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
                name: "branch_references",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Series = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Suspend = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BranchId = table.Column<long>(type: "bigint", nullable: true),
                    ReferenceValueId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_branch_references", x => x.Id);
                    table.ForeignKey(
                        name: "FK_branch_references_acc_reference_values_ReferenceValueId",
                        column: x => x.ReferenceValueId,
                        principalSchema: "mfi",
                        principalTable: "acc_reference_values",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_branch_references_branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id");
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
                name: "InsuranceProductTaxGroup",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    TaxGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceProductTaxGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceProductTaxGroup_InsuranceProduct_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "mfi",
                        principalTable: "InsuranceProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsuranceProductTaxGroup_TaxGroups_TaxGroupId",
                        column: x => x.TaxGroupId,
                        principalSchema: "mfi",
                        principalTable: "TaxGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavingProductTaxGroup",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SavingProductId = table.Column<long>(type: "bigint", nullable: false),
                    TaxGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingProductTaxGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingProductTaxGroup_SavingProduct_SavingProductId",
                        column: x => x.SavingProductId,
                        principalSchema: "mfi",
                        principalTable: "SavingProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavingProductTaxGroup_TaxGroups_TaxGroupId",
                        column: x => x.TaxGroupId,
                        principalSchema: "mfi",
                        principalTable: "TaxGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShareProductTaxGroup",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    TaxGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareProductTaxGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareProductTaxGroup_ShareProduct_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "mfi",
                        principalTable: "ShareProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareProductTaxGroup_TaxGroups_TaxGroupId",
                        column: x => x.TaxGroupId,
                        principalSchema: "mfi",
                        principalTable: "TaxGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimedepositProductTaxGroup",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TimedepositProductId = table.Column<long>(type: "bigint", nullable: false),
                    TaxGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedepositProductTaxGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimedepositProductTaxGroup_TaxGroups_TaxGroupId",
                        column: x => x.TaxGroupId,
                        principalSchema: "mfi",
                        principalTable: "TaxGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimedepositProductTaxGroup_TimedepositProduct_TimedepositPr~",
                        column: x => x.TimedepositProductId,
                        principalSchema: "mfi",
                        principalTable: "TimedepositProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_charge_items",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ChargeOn = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FixedAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IsRated = table.Column<bool>(type: "boolean", nullable: false),
                    Percentage = table.Column<decimal>(type: "numeric(10,4)", nullable: false),
                    LedgerCode = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    TaxId = table.Column<long>(type: "bigint", nullable: true),
                    SavingProductId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_acc_charge_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_charge_items_SavingProduct_SavingProductId",
                        column: x => x.SavingProductId,
                        principalSchema: "mfi",
                        principalTable: "SavingProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_acc_charge_items_acc_taxes_TaxId",
                        column: x => x.TaxId,
                        principalSchema: "mfi",
                        principalTable: "acc_taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "acc_cards",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Holder = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CardNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    TransactionType = table.Column<int>(type: "integer", nullable: false),
                    Freeze = table.Column<bool>(type: "boolean", nullable: false),
                    Limit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    VendorId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_cards_vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "mfi",
                        principalTable: "vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_vendor_references",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VendorId = table.Column<long>(type: "bigint", nullable: false),
                    ReferenceValueId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_vendor_references", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_vendor_references_acc_reference_values_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "mfi",
                        principalTable: "acc_reference_values",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_vendor_references_vendor_ReferenceValueId",
                        column: x => x.ReferenceValueId,
                        principalSchema: "mfi",
                        principalTable: "vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_vendor_taxes",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VendorId = table.Column<long>(type: "bigint", nullable: false),
                    TaxId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_vendor_taxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_vendor_taxes_acc_taxes_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "mfi",
                        principalTable: "acc_taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_vendor_taxes_vendor_TaxId",
                        column: x => x.TaxId,
                        principalSchema: "mfi",
                        principalTable: "vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_contracts",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactPerson = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    BusinessTitle = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    VendorId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_business_contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_business_contracts_vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "mfi",
                        principalTable: "vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "delivery_defaults",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Receiver = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    ReferenceGroup = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    ReferenceValue = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    DeliveryAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Notes = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    VendorId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_delivery_defaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_delivery_defaults_vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "mfi",
                        principalTable: "vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoicing_defaults",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MultiBranchInvoiceAccount = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    InvoicingLedger = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    InvoicingAddress = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    PriceIncludesSalesTax = table.Column<bool>(type: "boolean", nullable: false),
                    PriceIncludesWithHoldingTax = table.Column<bool>(type: "boolean", nullable: false),
                    PriceIncludesVat = table.Column<bool>(type: "boolean", nullable: false),
                    VendorId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_invoicing_defaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoicing_defaults_vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "mfi",
                        principalTable: "vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_defaults",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactPerson = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    ReferenceGroup = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ReferenceValue = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PurchaseOfficer = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Notes = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    VendorId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_purchase_defaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_purchase_defaults_vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "mfi",
                        principalTable: "vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vendor_addresses",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    For = table.Column<int>(type: "integer", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    VendorId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_vendor_addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vendor_addresses_vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "mfi",
                        principalTable: "vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_tax_items",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Item = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Suspend = table.Column<bool>(type: "boolean", nullable: false),
                    Started = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TaxId = table.Column<long>(type: "bigint", nullable: false),
                    TimedepositProductId = table.Column<long>(type: "bigint", nullable: true),
                    InsuranceProductId = table.Column<long>(type: "bigint", nullable: true),
                    ShareProductId = table.Column<long>(type: "bigint", nullable: true),
                    SavingProductId = table.Column<long>(type: "bigint", nullable: true),
                    LoanProductId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_acc_tax_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_tax_items_InsuranceProduct_TimedepositProductId",
                        column: x => x.TimedepositProductId,
                        principalSchema: "mfi",
                        principalTable: "InsuranceProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_tax_items_LoanProduct_LoanProductId",
                        column: x => x.LoanProductId,
                        principalSchema: "mfi",
                        principalTable: "LoanProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_tax_items_SavingProduct_SavingProductId",
                        column: x => x.SavingProductId,
                        principalSchema: "mfi",
                        principalTable: "SavingProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_tax_items_ShareProduct_ShareProductId",
                        column: x => x.ShareProductId,
                        principalSchema: "mfi",
                        principalTable: "ShareProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_tax_items_TimedepositProduct_InsuranceProductId",
                        column: x => x.InsuranceProductId,
                        principalSchema: "mfi",
                        principalTable: "TimedepositProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_tax_items_acc_taxes_TaxId",
                        column: x => x.TaxId,
                        principalSchema: "mfi",
                        principalTable: "acc_taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanProductTaxGroup",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    TaxGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanProductTaxGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanProductTaxGroup_LoanProduct_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "mfi",
                        principalTable: "LoanProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanProductTaxGroup_TaxGroups_TaxGroupId",
                        column: x => x.TaxGroupId,
                        principalSchema: "mfi",
                        principalTable: "TaxGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_branch_ledger",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LedgerAccountNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    BranchAccountNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Suspend = table.Column<bool>(type: "boolean", nullable: false),
                    LedgerAccountId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", maxLength: 80, nullable: false),
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
                    table.PrimaryKey("PK_acc_branch_ledger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_branch_ledger_acc_ledger_accounts_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "acc_ledger_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_acc_branch_ledger_branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "acc_cash_accounts",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LedgerNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LedgerAccountId = table.Column<long>(type: "bigint", nullable: false),
                    MinimumPayout = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MaximumPayout = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AllowMultiCurrency = table.Column<bool>(type: "boolean", nullable: false),
                    LedgerAccountId1 = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_acc_cash_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_cash_accounts_acc_ledger_accounts_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_ledger_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_cash_accounts_acc_ledger_accounts_LedgerAccountId1",
                        column: x => x.LedgerAccountId1,
                        principalSchema: "mfi",
                        principalTable: "acc_ledger_accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "acc_ledger_account_references",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LedgerAccountId = table.Column<long>(type: "bigint", nullable: false),
                    ReferenceId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_ledger_account_references", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_ledger_account_references_acc_ledger_accounts_LedgerAcc~",
                        column: x => x.LedgerAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_ledger_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_ledger_account_references_acc_references_ReferenceId",
                        column: x => x.ReferenceId,
                        principalSchema: "mfi",
                        principalTable: "acc_references",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acc_bank_accounts",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HolderCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AccountName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AccountNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IbanNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SwiftNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AccountFor = table.Column<int>(type: "integer", nullable: false),
                    AllowedOperations = table.Column<int>(type: "integer", nullable: false),
                    MultiCurrency = table.Column<bool>(type: "boolean", nullable: false),
                    WithdrawInterval = table.Column<int>(type: "integer", nullable: false),
                    WithdrawIntervalUnit = table.Column<int>(type: "integer", nullable: false),
                    HasChequeBook = table.Column<bool>(type: "boolean", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ExcludeBranches = table.Column<bool>(type: "boolean", nullable: false),
                    LedgerAccountId = table.Column<long>(type: "bigint", nullable: true),
                    BankBranchId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_bank_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_bank_accounts_acc_bank_branches_BankBranchId",
                        column: x => x.BankBranchId,
                        principalSchema: "mfi",
                        principalTable: "acc_bank_branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_bank_accounts_acc_ledger_accounts_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_ledger_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "acc_monthly_closures",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinancialYearId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_monthly_closures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_monthly_closures_acc_financial_years_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalSchema: "mfi",
                        principalTable: "acc_financial_years",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "acc_cashiers",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CurrentBranch = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DefaultAccount = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    LowerLimit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    UpperLimit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_acc_cashiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_cashiers_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "mfi",
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "lnr_loan_officer",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LoanOfficerCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ApprovalLimit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AppUserId = table.Column<long>(type: "bigint", nullable: false),
                    AppUserId1 = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_lnr_loan_officer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lnr_loan_officer_users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "mfi",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_lnr_loan_officer_users_AppUserId1",
                        column: x => x.AppUserId1,
                        principalSchema: "mfi",
                        principalTable: "users",
                        principalColumn: "Id");
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
                name: "tellers",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TellerCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    MaximumLimit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MinimumLimit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AppUserId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_tellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tellers_users_AppUserId",
                        column: x => x.AppUserId,
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
                name: "acc_charge_item_charges",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChargeItemId = table.Column<long>(type: "bigint", nullable: false),
                    ChargeId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_charge_item_charges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_charge_item_charges_acc_charge_items_ChargeItemId",
                        column: x => x.ChargeItemId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_charge_item_charges_acc_charges_ChargeId",
                        column: x => x.ChargeId,
                        principalSchema: "mfi",
                        principalTable: "acc_charges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_charge_ledger_entry",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Series = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Customer = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    LoanNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Product = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    LedgerNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PostedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChargeItemId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_charge_ledger_entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_charge_ledger_entry_acc_charge_items_ChargeItemId",
                        column: x => x.ChargeItemId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acc_registration_ledger",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ClientCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    PostedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChargeItemId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_registration_ledger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_registration_ledger_acc_charge_items_ChargeItemId",
                        column: x => x.ChargeItemId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceProductChargeItem",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsuranceProductId = table.Column<long>(type: "bigint", nullable: false),
                    ChargeItemId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceProductChargeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceProductChargeItem_InsuranceProduct_InsuranceProduc~",
                        column: x => x.InsuranceProductId,
                        principalSchema: "mfi",
                        principalTable: "InsuranceProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsuranceProductChargeItem_acc_charge_items_ChargeItemId",
                        column: x => x.ChargeItemId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "lnr_charge_stages",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChargeItemId = table.Column<long>(type: "bigint", nullable: false),
                    LoanProductId = table.Column<long>(type: "bigint", nullable: false),
                    BeforeApplication = table.Column<bool>(type: "boolean", nullable: false),
                    BeforeApproval = table.Column<bool>(type: "boolean", nullable: false),
                    AfterApproval = table.Column<bool>(type: "boolean", nullable: false),
                    AtDisbursement = table.Column<bool>(type: "boolean", nullable: false),
                    AtAccountOpening = table.Column<bool>(type: "boolean", nullable: false),
                    AtAccountClosure = table.Column<bool>(type: "boolean", nullable: false),
                    Recurring = table.Column<bool>(type: "boolean", nullable: false),
                    InsuranceProductId = table.Column<long>(type: "bigint", nullable: true),
                    SavingProductId = table.Column<long>(type: "bigint", nullable: true),
                    ShareProductId = table.Column<long>(type: "bigint", nullable: true),
                    TimedepositProductId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_lnr_charge_stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lnr_charge_stages_InsuranceProduct_InsuranceProductId",
                        column: x => x.InsuranceProductId,
                        principalSchema: "mfi",
                        principalTable: "InsuranceProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_lnr_charge_stages_LoanProduct_ChargeItemId",
                        column: x => x.ChargeItemId,
                        principalSchema: "mfi",
                        principalTable: "LoanProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lnr_charge_stages_SavingProduct_SavingProductId",
                        column: x => x.SavingProductId,
                        principalSchema: "mfi",
                        principalTable: "SavingProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_lnr_charge_stages_ShareProduct_ShareProductId",
                        column: x => x.ShareProductId,
                        principalSchema: "mfi",
                        principalTable: "ShareProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_lnr_charge_stages_TimedepositProduct_TimedepositProductId",
                        column: x => x.TimedepositProductId,
                        principalSchema: "mfi",
                        principalTable: "TimedepositProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_lnr_charge_stages_acc_charge_items_ChargeItemId",
                        column: x => x.ChargeItemId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanProductChargeItem",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LoanProductId = table.Column<long>(type: "bigint", nullable: false),
                    ChargeItemId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanProductChargeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanProductChargeItem_LoanProduct_LoanProductId",
                        column: x => x.LoanProductId,
                        principalSchema: "mfi",
                        principalTable: "LoanProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanProductChargeItem_acc_charge_items_ChargeItemId",
                        column: x => x.ChargeItemId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SavingProductChargeItem",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SavingProductId = table.Column<long>(type: "bigint", nullable: false),
                    ChargeItemId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingProductChargeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavingProductChargeItem_SavingProduct_SavingProductId",
                        column: x => x.SavingProductId,
                        principalSchema: "mfi",
                        principalTable: "SavingProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavingProductChargeItem_acc_charge_items_ChargeItemId",
                        column: x => x.ChargeItemId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ShareProductChargeItem",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShareProductId = table.Column<long>(type: "bigint", nullable: false),
                    ChargeItemId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareProductChargeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareProductChargeItem_ShareProduct_ShareProductId",
                        column: x => x.ShareProductId,
                        principalSchema: "mfi",
                        principalTable: "ShareProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareProductChargeItem_acc_charge_items_ChargeItemId",
                        column: x => x.ChargeItemId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TimedepositProductChargeItem",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TimedepositProductId = table.Column<long>(type: "bigint", nullable: false),
                    ChargeItemId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedepositProductChargeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimedepositProductChargeItem_TimedepositProduct_Timedeposit~",
                        column: x => x.TimedepositProductId,
                        principalSchema: "mfi",
                        principalTable: "TimedepositProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimedepositProductChargeItem_acc_charge_items_ChargeItemId",
                        column: x => x.ChargeItemId,
                        principalSchema: "mfi",
                        principalTable: "acc_charge_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "acc_bank_account_currencies",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BankAccountId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_bank_account_currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_bank_account_currencies_acc_bank_accounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_bank_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_bank_account_currencies_acc_currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "mfi",
                        principalTable: "acc_currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_bank_ledger",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PostedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FolioCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    VoucherNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LedgerCode = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Debit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    TransactionType = table.Column<int>(type: "integer", nullable: false),
                    Nature = table.Column<int>(type: "integer", nullable: false),
                    Clearance = table.Column<int>(type: "integer", nullable: false),
                    Reconciled = table.Column<bool>(type: "boolean", nullable: false),
                    BankAccountId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionDocumentId = table.Column<long>(type: "bigint", nullable: true),
                    BankAccountId1 = table.Column<long>(type: "bigint", nullable: true),
                    TransactionDocumentId1 = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_acc_bank_ledger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_bank_ledger_acc_bank_accounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_bank_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_bank_ledger_acc_bank_accounts_BankAccountId1",
                        column: x => x.BankAccountId1,
                        principalSchema: "mfi",
                        principalTable: "acc_bank_accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_acc_bank_ledger_acc_transaction_documents_TransactionDocume~",
                        column: x => x.TransactionDocumentId,
                        principalSchema: "mfi",
                        principalTable: "acc_transaction_documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_acc_bank_ledger_acc_transaction_documents_TransactionDocum~1",
                        column: x => x.TransactionDocumentId1,
                        principalSchema: "mfi",
                        principalTable: "acc_transaction_documents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "acc_cheque_books",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BankAccountId = table.Column<long>(type: "bigint", nullable: false),
                    SerialNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstChequeNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LastChequeNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    NumberOfLeafs = table.Column<int>(type: "integer", nullable: false),
                    LastIssuedCheque = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_acc_cheque_books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_cheque_books_acc_bank_accounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_bank_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payment_defaults",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    PaymentTermId = table.Column<long>(type: "bigint", nullable: true),
                    VendorId = table.Column<long>(type: "bigint", nullable: true),
                    BankAccountId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_payment_defaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payment_defaults_acc_bank_accounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_bank_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payment_defaults_payment_terms_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalSchema: "mfi",
                        principalTable: "payment_terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payment_defaults_vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "mfi",
                        principalTable: "vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order_defaults",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MultiBranchAccount = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    VendorId = table.Column<long>(type: "bigint", nullable: true),
                    VendorGroupId = table.Column<long>(type: "bigint", nullable: true),
                    VendorItemGroupId = table.Column<long>(type: "bigint", nullable: true),
                    DiscountGroupId = table.Column<long>(type: "bigint", nullable: true),
                    PriceGroupId = table.Column<long>(type: "bigint", nullable: true),
                    PurchaseOrderClassificationId = table.Column<long>(type: "bigint", nullable: true),
                    BankAccountId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_purchase_order_defaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_purchase_order_defaults_acc_bank_accounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_bank_accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_purchase_order_defaults_discount_groups_DiscountGroupId",
                        column: x => x.DiscountGroupId,
                        principalSchema: "mfi",
                        principalTable: "discount_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_defaults_price_groups_PriceGroupId",
                        column: x => x.PriceGroupId,
                        principalSchema: "mfi",
                        principalTable: "price_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_defaults_purchase_order_classifications_Purc~",
                        column: x => x.PurchaseOrderClassificationId,
                        principalSchema: "mfi",
                        principalTable: "purchase_order_classifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_defaults_vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "mfi",
                        principalTable: "vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_defaults_vendor_groups_VendorGroupId",
                        column: x => x.VendorGroupId,
                        principalSchema: "mfi",
                        principalTable: "vendor_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_defaults_vendor_item_groups_VendorItemGroupId",
                        column: x => x.VendorItemGroupId,
                        principalSchema: "mfi",
                        principalTable: "vendor_item_groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vendor_bank_accounts",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VendorId = table.Column<long>(type: "bigint", nullable: false),
                    BankAccountId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_vendor_bank_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vendor_bank_accounts_acc_bank_accounts_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "mfi",
                        principalTable: "acc_bank_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vendor_bank_accounts_vendor_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "mfi",
                        principalTable: "vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_general_ledger",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LedgerAccountId = table.Column<long>(type: "bigint", nullable: false),
                    TaxGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TransactionCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PostedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Particulars = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    FolioCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LedgerNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    PostingSeries = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    VoucherNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Debit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ExchangeAmount = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    GeneralReference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    BusinessReference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ChargeReference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference3 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference4 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference5 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference6 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TaxCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    TaxCharge1 = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TaxCharge2 = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Closed = table.Column<bool>(type: "boolean", nullable: false),
                    ClosedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Cashier = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MonthlyClosureId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acc_general_ledger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_general_ledger_TaxGroups_TaxGroupId",
                        column: x => x.TaxGroupId,
                        principalSchema: "mfi",
                        principalTable: "TaxGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_general_ledger_acc_ledger_accounts_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_ledger_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_general_ledger_acc_monthly_closures_MonthlyClosureId",
                        column: x => x.MonthlyClosureId,
                        principalSchema: "mfi",
                        principalTable: "acc_monthly_closures",
                        principalColumn: "Id");
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
                name: "acc_cashier_accounts",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashierId = table.Column<long>(type: "bigint", nullable: false),
                    CashAccountId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_cashier_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_cashier_accounts_acc_cash_accounts_CashAccountId",
                        column: x => x.CashAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_cash_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_cashier_accounts_acc_cashiers_CashierId",
                        column: x => x.CashierId,
                        principalSchema: "mfi",
                        principalTable: "acc_cashiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_cashier_branch_access",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashierId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_cashier_branch_access", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_cashier_branch_access_acc_cashiers_CashierId",
                        column: x => x.CashierId,
                        principalSchema: "mfi",
                        principalTable: "acc_cashiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_cashier_branch_access_branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "mfi",
                        principalTable: "branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acc_cashier_journal_types",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashierId = table.Column<long>(type: "bigint", nullable: false),
                    JournalTypeId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_cashier_journal_types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_cashier_journal_types_acc_cashiers_CashierId",
                        column: x => x.CashierId,
                        principalSchema: "mfi",
                        principalTable: "acc_cashiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_cashier_journal_types_acc_journal_types_JournalTypeId",
                        column: x => x.JournalTypeId,
                        principalSchema: "mfi",
                        principalTable: "acc_journal_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acc_cashier_voucher_types",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashierId = table.Column<long>(type: "bigint", nullable: false),
                    VoucherTypeId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_acc_cashier_voucher_types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_cashier_voucher_types_acc_cashiers_CashierId",
                        column: x => x.CashierId,
                        principalSchema: "mfi",
                        principalTable: "acc_cashiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_cashier_voucher_types_acc_voucher_types_VoucherTypeId",
                        column: x => x.VoucherTypeId,
                        principalSchema: "mfi",
                        principalTable: "acc_voucher_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lnr_loan_officer_acc",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LoanOfficerId = table.Column<long>(type: "bigint", nullable: false),
                    LegderAccountId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_lnr_loan_officer_acc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lnr_loan_officer_acc_acc_ledger_accounts_LegderAccountId",
                        column: x => x.LegderAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_ledger_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_lnr_loan_officer_acc_lnr_loan_officer_LoanOfficerId",
                        column: x => x.LoanOfficerId,
                        principalSchema: "mfi",
                        principalTable: "lnr_loan_officer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bnk_teller_acc",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TellerId = table.Column<long>(type: "bigint", nullable: false),
                    LegderAccountId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_bnk_teller_acc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bnk_teller_acc_acc_ledger_accounts_LegderAccountId",
                        column: x => x.LegderAccountId,
                        principalSchema: "mfi",
                        principalTable: "acc_ledger_accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bnk_teller_acc_tellers_TellerId",
                        column: x => x.TellerId,
                        principalSchema: "mfi",
                        principalTable: "tellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "acc_cheques",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChequeBookId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    IssuerAccount = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Recipient = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    RecipientAccount = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AmountInWords = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Reversed = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_acc_cheques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_cheques_acc_cheque_books_ChequeBookId",
                        column: x => x.ChequeBookId,
                        principalSchema: "mfi",
                        principalTable: "acc_cheque_books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acc_card_ledger",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardId = table.Column<long>(type: "bigint", nullable: false),
                    ExternalTransactionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Particulars = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    FolioCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    GeneralLedgerEntryId = table.Column<long>(type: "bigint", nullable: false),
                    CardId1 = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_acc_card_ledger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_card_ledger_acc_cards_CardId",
                        column: x => x.CardId,
                        principalSchema: "mfi",
                        principalTable: "acc_cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_card_ledger_acc_cards_CardId1",
                        column: x => x.CardId1,
                        principalSchema: "mfi",
                        principalTable: "acc_cards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_acc_card_ledger_acc_general_ledger_GeneralLedgerEntryId",
                        column: x => x.GeneralLedgerEntryId,
                        principalSchema: "mfi",
                        principalTable: "acc_general_ledger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "acc_journal_entries",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JournalTypeId = table.Column<long>(type: "bigint", nullable: false),
                    UnPosted = table.Column<bool>(type: "boolean", nullable: false),
                    Approved = table.Column<bool>(type: "boolean", nullable: false),
                    ApprovedBy = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Voided = table.Column<bool>(type: "boolean", nullable: false),
                    Reversed = table.Column<bool>(type: "boolean", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: false),
                    GeneralLedgerEntryId = table.Column<long>(type: "bigint", nullable: true),
                    GeneralLedgerEntryId1 = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TransactionCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PostedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Particulars = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    FolioCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LedgerNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    PostingSeries = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    VoucherNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Debit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ExchangeAmount = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    GeneralReference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    BusinessReference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ChargeReference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference3 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference4 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference5 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Reference6 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TaxCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    TaxCharge1 = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TaxCharge2 = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Closed = table.Column<bool>(type: "boolean", nullable: false),
                    ClosedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Cashier = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MonthlyClosureId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acc_journal_entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_journal_entries_acc_general_ledger_GeneralLedgerEntryId",
                        column: x => x.GeneralLedgerEntryId,
                        principalSchema: "mfi",
                        principalTable: "acc_general_ledger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_journal_entries_acc_general_ledger_GeneralLedgerEntryId1",
                        column: x => x.GeneralLedgerEntryId1,
                        principalSchema: "mfi",
                        principalTable: "acc_general_ledger",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_acc_journal_entries_acc_journal_types_JournalTypeId",
                        column: x => x.JournalTypeId,
                        principalSchema: "mfi",
                        principalTable: "acc_journal_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_journal_entries_acc_monthly_closures_MonthlyClosureId",
                        column: x => x.MonthlyClosureId,
                        principalSchema: "mfi",
                        principalTable: "acc_monthly_closures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "acc_voucher_lines",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PostedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Particulars = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    FolioCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    VoucherNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RelatesTo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Ref = table.Column<int>(type: "integer", nullable: false),
                    Payment = table.Column<int>(type: "integer", nullable: false),
                    Clearance = table.Column<int>(type: "integer", nullable: false),
                    Debit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Authorized = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CashierCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    VoucherTypeId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionDocumentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionDocumentId = table.Column<long>(type: "bigint", nullable: false),
                    GeneralLedgerEntryId = table.Column<long>(type: "bigint", nullable: true),
                    TransactionDocumentId1 = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_acc_voucher_lines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_voucher_lines_acc_document_types_TransactionDocumentTyp~",
                        column: x => x.TransactionDocumentTypeId,
                        principalSchema: "mfi",
                        principalTable: "acc_document_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_voucher_lines_acc_general_ledger_GeneralLedgerEntryId",
                        column: x => x.GeneralLedgerEntryId,
                        principalSchema: "mfi",
                        principalTable: "acc_general_ledger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_acc_voucher_lines_acc_transaction_documents_TransactionDocu~",
                        column: x => x.TransactionDocumentId,
                        principalSchema: "mfi",
                        principalTable: "acc_transaction_documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_acc_voucher_lines_acc_transaction_documents_TransactionDoc~1",
                        column: x => x.TransactionDocumentId1,
                        principalSchema: "mfi",
                        principalTable: "acc_transaction_documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_acc_voucher_lines_acc_voucher_types_VoucherTypeId",
                        column: x => x.VoucherTypeId,
                        principalSchema: "mfi",
                        principalTable: "acc_voucher_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "acc_cheque_ledger",
                schema: "mfi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChequeId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FolioCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Particulars = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_acc_cheque_ledger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_acc_cheque_ledger_acc_cheques_ChequeId",
                        column: x => x.ChequeId,
                        principalSchema: "mfi",
                        principalTable: "acc_cheques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "acc_cheque_bank_ledger_links",
                schema: "mfi",
                columns: table => new
                {
                    BankEntriesId = table.Column<long>(type: "bigint", nullable: false),
                    ChequeEntriesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acc_cheque_bank_ledger_links", x => new { x.BankEntriesId, x.ChequeEntriesId });
                    table.ForeignKey(
                        name: "FK_acc_cheque_bank_ledger_links_acc_bank_ledger_BankEntriesId",
                        column: x => x.BankEntriesId,
                        principalSchema: "mfi",
                        principalTable: "acc_bank_ledger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_acc_cheque_bank_ledger_links_acc_cheque_ledger_ChequeEntrie~",
                        column: x => x.ChequeEntriesId,
                        principalSchema: "mfi",
                        principalTable: "acc_cheque_ledger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_acc_bank_account_currencies_CurrencyId",
                schema: "mfi",
                table: "acc_bank_account_currencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_bank_acct_currencies",
                schema: "mfi",
                table: "acc_bank_account_currencies",
                columns: new[] { "BankAccountId", "CurrencyId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_bank_accounts_LedgerAccountId",
                schema: "mfi",
                table: "acc_bank_accounts",
                column: "LedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_bank_accounts_branch_number",
                schema: "mfi",
                table: "acc_bank_accounts",
                columns: new[] { "BankBranchId", "AccountNumber" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_acc_bank_branches_bank_code",
                schema: "mfi",
                table: "acc_bank_branches",
                columns: new[] { "BankId", "BranchCode" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ix_acc_bank_ledger_account",
                schema: "mfi",
                table: "acc_bank_ledger",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "ix_acc_bank_ledger_account_reconciled",
                schema: "mfi",
                table: "acc_bank_ledger",
                columns: new[] { "BankAccountId", "Reconciled" });

            migrationBuilder.CreateIndex(
                name: "IX_acc_bank_ledger_BankAccountId1",
                schema: "mfi",
                table: "acc_bank_ledger",
                column: "BankAccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_acc_bank_ledger_TransactionDocumentId",
                schema: "mfi",
                table: "acc_bank_ledger",
                column: "TransactionDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_bank_ledger_TransactionDocumentId1",
                schema: "mfi",
                table: "acc_bank_ledger",
                column: "TransactionDocumentId1");

            migrationBuilder.CreateIndex(
                name: "ix_acc_bank_ledger_txn",
                schema: "mfi",
                table: "acc_bank_ledger",
                column: "TransactionCode");

            migrationBuilder.CreateIndex(
                name: "IX_acc_banks_IbanId",
                schema: "mfi",
                table: "acc_banks",
                column: "IbanId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_banks_SwiftId",
                schema: "mfi",
                table: "acc_banks",
                column: "SwiftId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_banks_code",
                schema: "mfi",
                table: "acc_banks",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_branch_ledger_BranchId",
                schema: "mfi",
                table: "acc_branch_ledger",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_branch_posting_groups_code",
                schema: "mfi",
                table: "acc_branch_posting_groups",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_branch_revenue_center_BranchId",
                schema: "mfi",
                table: "acc_branch_revenue_center",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_business_posting_groups_code",
                schema: "mfi",
                table: "acc_business_posting_groups",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ix_acc_card_ledger_card",
                schema: "mfi",
                table: "acc_card_ledger",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_card_ledger_CardId1",
                schema: "mfi",
                table: "acc_card_ledger",
                column: "CardId1");

            migrationBuilder.CreateIndex(
                name: "IX_acc_card_ledger_GeneralLedgerEntryId",
                schema: "mfi",
                table: "acc_card_ledger",
                column: "GeneralLedgerEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_cards_VendorId",
                schema: "mfi",
                table: "acc_cards",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "ix_acc_cash_accounts_ledger",
                schema: "mfi",
                table: "acc_cash_accounts",
                column: "LedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_cash_accounts_LedgerAccountId1",
                schema: "mfi",
                table: "acc_cash_accounts",
                column: "LedgerAccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_acc_cashier_accounts_CashAccountId",
                schema: "mfi",
                table: "acc_cashier_accounts",
                column: "CashAccountId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_cashier_accounts",
                schema: "mfi",
                table: "acc_cashier_accounts",
                columns: new[] { "CashierId", "CashAccountId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_cashier_branch_access_BranchId",
                schema: "mfi",
                table: "acc_cashier_branch_access",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_cashier_branch_access",
                schema: "mfi",
                table: "acc_cashier_branch_access",
                columns: new[] { "CashierId", "BranchId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_cashier_journal_types_JournalTypeId",
                schema: "mfi",
                table: "acc_cashier_journal_types",
                column: "JournalTypeId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_cashier_journal_types",
                schema: "mfi",
                table: "acc_cashier_journal_types",
                columns: new[] { "CashierId", "JournalTypeId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_cashier_voucher_types_VoucherTypeId",
                schema: "mfi",
                table: "acc_cashier_voucher_types",
                column: "VoucherTypeId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_cashier_voucher_types",
                schema: "mfi",
                table: "acc_cashier_voucher_types",
                columns: new[] { "CashierId", "VoucherTypeId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_cashiers_UserId",
                schema: "mfi",
                table: "acc_cashiers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_cashiers_code",
                schema: "mfi",
                table: "acc_cashiers",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_charge_group_items_ChargeGroupId",
                schema: "mfi",
                table: "acc_charge_group_items",
                column: "ChargeGroupId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_charge_groups_name",
                schema: "mfi",
                table: "acc_charge_groups",
                column: "GroupName",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_charge_item_charges_ChargeId",
                schema: "mfi",
                table: "acc_charge_item_charges",
                column: "ChargeId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_charge_item_charges",
                schema: "mfi",
                table: "acc_charge_item_charges",
                columns: new[] { "ChargeItemId", "ChargeId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_charge_items_SavingProductId",
                schema: "mfi",
                table: "acc_charge_items",
                column: "SavingProductId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_charge_items_TaxId",
                schema: "mfi",
                table: "acc_charge_items",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_charge_items_code",
                schema: "mfi",
                table: "acc_charge_items",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_charge_ledger_entry_ChargeItemId",
                schema: "mfi",
                table: "acc_charge_ledger_entry",
                column: "ChargeItemId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_charges_code",
                schema: "mfi",
                table: "acc_charges",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_acc_charts_name",
                schema: "mfi",
                table: "acc_charts",
                column: "ChartName",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_cheque_bank_ledger_links_ChequeEntriesId",
                schema: "mfi",
                table: "acc_cheque_bank_ledger_links",
                column: "ChequeEntriesId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_cheque_books_BankAccountId",
                schema: "mfi",
                table: "acc_cheque_books",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_cheque_books_serial",
                schema: "mfi",
                table: "acc_cheque_books",
                column: "SerialNumber",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ix_acc_cheque_ledger",
                schema: "mfi",
                table: "acc_cheque_ledger",
                column: "ChequeId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_cheques_book_number",
                schema: "mfi",
                table: "acc_cheques",
                columns: new[] { "ChequeBookId", "Number" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_cost_center_BranchId",
                schema: "mfi",
                table: "acc_cost_center",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "ix_acc_currencies_base",
                schema: "mfi",
                table: "acc_currencies",
                column: "IsBaseCurrency");

            migrationBuilder.CreateIndex(
                name: "ux_acc_currencies_code",
                schema: "mfi",
                table: "acc_currencies",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_denominations_CurrencyId",
                schema: "mfi",
                table: "acc_denominations",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_document_types_code",
                schema: "mfi",
                table: "acc_document_types",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ix_acc_exchange_rates_currency_pair_running",
                schema: "mfi",
                table: "acc_exchange_rates",
                columns: new[] { "CurrencyId", "Against", "IsRunning" });

            migrationBuilder.CreateIndex(
                name: "ix_acc_financial_years_branch_closed",
                schema: "mfi",
                table: "acc_financial_years",
                columns: new[] { "BranchId", "Closed" });

            migrationBuilder.CreateIndex(
                name: "ux_acc_folio_types_code",
                schema: "mfi",
                table: "acc_folio_types",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_folios_FolioTypeId",
                schema: "mfi",
                table: "acc_folios",
                column: "FolioTypeId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_folios_code",
                schema: "mfi",
                table: "acc_folios",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_general_ledger_TaxGroupId",
                schema: "mfi",
                table: "acc_general_ledger",
                column: "TaxGroupId");

            migrationBuilder.CreateIndex(
                name: "ix_acc_gl_account_date",
                schema: "mfi",
                table: "acc_general_ledger",
                columns: new[] { "LedgerAccountId", "PostedOn" });

            migrationBuilder.CreateIndex(
                name: "ix_acc_gl_ledger_account",
                schema: "mfi",
                table: "acc_general_ledger",
                column: "LedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "ix_acc_gl_period",
                schema: "mfi",
                table: "acc_general_ledger",
                column: "MonthlyClosureId");

            migrationBuilder.CreateIndex(
                name: "ix_acc_gl_posted_on",
                schema: "mfi",
                table: "acc_general_ledger",
                column: "PostedOn");

            migrationBuilder.CreateIndex(
                name: "ix_acc_gl_txn_code",
                schema: "mfi",
                table: "acc_general_ledger",
                column: "TransactionCode");

            migrationBuilder.CreateIndex(
                name: "ux_acc_gen_posting_groups_code",
                schema: "mfi",
                table: "acc_general_posting_groups",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_acc_ibans_code",
                schema: "mfi",
                table: "acc_ibans",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_journal_entries_GeneralLedgerEntryId",
                schema: "mfi",
                table: "acc_journal_entries",
                column: "GeneralLedgerEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_journal_entries_GeneralLedgerEntryId1",
                schema: "mfi",
                table: "acc_journal_entries",
                column: "GeneralLedgerEntryId1");

            migrationBuilder.CreateIndex(
                name: "IX_acc_journal_entries_JournalTypeId",
                schema: "mfi",
                table: "acc_journal_entries",
                column: "JournalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_journal_entries_MonthlyClosureId",
                schema: "mfi",
                table: "acc_journal_entries",
                column: "MonthlyClosureId");

            migrationBuilder.CreateIndex(
                name: "ix_acc_journal_entries_posted_on",
                schema: "mfi",
                table: "acc_journal_entries",
                column: "PostedOn");

            migrationBuilder.CreateIndex(
                name: "ix_acc_journal_entries_status",
                schema: "mfi",
                table: "acc_journal_entries",
                columns: new[] { "UnPosted", "Approved" });

            migrationBuilder.CreateIndex(
                name: "ix_acc_journal_entries_txn",
                schema: "mfi",
                table: "acc_journal_entries",
                column: "TransactionCode");

            migrationBuilder.CreateIndex(
                name: "IX_acc_journal_type_tax_group_TaxGroupId",
                schema: "mfi",
                table: "acc_journal_type_tax_group",
                column: "TaxGroupId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_journal_type_tax_group",
                schema: "mfi",
                table: "acc_journal_type_tax_group",
                columns: new[] { "JournalTypeId", "TaxGroupId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_journal_types_BranchPostingGroupId",
                schema: "mfi",
                table: "acc_journal_types",
                column: "BranchPostingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_journal_types_BusinessPostingGroupId",
                schema: "mfi",
                table: "acc_journal_types",
                column: "BusinessPostingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_journal_types_GeneralPostingGroupId",
                schema: "mfi",
                table: "acc_journal_types",
                column: "GeneralPostingGroupId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_journal_types_name",
                schema: "mfi",
                table: "acc_journal_types",
                column: "JournalName",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_ledger_account_references_ReferenceId",
                schema: "mfi",
                table: "acc_ledger_account_references",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_ledger_refs_account_ref",
                schema: "mfi",
                table: "acc_ledger_account_references",
                columns: new[] { "LedgerAccountId", "ReferenceId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ix_acc_ledger_accounts_chart",
                schema: "mfi",
                table: "acc_ledger_accounts",
                column: "AccountsChartId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_ledger_accounts_CurrencyId",
                schema: "mfi",
                table: "acc_ledger_accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_ledger_accounts_FolioId",
                schema: "mfi",
                table: "acc_ledger_accounts",
                column: "FolioId");

            migrationBuilder.CreateIndex(
                name: "ix_acc_ledger_accounts_header",
                schema: "mfi",
                table: "acc_ledger_accounts",
                column: "LedgerAccountHeaderId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_ledger_accounts_number",
                schema: "mfi",
                table: "acc_ledger_accounts",
                column: "LedgerNumber",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_acc_ledger_headers_number",
                schema: "mfi",
                table: "acc_ledger_headers",
                column: "LedgerNumber",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_ledger_recurring_items_BranchId",
                schema: "mfi",
                table: "acc_ledger_recurring_items",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_recurring_items_code",
                schema: "mfi",
                table: "acc_ledger_recurring_items",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_ledger_totals_LedgerAccountHeaderId",
                schema: "mfi",
                table: "acc_ledger_totals",
                column: "LedgerAccountHeaderId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_monthly_closures_year_month",
                schema: "mfi",
                table: "acc_monthly_closures",
                columns: new[] { "FinancialYearId", "Month" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_acc_reference_values_ref_code",
                schema: "mfi",
                table: "acc_reference_values",
                columns: new[] { "ReferenceId", "Code" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_acc_references_code",
                schema: "mfi",
                table: "acc_references",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_registration_ledger_ChargeItemId",
                schema: "mfi",
                table: "acc_registration_ledger",
                column: "ChargeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_series_numbers_BranchId",
                schema: "mfi",
                table: "acc_series_numbers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "ix_acc_series_numbers_doctype_branch_default",
                schema: "mfi",
                table: "acc_series_numbers",
                columns: new[] { "DocumentTypeId", "BranchId", "IsDefault" });

            migrationBuilder.CreateIndex(
                name: "ux_acc_swifts_code",
                schema: "mfi",
                table: "acc_swifts",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_tax_items_InsuranceProductId",
                schema: "mfi",
                table: "acc_tax_items",
                column: "InsuranceProductId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_tax_items_LoanProductId",
                schema: "mfi",
                table: "acc_tax_items",
                column: "LoanProductId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_tax_items_SavingProductId",
                schema: "mfi",
                table: "acc_tax_items",
                column: "SavingProductId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_tax_items_ShareProductId",
                schema: "mfi",
                table: "acc_tax_items",
                column: "ShareProductId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_tax_items_TaxId",
                schema: "mfi",
                table: "acc_tax_items",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_tax_items_TimedepositProductId",
                schema: "mfi",
                table: "acc_tax_items",
                column: "TimedepositProductId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_tax_items_code",
                schema: "mfi",
                table: "acc_tax_items",
                column: "ItemCode",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_taxes_TaxGroupId",
                schema: "mfi",
                table: "acc_taxes",
                column: "TaxGroupId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_taxes_code",
                schema: "mfi",
                table: "acc_taxes",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ix_acc_transaction_docs_code",
                schema: "mfi",
                table: "acc_transaction_documents",
                column: "TransactionCode");

            migrationBuilder.CreateIndex(
                name: "ix_acc_transaction_docs_number",
                schema: "mfi",
                table: "acc_transaction_documents",
                column: "DocumentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_acc_transaction_documents_DocumentTypeId",
                schema: "mfi",
                table: "acc_transaction_documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_vendor_references_ReferenceValueId",
                schema: "mfi",
                table: "acc_vendor_references",
                column: "ReferenceValueId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_vendor_reference_values",
                schema: "mfi",
                table: "acc_vendor_references",
                columns: new[] { "VendorId", "ReferenceValueId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_vendor_taxes_TaxId",
                schema: "mfi",
                table: "acc_vendor_taxes",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_vendor_taxes",
                schema: "mfi",
                table: "acc_vendor_taxes",
                columns: new[] { "VendorId", "TaxId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_acc_voucher_lines_GeneralLedgerEntryId",
                schema: "mfi",
                table: "acc_voucher_lines",
                column: "GeneralLedgerEntryId");

            migrationBuilder.CreateIndex(
                name: "ix_acc_voucher_lines_posted_on",
                schema: "mfi",
                table: "acc_voucher_lines",
                column: "PostedOn");

            migrationBuilder.CreateIndex(
                name: "IX_acc_voucher_lines_TransactionDocumentId",
                schema: "mfi",
                table: "acc_voucher_lines",
                column: "TransactionDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_voucher_lines_TransactionDocumentId1",
                schema: "mfi",
                table: "acc_voucher_lines",
                column: "TransactionDocumentId1");

            migrationBuilder.CreateIndex(
                name: "IX_acc_voucher_lines_TransactionDocumentTypeId",
                schema: "mfi",
                table: "acc_voucher_lines",
                column: "TransactionDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "ix_acc_voucher_lines_txn",
                schema: "mfi",
                table: "acc_voucher_lines",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_voucher_lines_VoucherTypeId",
                schema: "mfi",
                table: "acc_voucher_lines",
                column: "VoucherTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_voucher_types_BranchPostingGroupId",
                schema: "mfi",
                table: "acc_voucher_types",
                column: "BranchPostingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_voucher_types_BusinessPostingGroupId",
                schema: "mfi",
                table: "acc_voucher_types",
                column: "BusinessPostingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_acc_voucher_types_GeneralPostingGroupId",
                schema: "mfi",
                table: "acc_voucher_types",
                column: "GeneralPostingGroupId");

            migrationBuilder.CreateIndex(
                name: "ux_acc_voucher_types_name",
                schema: "mfi",
                table: "acc_voucher_types",
                column: "VoucherName",
                unique: true,
                filter: "\"IsDeleted\" = false");

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
                name: "IX_bnk_teller_acc_LegderAccountId",
                schema: "mfi",
                table: "bnk_teller_acc",
                column: "LegderAccountId");

            migrationBuilder.CreateIndex(
                name: "ux_bnk_teller_ledger",
                schema: "mfi",
                table: "bnk_teller_acc",
                columns: new[] { "TellerId", "LegderAccountId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

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
                name: "IX_branch_references_BranchId",
                schema: "mfi",
                table: "branch_references",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_branch_references_ReferenceValueId",
                schema: "mfi",
                table: "branch_references",
                column: "ReferenceValueId");

            migrationBuilder.CreateIndex(
                name: "IX_branches_AccountsChartId",
                schema: "mfi",
                table: "branches",
                column: "AccountsChartId");

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
                name: "IX_business_contracts_VendorId",
                schema: "mfi",
                table: "business_contracts",
                column: "VendorId");

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
                name: "IX_delivery_defaults_VendorId",
                schema: "mfi",
                table: "delivery_defaults",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "ux_discount_groups",
                schema: "mfi",
                table: "discount_groups",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProduct_ChargeGroupId",
                schema: "mfi",
                table: "InsuranceProduct",
                column: "ChargeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProduct_ProductTypeId",
                schema: "mfi",
                table: "InsuranceProduct",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProductChargeItem_ChargeItemId",
                schema: "mfi",
                table: "InsuranceProductChargeItem",
                column: "ChargeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProductChargeItem_InsuranceProductId",
                schema: "mfi",
                table: "InsuranceProductChargeItem",
                column: "InsuranceProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProductTaxGroup_ProductId",
                schema: "mfi",
                table: "InsuranceProductTaxGroup",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProductTaxGroup_TaxGroupId",
                schema: "mfi",
                table: "InsuranceProductTaxGroup",
                column: "TaxGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_invoicing_defaults_VendorId",
                schema: "mfi",
                table: "invoicing_defaults",
                column: "VendorId");

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
                name: "ix_lnr_charge_stages_charge_item",
                schema: "mfi",
                table: "lnr_charge_stages",
                columns: new[] { "ChargeItemId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_lnr_charge_stages_InsuranceProductId",
                schema: "mfi",
                table: "lnr_charge_stages",
                column: "InsuranceProductId");

            migrationBuilder.CreateIndex(
                name: "ix_lnr_charge_stages_loan_product",
                schema: "mfi",
                table: "lnr_charge_stages",
                columns: new[] { "LoanProductId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_lnr_charge_stages_SavingProductId",
                schema: "mfi",
                table: "lnr_charge_stages",
                column: "SavingProductId");

            migrationBuilder.CreateIndex(
                name: "IX_lnr_charge_stages_ShareProductId",
                schema: "mfi",
                table: "lnr_charge_stages",
                column: "ShareProductId");

            migrationBuilder.CreateIndex(
                name: "IX_lnr_charge_stages_TimedepositProductId",
                schema: "mfi",
                table: "lnr_charge_stages",
                column: "TimedepositProductId");

            migrationBuilder.CreateIndex(
                name: "IX_lnr_loan_officer_AppUserId",
                schema: "mfi",
                table: "lnr_loan_officer",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_lnr_loan_officer_AppUserId1",
                schema: "mfi",
                table: "lnr_loan_officer",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "ix_loan_officer_code",
                schema: "mfi",
                table: "lnr_loan_officer",
                column: "LoanOfficerCode");

            migrationBuilder.CreateIndex(
                name: "IX_lnr_loan_officer_acc_LegderAccountId",
                schema: "mfi",
                table: "lnr_loan_officer_acc",
                column: "LegderAccountId");

            migrationBuilder.CreateIndex(
                name: "ux_lnr_loan_officer_ledger",
                schema: "mfi",
                table: "lnr_loan_officer_acc",
                columns: new[] { "LoanOfficerId", "LegderAccountId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_ChargeGroupId",
                schema: "mfi",
                table: "LoanProduct",
                column: "ChargeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_FundId",
                schema: "mfi",
                table: "LoanProduct",
                column: "FundId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProduct_ProductTypeId",
                schema: "mfi",
                table: "LoanProduct",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProductChargeItem_ChargeItemId",
                schema: "mfi",
                table: "LoanProductChargeItem",
                column: "ChargeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProductChargeItem_LoanProductId",
                schema: "mfi",
                table: "LoanProductChargeItem",
                column: "LoanProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProductTaxGroup_ProductId",
                schema: "mfi",
                table: "LoanProductTaxGroup",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanProductTaxGroup_TaxGroupId",
                schema: "mfi",
                table: "LoanProductTaxGroup",
                column: "TaxGroupId");

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
                name: "IX_payment_defaults_BankAccountId",
                schema: "mfi",
                table: "payment_defaults",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_defaults_PaymentTermId",
                schema: "mfi",
                table: "payment_defaults",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_defaults_VendorId",
                schema: "mfi",
                table: "payment_defaults",
                column: "VendorId");

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
                name: "ux_price_groups",
                schema: "mfi",
                table: "price_groups",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_defaults_VendorId",
                schema: "mfi",
                table: "purchase_defaults",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_defaults_BankAccountId",
                schema: "mfi",
                table: "purchase_order_defaults",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_defaults_DiscountGroupId",
                schema: "mfi",
                table: "purchase_order_defaults",
                column: "DiscountGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_defaults_PriceGroupId",
                schema: "mfi",
                table: "purchase_order_defaults",
                column: "PriceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_defaults_PurchaseOrderClassificationId",
                schema: "mfi",
                table: "purchase_order_defaults",
                column: "PurchaseOrderClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_defaults_VendorGroupId",
                schema: "mfi",
                table: "purchase_order_defaults",
                column: "VendorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_defaults_VendorId",
                schema: "mfi",
                table: "purchase_order_defaults",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_defaults_VendorItemGroupId",
                schema: "mfi",
                table: "purchase_order_defaults",
                column: "VendorItemGroupId");

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
                name: "IX_RevolvingFund_CurrencyId",
                schema: "mfi",
                table: "RevolvingFund",
                column: "CurrencyId");

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
                name: "IX_SavingProduct_ChargeGroupId",
                schema: "mfi",
                table: "SavingProduct",
                column: "ChargeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingProduct_ProductTypeId",
                schema: "mfi",
                table: "SavingProduct",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingProductChargeItem_ChargeItemId",
                schema: "mfi",
                table: "SavingProductChargeItem",
                column: "ChargeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingProductChargeItem_SavingProductId",
                schema: "mfi",
                table: "SavingProductChargeItem",
                column: "SavingProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingProductTaxGroup_SavingProductId",
                schema: "mfi",
                table: "SavingProductTaxGroup",
                column: "SavingProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingProductTaxGroup_TaxGroupId",
                schema: "mfi",
                table: "SavingProductTaxGroup",
                column: "TaxGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareProduct_ChargeGroupId",
                schema: "mfi",
                table: "ShareProduct",
                column: "ChargeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareProduct_ProductTypeId",
                schema: "mfi",
                table: "ShareProduct",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareProductChargeItem_ChargeItemId",
                schema: "mfi",
                table: "ShareProductChargeItem",
                column: "ChargeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareProductChargeItem_ShareProductId",
                schema: "mfi",
                table: "ShareProductChargeItem",
                column: "ShareProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareProductTaxGroup_ProductId",
                schema: "mfi",
                table: "ShareProductTaxGroup",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareProductTaxGroup_TaxGroupId",
                schema: "mfi",
                table: "ShareProductTaxGroup",
                column: "TaxGroupId");

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
                name: "ix_teller_code",
                schema: "mfi",
                table: "tellers",
                column: "TellerCode");

            migrationBuilder.CreateIndex(
                name: "IX_tellers_AppUserId",
                schema: "mfi",
                table: "tellers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimedepositProduct_ChargeGroupId",
                schema: "mfi",
                table: "TimedepositProduct",
                column: "ChargeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TimedepositProduct_ProductTypeId",
                schema: "mfi",
                table: "TimedepositProduct",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimedepositProductChargeItem_ChargeItemId",
                schema: "mfi",
                table: "TimedepositProductChargeItem",
                column: "ChargeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TimedepositProductChargeItem_TimedepositProductId",
                schema: "mfi",
                table: "TimedepositProductChargeItem",
                column: "TimedepositProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TimedepositProductTaxGroup_TaxGroupId",
                schema: "mfi",
                table: "TimedepositProductTaxGroup",
                column: "TaxGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TimedepositProductTaxGroup_TimedepositProductId",
                schema: "mfi",
                table: "TimedepositProductTaxGroup",
                column: "TimedepositProductId");

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

            migrationBuilder.CreateIndex(
                name: "IX_vendor_DeliverTermsId",
                schema: "mfi",
                table: "vendor",
                column: "DeliverTermsId");

            migrationBuilder.CreateIndex(
                name: "IX_vendor_DeliveryModeId",
                schema: "mfi",
                table: "vendor",
                column: "DeliveryModeId");

            migrationBuilder.CreateIndex(
                name: "IX_vendor_VendorGroupId",
                schema: "mfi",
                table: "vendor",
                column: "VendorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_vendor_addresses_VendorId",
                schema: "mfi",
                table: "vendor_addresses",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_vendor_bank_accounts_BankAccountId",
                schema: "mfi",
                table: "vendor_bank_accounts",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "ux_vendor_bank_accounts",
                schema: "mfi",
                table: "vendor_bank_accounts",
                columns: new[] { "VendorId", "BankAccountId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_vendor_delivery_terms",
                schema: "mfi",
                table: "vendor_delivery_terms",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_vendor_group_code",
                schema: "mfi",
                table: "vendor_groups",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "ux_vendor_item_groups",
                schema: "mfi",
                table: "vendor_item_groups",
                column: "Code",
                unique: true,
                filter: "\"IsDeleted\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "acc_bank_account_currencies",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_branch_ledger",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_branch_revenue_center",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_card_ledger",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cashier_accounts",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cashier_branch_access",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cashier_journal_types",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cashier_voucher_types",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_charge_group_items",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_charge_item_charges",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_charge_ledger_entry",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cheque_bank_ledger_links",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cost_center",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_denominations",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_exchange_rates",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_journal_entries",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_journal_type_tax_group",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_ledger_account_references",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_ledger_recurring_items",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_ledger_totals",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_registration_ledger",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_series_numbers",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_tax_items",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_vendor_references",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_vendor_taxes",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_voucher_lines",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "audit_logs",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "bnk_teller_acc",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "branch_holidays",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "branch_references",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "business_contracts",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "delivery_defaults",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "InsuranceProductChargeItem",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "InsuranceProductTaxGroup",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "invoicing_defaults",
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
                name: "lnr_charge_stages",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "lnr_loan_officer_acc",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "LoanProductChargeItem",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "LoanProductTaxGroup",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "password_history",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "payment_defaults",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "purchase_defaults",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "purchase_order_defaults",
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
                name: "SavingProductChargeItem",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "SavingProductTaxGroup",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "ShareProductChargeItem",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "ShareProductTaxGroup",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "system_configs",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "TimedepositProductChargeItem",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "TimedepositProductTaxGroup",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "user_branch_access",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "vendor_addresses",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "vendor_bank_accounts",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cards",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cash_accounts",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cashiers",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_charges",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_bank_ledger",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cheque_ledger",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_journal_types",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_general_ledger",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_voucher_types",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "tellers",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_reference_values",
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
                name: "InsuranceProduct",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "lnr_loan_officer",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "LoanProduct",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "payment_terms",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "discount_groups",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "price_groups",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "purchase_order_classifications",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "vendor_item_groups",
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
                name: "ShareProduct",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_charge_items",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "TimedepositProduct",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "vendor",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_transaction_documents",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cheques",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_monthly_closures",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_branch_posting_groups",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_business_posting_groups",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_general_posting_groups",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_references",
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
                name: "users",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "RevolvingFund",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "SavingProduct",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_taxes",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "delivery_mode",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "vendor_delivery_terms",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "vendor_groups",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_document_types",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_cheque_books",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_financial_years",
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
                name: "ProductType",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_charge_groups",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "TaxGroups",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_bank_accounts",
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
                name: "acc_bank_branches",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_ledger_accounts",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "customers_base",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_banks",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_currencies",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_folios",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_ledger_headers",
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
                name: "acc_ibans",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_swifts",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_folio_types",
                schema: "mfi");

            migrationBuilder.DropTable(
                name: "acc_charts",
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
