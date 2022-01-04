namespace ApiDisneyPruebaCore5.Models
{
    public class PeliculaSeriePersonaje
    {
        public int PersonajeId { get; set; }
        public int PeliculaSerieId { get; set; }

        public Personaje Personaje { get; set; }

        public PeliculaSerie PeliculaSerie { get; set; }
    }
}
