using System.ComponentModel.DataAnnotations;

namespace ApiDisneyPruebaCore5.DTOs
{
    public class PersonajePatchDTO
    {
        [Required]
        [StringLength(maximumLength: 250)]
        public string Nombre { get; set; }
        public decimal? Peso { get; set; }
    }
}
