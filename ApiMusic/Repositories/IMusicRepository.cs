using ApiMusic.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMusic.Repositories
{
    public interface IMusicRepository : IDisposable
    {
        Task<List<Music>> Obter(int pagina, int quantidade);
        Task<Music> Obter(Guid id);
        Task<List<Music>> Obter(string nome, string produtora);
        Task Inserir(Music jogo);
        Task Atualizar(Music jogo);
        Task Remover(Guid id);
    }
}
