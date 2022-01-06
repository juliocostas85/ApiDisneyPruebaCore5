using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDisneyPruebaCore5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ApiDisneyPruebaCore5.DTOs;
using AutoMapper;
using System.Reflection;

namespace ApiDisneyPruebaCore5.Controllers
{
    [Produces("application/json")]
    [Route("movies")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PeliculasSeriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public PeliculasSeriesController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }

       
        [HttpGet]
   
        public async Task<ActionResult<IEnumerable<PeliculasSeriesGet>>> Get([FromQuery]Filtros filtros)
        {

            List<PeliculaSerie> lst = new List<PeliculaSerie>();
            bool passedFilters = false;
            if (filtros.name != "")
            {
                passedFilters = true;
                lst.AddRange(await _context.PeliculasSeries.Where(x => x.Titulo.Contains(filtros.name)).ToListAsync());

            }
          

            if (filtros.genre >= 0)
            {
                passedFilters = true;
                lst.AddRange(await _context.PeliculasSeries.Where(p => p.GeneroId == filtros.genre).ToListAsync());
            }

            if (filtros.order == "ASC")
            {
                passedFilters = true;
                lst =  lst.OrderBy(x => x.FechaCreacion).ToList();
            }
            if (filtros.order == "DESC")
            {
                passedFilters = true;
                lst = lst.OrderByDescending(x => x.FechaCreacion).ToList();
            }

            if (!passedFilters)
            {
                lst = await _context.PeliculasSeries.ToListAsync();
            }

            var lstResult = (from d in lst
                             select new PeliculasSeriesGet
                             {
                                 Imagen = d.Imagen,
                                 Titulo = d.Titulo,
                                 FechaCreacion = d.FechaCreacion
                             });


            return Ok(lstResult.ToList());
          
        }



        [HttpGet("{idpeliculaserie:int}", Name = "peliculaCreada")]
        public async Task<ActionResult<PeliculasSeriesDTOconPersonajes>> Detalle(int idpeliculaserie)
        {
            var peliculaseries = await _context.PeliculasSeries.Include(x => x.PeliculasSeriesPersonajes).ThenInclude(pe=>pe.Personaje).FirstOrDefaultAsync(x => x.PeliculaSerieId == idpeliculaserie);

            if (peliculaseries == null)
            {
                return NotFound();
            }

            
            return mapper.Map<PeliculasSeriesDTOconPersonajes>(peliculaseries); 
        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutPeliculaSerie(int id, PeliculasSeriesModificacionDTO peliculaSerie)
        {
           

            var ps = await _context.PeliculasSeries.FirstOrDefaultAsync(x => x.PeliculaSerieId == id);
            if (ps == null)
            {
                return NotFound();
            }

            ps = mapper.Map(peliculaSerie, ps);


            await _context.SaveChangesAsync();


            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult> PostPeliculaSerie(PeliculasSeriesCreacionDTO peliculaSerie)
        {

            var existe = await _context.Generos.AnyAsync(x => x.GeneroId == peliculaSerie.GeneroId);
            if (!existe)
            {
                return BadRequest($"No existe el genero con ID {peliculaSerie.GeneroId}.");
            }


            if (ModelState.IsValid)
            {
                var pelicula = mapper.Map<PeliculaSerie>(peliculaSerie);
                _context.PeliculasSeries.Add(pelicula);
                await _context.SaveChangesAsync();

                var peliculacreada = mapper.Map<PeliculasSeriesDTO>(pelicula);

                return new CreatedAtRouteResult("peliculaCreada", new { idpeliculaserie = pelicula.PeliculaSerieId }, peliculacreada);
             
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        // DELETE: api/PeliculasSeries/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePeliculaSerie(int id)
        {
            var peliculaSerie = await _context.PeliculasSeries.FindAsync(id);
            //var existe = await _context.PeliculasSeries.AnyAsync(x=>x.PeliculasSeriesId == id);
            if (peliculaSerie == null)
            {
                return NotFound();
            }

            _context.PeliculasSeries.Remove(peliculaSerie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeliculaSerieExists(int id)
        {
            return _context.PeliculasSeries.Any(e => e.PeliculaSerieId == id);
        }
    }
}
