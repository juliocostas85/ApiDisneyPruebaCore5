using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiDisneyPruebaCore5.Models
{
    public class Genero
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeneroId { get; set; }

        [StringLength(maximumLength:120)]
        public string Nombre { get; set; }

        [Column(TypeName = "image")]
        public byte[] Imagen { get; set; }

        //[JsonIgnore]
        public List<PeliculaSerie> PeliculasSeries { get; set; }
    }
}
