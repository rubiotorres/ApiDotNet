using System;

namespace ApiMusic.Exceptions
{
    public class MusicaNaoCadastradaException: Exception
    {
        public MusicaNaoCadastradaException()
            :base("Essa musica não está cadastrada")
        {}
    }
}
