using System;

namespace ApiDisneyPruebaCore5.Models
{
    public class RespuestaAutenticacion
    {
        public string Token { get; set; }
        public DateTime? FechaExpiracion { get; set; }
    }
}
