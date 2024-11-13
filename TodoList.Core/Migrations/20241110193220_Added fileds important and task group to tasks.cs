using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.Core.Migrations
{
    public partial class Addedfiledsimportantandtaskgrouptotasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Tasks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Important",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Important",
                table: "Tasks");
        }
    }
}
