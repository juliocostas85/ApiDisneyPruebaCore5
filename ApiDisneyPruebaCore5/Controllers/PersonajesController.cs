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
using AutoMapper;
using ApiDisneyPruebaCore5.DTOs;

namespace ApiDisneyPruebaCore5.Controllers
{
    [Produces("application/json")]
    [Route("characters")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonajesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public PersonajesController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }

        // GET: api/Personajes
      


        [HttpGet]
        public async Task<ActionResult<List<PersonajesGet>>> BuscarPersonaje([FromQuery]Filtros filtros)
        {

            List<Personaje> lst = new List<Personaje>();
            bool passedFilters = false;

            if (filtros.name != "")
            {
                passedFilters = true;
                lst.AddRange(await _context.Personaje.Where(x => x.Nombre.Contains(filtros.name)).ToListAsync());

            }
            if(filtros.weigth >= 0)
            {
                passedFilters = true;
                lst.AddRange(await _context.Personaje.Where(x => x.Peso == filtros.weigth).ToListAsync());
            }
            if (filtros.age > -1)
            {
                passedFilters = true;
                lst.AddRange(await _context.Personaje.Where(x => x.Edad == filtros.age).ToListAsync());
            }
            if (filtros.movies >= 0)
            {
                passedFilters = true;
                lst.AddRange(await _context.Personaje.FromSqlRaw("select p.* from Personaje p where p.PersonajeId IN (select psp.PersonajesPersonajeId from PeliculaSeriePersonaje psp where psp.PeliculasSeriesPeliculaSerieId = '"+filtros.movies+"')").ToListAsync());

            }

            if (!passedFilters)
            {
                lst = await _context.Personaje.ToListAsync();
            }


            var lstResult =  (from d in lst
                             select new PersonajesGet
                             {
                                 Imagen = d.Imagen,
                                 Nombre = d.Nombre
                             }).ToList();


            return Ok(lstResult);
            
        }

        // GET: api/Personajes/5
       
        
        [HttpGet("{idpersonaje:int}", Name = "personajeCreado")]
        public async Task<ActionResult<PersonajesDTOConPeliculas>> Detalle(int idpersonaje)
        {
            var personaje = await _context.Personaje.Include(x => x.PeliculasSeriesPersonajes).ThenInclude(p=>p.PeliculaSerie).FirstOrDefaultAsync(x => x.PersonajeId == idpersonaje);

            if (personaje == null)
            {
                return NotFound();
            }


            return mapper.Map<PersonajesDTOConPeliculas>(personaje);
        }


        // PUT: api/Personajes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutPersonaje(int id, Personaje personaje)
        {
            if (id != personaje.PersonajeId)
            {
                return BadRequest();
            }


            var existe = await _context.Personaje.AnyAsync(x => x.PersonajeId == id);
            if (!existe)
            {
                return NotFound();
            }

            _context.Entry(personaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonajeExists(id))
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

        // POST: api/Personajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPersonaje(PersonajeCreacionDTO personaje)
        {
            if (ModelState.IsValid)
            {
                var personajeCreacion = mapper.Map<Personaje>(personaje);

                _context.Personaje.Add(personajeCreacion);
                await _context.SaveChangesAsync();

                var personajeDTO = mapper.Map<PersonajeDTO>(personajeCreacion);

                return new CreatedAtRouteResult("personajeCreado", new { idpersonaje = personajeCreacion.PersonajeId }, personajeDTO);
               
            }
            else
            {
                return BadRequest(ModelState);
            }


           
        }

        // DELETE: api/Personajes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePersonaje(int id)
        {
            var personaje = await _context.Personaje.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }

            _context.Personaje.Remove(personaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonajeExists(int id)
        {
            return _context.Personaje.Any(e => e.PersonajeId == id);
        }
    }
}
