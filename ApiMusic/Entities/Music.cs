using System;

namespace ApiMusic.Entities
{
    public class Music
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Artista { get; set; }
        public bool Premium { get; set; }
    }
}
