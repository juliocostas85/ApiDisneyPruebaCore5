using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDisneyPruebaCore5.Models
{
    public class PeliculaSerie
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PeliculaSerieId { get; set; }

        public string Titulo { get; set; }

        [Column(TypeName = "image")]
        public byte[] Imagen { get; set; }

        public DateTime? FechaCreacion { get; set; }

        [ForeignKey("Fk_Genero")]
        public int GeneroId { get; set; }

        [JsonIgnore]
        public Genero FK_Genero { get; set; }

        //[JsonIgnore]
        public List<Personaje> Personajes { get; set; }

    }
}
