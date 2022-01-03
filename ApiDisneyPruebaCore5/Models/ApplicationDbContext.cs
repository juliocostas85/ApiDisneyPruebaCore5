

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDisneyPruebaCore5.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ApiDisneyPruebaCore5.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            // ...
        }


        public DbSet<Genero> Generos { get; set; }
        public DbSet<PeliculaSerie> PeliculasSeries { get; set; }
        public DbSet<ApiDisneyPruebaCore5.Models.Personaje> Personaje { get; set; }
    }
}
