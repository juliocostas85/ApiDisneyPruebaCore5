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


           

            CreateMap<Personaje, PersonajeDTO>();//GET



            CreateMap<PersonajeCreacionDTO, Personaje>();//POST

            CreateMap<PeliculaSerie, PeliculasSeriesDTO>();

            //CreateMap<PeliculaSerie, PeliculasSeriesDTOconPersonajes>()
            //    .ForMember(pp=>pp.Personajes, opciones=>opciones.MapFrom(MapPeliculasPersonajes));//GET




            CreateMap<PeliculasSeriesCreacionDTO, PeliculaSerie>();//post
         

         
        }

        private object MapPeliculasPersonajes(PeliculaSerie arg)
        {
            throw new NotImplementedException();
        }
    }

    
}
