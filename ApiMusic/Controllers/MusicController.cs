using ApiMusic.Exceptions;
using ApiMusic.InputModel;
using ApiMusic.Services;
using ApiMusic.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicaService;

        public MusicController(IMusicService musicaservice)
        {
            _musicaService = musicaservice;
        }

        /// <summary>
        /// Buscar todos os musicas de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os musicas sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de musicas</response>
        /// <response code="204">Caso não haja musicas</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var musicas = await _musicaService.Obter(pagina, quantidade);

            if (musicas.Count() == 0)
                return NoContent();

            return Ok(musicas);
        }

        /// <summary>
        /// Buscar um Musica pelo seu Id
        /// </summary>
        /// <param name="idMusica">Id do Musica buscado</param>
        /// <response code="200">Retorna o Musica filtrado</response>
        /// <response code="204">Caso não haja Musica com este id</response>   
        [HttpGet("{idMusica:guid}")]
        public async Task<ActionResult<MusicViewModel>> Obter([FromRoute] Guid idMusica)
        {
            var Musica = await _musicaService.Obter(idMusica);

            if (Musica == null)
                return NoContent();

            return Ok(Musica);
        }

        /// <summary>
        /// Inserir um Musica no catálogo
        /// </summary>
        /// <param name="MusicInputModel">Dados do Musica a ser inserido</param>
        /// <response code="200">Cao o Musica seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um Musica com mesmo nome para a mesma produtora</response>   
        [HttpPost]
        public async Task<ActionResult<MusicViewModel>> InserirMusica([FromBody] MusicInputModel MusicInputModel)
        {
            try
            {
                var Musica = await _musicaService.Inserir(MusicInputModel);

                return Ok(Musica);
            }
            catch (MusicaJaCadastradaException ex)
            {
                return UnprocessableEntity("Já existe um Musica com este nome para esta produtora");
            }
        }

        /// <summary>
        /// Atualizar um Musica no catálogo
        /// </summary>
        /// /// <param name="idMusica">Id do Musica a ser atualizado</param>
        /// <param name="MusicInputModel">Novos dados para atualizar o Musica indicado</param>
        /// <response code="200">Cao o Musica seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um Musica com este Id</response>   
        [HttpPut("{idMusica:guid}")]
        public async Task<ActionResult> AtualizarMusica([FromRoute] Guid idMusica, [FromBody] MusicInputModel MusicInputModel)
        {
            try
            {
                await _musicaService.Atualizar(idMusica, MusicInputModel);

                return Ok();
            }
            catch (MusicaNaoCadastradaException ex)
            {
                return NotFound("Não existe este Musica");
            }
        }

        /// <summary>
        /// Atualizar o preço de um Musica
        /// </summary>
        /// /// <param name="idMusica">Id do Musica a ser atualizado</param>
        /// <param name="premium">Novo preço do Musica</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um Musica com este Id</response>   
        [HttpPatch("{idMusica:guid}/premium/{premium:double}")]
        public async Task<ActionResult> AtualizarMusica([FromRoute] Guid idMusica, [FromRoute] bool premium)
        {
            try
            {
                await _musicaService.Atualizar(idMusica, premium);

                return Ok();
            }
            catch (MusicaNaoCadastradaException ex)
            {
                return NotFound("Não existe este Musica");
            }
        }

        /// <summary>
        /// Excluir um Musica
        /// </summary>
        /// /// <param name="idMusica">Id do Musica a ser excluído</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um Musica com este Id</response>   
        [HttpDelete("{idMusica:guid}")]
        public async Task<ActionResult> ApagarMusica([FromRoute] Guid idMusica)
        {
            try
            {
                await _musicaService.Remover(idMusica);

                return Ok();
            }
            catch (MusicaNaoCadastradaException ex)
            {
                return NotFound("Não existe este Musica");
            }
        }

    }
}
