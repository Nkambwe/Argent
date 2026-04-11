using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Argent.Api.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_Access_objects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roles_RoleGroup_RoleGroupId",
                schema: "mfi",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleGroup",
                schema: "mfi",
                table: "RoleGroup");

            migrationBuilder.RenameTable(
                name: "RoleGroup",
                schema: "mfi",
                newName: "role_group",
                newSchema: "mfi");

            migrationBuilder.RenameColumn(
                name: "LastLoginAt",
                schema: "mfi",
                table: "users",
                newName: "LastLoginOn");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                schema: "mfi",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "mfi",
                table: "role_group",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "mfi",
                table: "role_group",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "mfi",
                table: "role_group",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                schema: "mfi",
                table: "role_group",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "mfi",
                table: "role_group",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_group",
                schema: "mfi",
                table: "role_group",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_roles_role_group_RoleGroupId",
                schema: "mfi",
                table: "roles",
                column: "RoleGroupId",
                principalSchema: "mfi",
                principalTable: "role_group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roles_role_group_RoleGroupId",
                schema: "mfi",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_group",
                schema: "mfi",
                table: "role_group");

            migrationBuilder.RenameTable(
                name: "role_group",
                schema: "mfi",
                newName: "RoleGroup",
                newSchema: "mfi");

            migrationBuilder.RenameColumn(
                name: "LastLoginOn",
                schema: "mfi",
                table: "users",
                newName: "LastLoginAt");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                schema: "mfi",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "mfi",
                table: "RoleGroup",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "mfi",
                table: "RoleGroup",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "mfi",
                table: "RoleGroup",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                schema: "mfi",
                table: "RoleGroup",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "mfi",
                table: "RoleGroup",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleGroup",
                schema: "mfi",
                table: "RoleGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_roles_RoleGroup_RoleGroupId",
                schema: "mfi",
                table: "roles",
                column: "RoleGroupId",
                principalSchema: "mfi",
                principalTable: "RoleGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
