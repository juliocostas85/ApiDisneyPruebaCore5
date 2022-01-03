using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDisneyPruebaCore5.Migrations
{
    public partial class CampoNombre_Genero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Generos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Generos");
        }
    }
}
