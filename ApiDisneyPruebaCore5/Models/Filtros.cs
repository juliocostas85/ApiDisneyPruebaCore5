using ApiDisneyPruebaCore5.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDisneyPruebaCore5.Models
{
    public class Filtros
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosPorPagina { get; set; } = 10;
        public PaginacionDTO Paginacion
        {
            get { return new PaginacionDTO() { Pagina = Pagina, CantidadRegistrosPorPagina = CantidadRegistrosPorPagina }; }
        }
        public string name { get; set; } = "";
        public string order { get; set; } = "";

        public int? genre { get; set; }
        public int? age { get; set; }

        public int? weigth { get; set; }

        public int? movies { get; set; }
    }
}
