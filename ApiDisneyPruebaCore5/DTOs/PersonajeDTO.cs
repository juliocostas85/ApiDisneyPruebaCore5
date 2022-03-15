using ApiDisneyPruebaCore5.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiDisneyPruebaCore5.DTOs
{
    public class PersonajeDTO:PersonajePatchDTO
    {
        public int Id { get; set; }

       

       

        public byte[] Imagen { get; set; }



       



        public int? Edad { get; set; }
      

        public string Historia { get; set; }
      

    }
}
