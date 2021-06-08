using ApiMusic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMusic.Repositories
{
    public class MusicRepository : IMusicRepository
    {
        private static Dictionary<Guid, Music> musicas = new Dictionary<Guid, Music>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Music{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Ocean", Artista = "Seafreat", Premium = true} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Music{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Lovely", Artista = "Billie Eilish", Premium = true} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Music{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Obsoleta", Artista = "Sophi", Premium = false} }
        };

        public Task<List<Music>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(musicas.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Music> Obter(Guid id)
        {
            if (!musicas.ContainsKey(id))
                return Task.FromResult<Music>(null);

            return Task.FromResult(musicas[id]);
        }

        public Task<List<Music>> Obter(string nome, string produtora)
        {
            return Task.FromResult(musicas.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Artista.Equals(produtora)).ToList());
        }

        public Task Inserir(Music jogo)
        {
            musicas.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Music jogo)
        {
            musicas[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            musicas.Remove(id);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}
