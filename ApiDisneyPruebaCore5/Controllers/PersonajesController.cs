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
using AutoMapper;
using ApiDisneyPruebaCore5.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using ApiDisneyPruebaCore5.Tools;
using ApiDisneyPruebaCore5.Helpers;
using ApiDisneyPruebaCore5.Filters;
using ApiDisneyPruebaCore5.Services;
using ApiDisneyPruebaCore5.Wrappers;

namespace ApiDisneyPruebaCore5.Controllers
{
    [Produces("application/json")]
    [Route("characters")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonajesController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;
        private readonly IUriService uriService;
        
        public PersonajesController(ApplicationDbContext context, IMapper mapper, IUriService uriService) : base(context, mapper, uriService)
        {
            this._context = context;
            this.mapper = mapper;
            this.uriService = uriService;
        }
        // GET: api/Personajes



        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<PersonajesGet>>>> BuscarPersonaje([FromQuery] Filtros filtros)
        {
            var personajesQueryable =_context.Personaje.AsQueryable();

            List<Personaje> lst = new List<Personaje>();
         
            if (filtros.name != "")
            {
       
                personajesQueryable.Where(x => x.Nombre.Contains(filtros.name));
               

            }
            if (filtros.weigth >= 0)
            {
           
                personajesQueryable.Where(x => x.Peso == filtros.weigth);
              
            }
            if (filtros.age > -1)
            {
   
                personajesQueryable.Where(x => x.Edad == filtros.age);
              
            }
            if (filtros.movies >= 0)
            {
            
                lst = personajesQueryable.ToList();
                lst.AddRange(await _context.Personaje.FromSqlRaw("select p.* from Personaje p where p.PersonajeId IN (select psp.PersonajesPersonajeId from PeliculaSeriePersonaje psp where psp.PeliculasSeriesPeliculaSerieId = '"+filtros.movies+"')").ToListAsync());
                personajesQueryable = lst.AsQueryable();
            }

            //sobre el total
            var totalRecords = await personajesQueryable.CountAsync();

            await HttpContext.InsertarParametrosPaginacion(personajesQueryable,
                filtros.CantidadRegistrosPorPagina);

            var personajes = await personajesQueryable.Paginar(filtros.Paginacion).ToListAsync();
            var personajesGet = mapper.Map<List<PersonajesGet>>(personajes);

            //se prepara la respuesta
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filtros.Pagina, filtros.CantidadRegistrosPorPagina);
            var pagedReponse = PaginationHelper.CreatePagedReponse<PersonajesGet>(personajesGet, validFilter, totalRecords, uriService, route);
           
            return pagedReponse;
          

        }

        // GET: api/Personajes/5


        [HttpGet("{Id:int}", Name = "personajeCreado")]
        public async Task<ActionResult<PersonajesDTOConPeliculas>> Detalle(int Id)
        {
            var personaje = await _context.Personaje.Include(x => x.PeliculasSeriesPersonajes).ThenInclude(p => p.PeliculaSerie).FirstOrDefaultAsync(x => x.Id == Id);
            
            if (personaje == null)
            {
                return NotFound();
            }


            return mapper.Map<PersonajesDTOConPeliculas>(personaje);
        }


        // PUT: api/Personajes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutPersonaje(int id, PersonajeModificacionDTO personajeDTO)
        {
            return await Put<PersonajeModificacionDTO, Personaje>(id, personajeDTO);
        }

        // POST: api/Personajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPersonaje(PersonajeCreacionDTO personaje)
        {
            if (ModelState.IsValid)
            {
                return await
                Post<PersonajeCreacionDTO, Personaje, PersonajeDTO>(personaje, "personajeCreado");

            }
            else
            {
                return BadRequest(ModelState);
            }



        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchPersonaje(int id, [FromBody]JsonPatchDocument<PersonajePatchDTO> jsonPatchDocument)
        {
            if(jsonPatchDocument == null)
            {
                return BadRequest();
            }

            var personajeDB = await _context.Personaje.FirstOrDefaultAsync(X => X.Id == id);
            if(personajeDB == null)
            {
                return NotFound();
            }

            var personajeDTO = mapper.Map<PersonajePatchDTO>(personajeDB);
            jsonPatchDocument.ApplyTo(personajeDTO, ModelState);

            var esValido = TryValidateModel(personajeDTO);
            if (!esValido)
            {
                return BadRequest(ModelState);
            }


            mapper.Map(personajeDTO, personajeDB);
            await _context.SaveChangesAsync();
            return NoContent();


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
            return _context.Personaje.Any(e => e.Id == id);
        }
    }
}
