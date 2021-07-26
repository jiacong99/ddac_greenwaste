using Microsoft.EntityFrameworkCore.Migrations;

namespace DDAC_Assignment.Migrations.DDAC_
{
    public partial class services : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WasteServices",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    servicesTitle = table.Column<string>(maxLength: 100, nullable: false),
                    serviceDescription = table.Column<string>(maxLength: 100, nullable: true),
                    serviceMediaURL = table.Column<string>(nullable: true),
                    serviceSize = table.Column<string>(maxLength: 100, nullable: false),
                    serviceLimit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteServices", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "WasteServices");
        }
    }
}
