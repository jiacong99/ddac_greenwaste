using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_Assignment.Migrations.DDAC_
{
    public partial class addBookingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingName",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "BookingType",
                table: "Booking",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "Booking",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BookingLocation",
                table: "Booking",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookingStatus",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverID",
                table: "Booking",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingLocation",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingStatus",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "BookingType",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "BookingName",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
