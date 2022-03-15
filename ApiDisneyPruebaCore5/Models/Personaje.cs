using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDisneyPruebaCore5.Models
{
    public class Personaje:IId
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 250)]
        public string Nombre { get; set; }

        [Column(TypeName = "image")]

        public byte[] Imagen { get; set; }

      

        public decimal? Peso { get; set; }

     

        public int? Edad { get; set; }


        [Column(TypeName = "text")]

        public string Historia { get; set; }

        // [JsonIgnore]
        public List<PeliculaSeriePersonaje> PeliculasSeriesPersonajes { get; set; }

    }
}
