using ApiDisneyPruebaCore5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiDisneyPruebaCore5.DTOs
{
    public class PeliculasSeriesDTO
    {
        public int PeliculaSerieId { get; set; }

        public string Titulo { get; set; }

      
        public byte[] Imagen { get; set; }

        public DateTime? FechaCreacion { get; set; }

      
        public int GeneroId { get; set; }

        public List<PeliculaSeriePersonaje> PeliculasSeriesPersonajes { get; set; }

    }
}
