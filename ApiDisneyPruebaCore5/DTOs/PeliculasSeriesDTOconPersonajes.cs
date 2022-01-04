using System.Collections.Generic;

namespace ApiDisneyPruebaCore5.DTOs
{
    public class PeliculasSeriesDTOconPersonajes: PeliculasSeriesDTO
    {
        public List<PersonajeDTO> Personajes { get; set; }
    }
}
