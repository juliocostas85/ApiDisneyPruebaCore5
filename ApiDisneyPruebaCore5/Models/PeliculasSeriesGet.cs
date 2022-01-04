using System;

namespace ApiDisneyPruebaCore5.DTOs
{
    public class PeliculasSeriesGet
    {
        public byte[] Imagen { get; set; }
        public string Titulo { get; set; }

        public DateTime? FechaCreacion { get; set; }
    }
}
