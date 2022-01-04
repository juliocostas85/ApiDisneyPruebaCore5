using System.ComponentModel.DataAnnotations;

namespace ApiDisneyPruebaCore5.DTOs
{
    public class PersonajeCreacionDTO
    {

        [StringLength(maximumLength: 250)]
        public string Nombre { get; set; }

       
        public byte[] Imagen { get; set; }


        public decimal? Peso { get; set; }


        public int? Edad { get; set; }


        public string Historia { get; set; }
    }
}
