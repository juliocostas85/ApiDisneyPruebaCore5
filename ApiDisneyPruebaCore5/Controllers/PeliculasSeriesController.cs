﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDisneyPruebaCore5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiDisneyPruebaCore5.Controllers
{
    [Produces("application/json")]
    [Route("movies")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PeliculasSeriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PeliculasSeriesController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
   
        public async Task<IActionResult> Get([FromQuery]Filtros filtros)
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

            var lstResult =  (from d in lst
                                   select new
                                   {
                                       Imagen = d.Imagen,
                                       Titulo = d.Titulo,
                                       FechaCreacion = d.FechaCreacion
                                   });

            return Ok(lstResult);
          
        }



        
        [HttpGet("{idpeliculaserie:int}", Name = "peliculaCreada")]
        public async Task<ActionResult<PeliculaSerie>> Detalle(int idpeliculaserie)
        {
            PeliculaSerie peliculaseries = await _context.PeliculasSeries.Include(x => x.Personajes).FirstOrDefaultAsync(x => x.PeliculaSerieId == idpeliculaserie);

            if (peliculaseries == null)
            {
                return NotFound();
            }

            return peliculaseries;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutPeliculaSerie(int id, PeliculaSerie peliculaSerie)
        {
            if (id != peliculaSerie.PeliculaSerieId)
            {
                return BadRequest();
            }

            var existe = await _context.PeliculasSeries.AnyAsync(x => x.PeliculaSerieId == id);
            if (!existe)
            {
                return NotFound();
            }

            _context.Entry(peliculaSerie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaSerieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<PeliculaSerie>> PostPeliculaSerie(PeliculaSerie peliculaSerie)
        {

            var existe = await _context.Generos.AnyAsync(x => x.GeneroId == peliculaSerie.GeneroId);
            if (!existe)
            {
                return BadRequest("No existe el genero.");
            }


            if (ModelState.IsValid)
            {
                _context.PeliculasSeries.Add(peliculaSerie);
                await _context.SaveChangesAsync();

                return new CreatedAtRouteResult("peliculaCreada", new { idpeliculaserie = peliculaSerie.PeliculaSerieId }, peliculaSerie);
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