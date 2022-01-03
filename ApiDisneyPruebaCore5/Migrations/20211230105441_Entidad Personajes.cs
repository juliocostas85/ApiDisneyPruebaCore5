using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDisneyPruebaCore5.Migrations
{
    public partial class EntidadPersonajes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personaje",
                columns: table => new
                {
                    PersonajeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen = table.Column<byte[]>(type: "image", nullable: true),
                    Peso = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: true),
                    Historia = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personaje", x => x.PersonajeId);
                });

            migrationBuilder.CreateTable(
                name: "PeliculaSeriePersonaje",
                columns: table => new
                {
                    PeliculasSeriesPeliculaSerieId = table.Column<int>(type: "int", nullable: false),
                    PersonajesPersonajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaSeriePersonaje", x => new { x.PeliculasSeriesPeliculaSerieId, x.PersonajesPersonajeId });
                    table.ForeignKey(
                        name: "FK_PeliculaSeriePersonaje_PeliculasSeries_PeliculasSeriesPeliculaSerieId",
                        column: x => x.PeliculasSeriesPeliculaSerieId,
                        principalTable: "PeliculasSeries",
                        principalColumn: "PeliculaSerieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaSeriePersonaje_Personaje_PersonajesPersonajeId",
                        column: x => x.PersonajesPersonajeId,
                        principalTable: "Personaje",
                        principalColumn: "PersonajeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSeriePersonaje_PersonajesPersonajeId",
                table: "PeliculaSeriePersonaje",
                column: "PersonajesPersonajeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculaSeriePersonaje");

            migrationBuilder.DropTable(
                name: "Personaje");
        }
    }
}
