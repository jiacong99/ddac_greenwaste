using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_Assignment.Migrations
{
    public partial class addUser_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_Role",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_Role",
                table: "AspNetUsers");
        }
    }
}
