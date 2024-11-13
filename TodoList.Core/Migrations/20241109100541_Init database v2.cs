using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.Core.Migrations
{
    public partial class Initdatabasev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Tasks",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Tasks");
        }
    }
}
