using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contactos_moviles.Migrations
{
    public partial class AgregarDireccionSoloColumna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Contactos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Contactos");
        }
    }
}

