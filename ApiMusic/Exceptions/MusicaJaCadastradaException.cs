using System;

namespace ApiMusic.Exceptions
{
    public class MusicaJaCadastradaException : Exception
    {
        public MusicaJaCadastradaException()
            : base("Esta musica ja esta cadastrada")
        { }
    }
}
