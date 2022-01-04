using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDisneyPruebaCore5.Migrations
{
    public partial class Prueba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculaSeriePersonaje");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
