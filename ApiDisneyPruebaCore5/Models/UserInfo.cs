using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDisneyPruebaCore5.Models
{
    public class UserInfo
    {
        [Required(ErrorMessage ="El Email es requerido.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="El password es requerido.")]
        public string Password { get; set; }
    }
}
