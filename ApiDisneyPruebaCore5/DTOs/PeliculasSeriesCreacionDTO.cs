using ApiDisneyPruebaCore5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiDisneyPruebaCore5.DTOs
{
    public class PeliculasSeriesCreacionDTO
    {
        

        [StringLength(maximumLength: 250)]
        public string Titulo { get; set; }

        public DateTime? FechaCreacion { get; set; } = DateTime.Now;
        public byte[] Imagen { get; set; }

      
        public int GeneroId { get; set; }

        //public List<int> PersonajesIds { get; set; }

        //[JsonIgnore]


    }
}
