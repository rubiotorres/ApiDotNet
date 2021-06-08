using ApiMusic.InputModel;
using ApiMusic.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMusic.Services
{
    public interface IMusicService : IDisposable
    {
        Task<List<MusicViewModel>> Obter(int pagina, int quantidade);
        Task<MusicViewModel> Obter(Guid id);
        Task<MusicViewModel> Inserir(MusicInputModel musica);
        Task Atualizar(Guid id, MusicInputModel musica);
        Task Atualizar(Guid id, bool premium);
        Task Remover(Guid id);
    }
}
