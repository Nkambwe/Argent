using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Argent.Api.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_branchcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchCode",
                schema: "mfi",
                table: "branches",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchCode",
                schema: "mfi",
                table: "branches");
        }
    }
}
