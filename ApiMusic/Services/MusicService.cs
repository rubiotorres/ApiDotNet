using ApiMusic.ViewModel;
using ApiMusic.Entities;
using ApiMusic.Exceptions;
using ApiMusic.InputModel;
using ApiMusic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMusic.Services
{
    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _musicaRepository;

        public MusicService(IMusicRepository musicaRepository)
        {
            _musicaRepository = musicaRepository;
        }

        public async Task<List<MusicViewModel>> Obter(int pagina, int quantidade)
        {
            var Musicas = await _musicaRepository.Obter(pagina, quantidade);

            return Musicas.Select(musica => new MusicViewModel
                                {
                                    Id = musica.Id,
                                    Nome = musica.Nome,
                                    Artista = musica.Artista,
                                    Premium = musica.Premium
                                })
                               .ToList();
        }

        public async Task<MusicViewModel> Obter(Guid id)
        {
            var musica = await _musicaRepository.Obter(id);

            if (musica == null)
                return null;

            return new MusicViewModel
            {
                Id = musica.Id,
                Nome = musica.Nome,
                Artista = musica.Artista,
                Premium = musica.Premium
            };
        }

        public async Task<MusicViewModel> Inserir(MusicInputModel musica)
        {
            var entidademusica = await _musicaRepository.Obter(musica.Nome, musica.Artista);

            if (entidademusica.Count > 0)
                throw new MusicaJaCadastradaException();

            var musicaInsert = new Music
            {
                Id = Guid.NewGuid(),
                Nome = musica.Nome,
                Artista = musica.Artista,
                Premium = musica.Premium
            };

            await _musicaRepository.Inserir(musicaInsert);

            return new MusicViewModel
            {
                Id = musicaInsert.Id,
                Nome = musica.Nome,
                Artista = musica.Artista,
                Premium = musica.Premium
            };
        }

        public async Task Atualizar(Guid id, MusicInputModel musica)
        {
            var entidademusica = await _musicaRepository.Obter(id);

            if (entidademusica == null)
                throw new MusicaNaoCadastradaException();

            entidademusica.Nome = musica.Nome;
            entidademusica.Artista = musica.Artista;
            entidademusica.Premium = musica.Premium;

            await _musicaRepository.Atualizar(entidademusica);
        }

        public async Task Atualizar(Guid id, bool Premium)
        {
            var entidademusica = await _musicaRepository.Obter(id);

            if (entidademusica == null)
                throw new MusicaNaoCadastradaException();

            entidademusica.Premium = Premium;

            await _musicaRepository.Atualizar(entidademusica);
        }

        public async Task Remover(Guid id)
        {
            var musica = await _musicaRepository.Obter(id);

            if (musica == null)
                throw new MusicaNaoCadastradaException();

            await _musicaRepository.Remover(id);
        }

        public void Dispose()
        {
            _musicaRepository?.Dispose();
        }
    }
}
