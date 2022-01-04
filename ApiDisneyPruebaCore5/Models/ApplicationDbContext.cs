

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PeliculaSeriePersonaje>()
                 .HasKey(pp => new { pp.PeliculaSerieId, pp.PersonajeId });
        }


        public DbSet<Genero> Generos { get; set; }
        public DbSet<PeliculaSerie> PeliculasSeries { get; set; }
        public DbSet<ApiDisneyPruebaCore5.Models.Personaje> Personaje { get; set; }

        public DbSet<PeliculaSeriePersonaje> PeliculasSeriesPersonajes { get; set; }
    }
}
