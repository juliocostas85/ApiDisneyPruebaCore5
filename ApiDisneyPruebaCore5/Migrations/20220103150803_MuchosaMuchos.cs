using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDisneyPruebaCore5.Migrations
{
    public partial class MuchosaMuchos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Personaje",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "PeliculasSeries",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PeliculasSeriesPersonajes",
                columns: table => new
                {
                    PersonajeId = table.Column<int>(type: "int", nullable: false),
                    PeliculaSerieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculasSeriesPersonajes", x => new { x.PeliculaSerieId, x.PersonajeId });
                    table.ForeignKey(
                        name: "FK_PeliculasSeriesPersonajes_PeliculasSeries_PeliculaSerieId",
                        column: x => x.PeliculaSerieId,
                        principalTable: "PeliculasSeries",
                        principalColumn: "PeliculaSerieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculasSeriesPersonajes_Personaje_PersonajeId",
                        column: x => x.PersonajeId,
                        principalTable: "Personaje",
                        principalColumn: "PersonajeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSeriesPersonajes_PersonajeId",
                table: "PeliculasSeriesPersonajes",
                column: "PersonajeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculasSeriesPersonajes");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Personaje",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "PeliculasSeries",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
