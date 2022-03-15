using ApiDisneyPruebaCore5.DTOs;
using ApiDisneyPruebaCore5.Models;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace ApiDisneyPruebaCore5.Tools
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {


            CreateMap<Personaje, PersonajeDTO>();

            CreateMap<Personaje, PersonajesDTOConPeliculas>()
                .ForMember(x=>x.PeliculasSeries, opciones=>opciones.MapFrom(MapPersonajesPeliculas));//GET



            CreateMap<PersonajeCreacionDTO, Personaje>();//POST

            CreateMap<PeliculaSerie, PeliculasSeriesDTO>();
            CreateMap<PeliculaSerie, PeliculasSeriesDTOconPersonajes>()
                .ForMember(pp => pp.Personajes, opciones => opciones.MapFrom(MapPeliculasPersonajes));//GET




            CreateMap<PeliculasSeriesCreacionDTO, PeliculaSerie>();//post
            CreateMap<PeliculasSeriesModificacionDTO, PeliculaSerie>();//put
            CreateMap<PersonajeModificacionDTO, Personaje>().ReverseMap();//put


            CreateMap<PersonajePatchDTO, Personaje>().ReverseMap();
            CreateMap<Personaje, PersonajesGet>();

        }

        private List<PersonajeDTO> MapPeliculasPersonajes(PeliculaSerie pelicula, PeliculasSeriesDTOconPersonajes peliculaspersonajes)
        {
            var resultado = new List<PersonajeDTO>();

            if (pelicula.PeliculasSeriesPersonajes == null) { return resultado; }

            foreach (var personaje in pelicula.PeliculasSeriesPersonajes)
            {
                resultado.Add(new PersonajeDTO()
                {
                    Id = personaje.PersonajeId,
                    Edad = personaje.Personaje.Edad,
                    Historia = personaje.Personaje.Historia,
                    Imagen = personaje.Personaje.Imagen,
                    Nombre = personaje.Personaje.Nombre,
                    Peso = personaje.Personaje.Peso

                    
                });
            }

            return resultado;
        }



        private List<PeliculasSeriesDTO> MapPersonajesPeliculas(Personaje personaje, PersonajesDTOConPeliculas personajespeliculas)
        {
            var resultado = new List<PeliculasSeriesDTO>();

            if(personaje.PeliculasSeriesPersonajes == null) { return resultado; }

            foreach (var pelicula in personaje.PeliculasSeriesPersonajes)
            {
                resultado.Add(new PeliculasSeriesDTO()
                {
                 FechaCreacion = pelicula.PeliculaSerie.FechaCreacion,
                 GeneroId = pelicula.PeliculaSerie.GeneroId,
                 Imagen = pelicula.PeliculaSerie.Imagen,
                 PeliculaSerieId = pelicula.PeliculaSerieId,
                 Titulo = pelicula.PeliculaSerie.Titulo

                });
            }

            return resultado;
        }


    }

    
}
