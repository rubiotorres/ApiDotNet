using System;

namespace ApiMusic.ViewModel
{
    public class MusicViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Artista { get; set; }
        public bool Premium{ get; set; }

    }
}
