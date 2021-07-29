using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_Assignment.Migrations.DDAC_
{
    public partial class services_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "serviceAmount",
                table: "WasteServices",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "serviceAmount",
                table: "WasteServices");
        }
    }
}
