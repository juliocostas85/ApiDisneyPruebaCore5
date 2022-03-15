using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDisneyPruebaCore5.Filters;
namespace ApiDisneyPruebaCore5.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
