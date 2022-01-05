using ApiDisneyPruebaCore5.Models;
using System.Collections.Generic;

namespace ApiDisneyPruebaCore5.DTOs
{
    public class PersonajesDTOConPeliculas:PersonajeDTO
    {
        public List<PeliculasSeriesDTO> PeliculasSeries { get; set; }

    }
}
