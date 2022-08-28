using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "project");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "project",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "project",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "project",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "project",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project",
                table: "project",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_project",
                table: "project");

            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "project");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "project");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "project");

            migrationBuilder.RenameTable(
                name: "project",
                newName: "user");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "user",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "Id");
        }
    }
}
