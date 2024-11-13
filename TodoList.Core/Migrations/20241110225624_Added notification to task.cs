using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.Core.Migrations
{
    public partial class Addednotificationtotask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AddNotification",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddNotification",
                table: "Tasks");
        }
    }
}
