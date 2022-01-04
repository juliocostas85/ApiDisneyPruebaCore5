using ApiDisneyPruebaCore5.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiDisneyPruebaCore5.DTOs
{
    public class PersonajeDTO
    {
        public int PersonajeId { get; set; }

        [Required]

        public string Nombre { get; set; }

        public byte[] Imagen { get; set; }



        public decimal? Peso { get; set; }



        public int? Edad { get; set; }


        public string Historia { get; set; }


        public List<PeliculaSeriePersonaje> PeliculasSeriesPersonajes { get; set; }
    }
}
