using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_Assignment.Migrations.DDAC_
{
    public partial class data_context_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "Booking");

            migrationBuilder.AlterColumn<decimal>(
                name: "serviceAmount",
                table: "WasteServices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "Booking",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "Booking");

            migrationBuilder.AlterColumn<float>(
                name: "serviceAmount",
                table: "WasteServices",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "DriverID",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
