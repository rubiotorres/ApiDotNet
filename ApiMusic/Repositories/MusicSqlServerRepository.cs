using ApiMusic.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiMusic.Repositories
{
    public class MusicSqlServerRepository : IMusicRepository
    {
        private readonly SqlConnection sqlConnection;

        public MusicSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Music>> Obter(int pagina, int quantidade)
        {
            var Musica = new List<Music>();

            var comando = $"select * from Musica order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                Musica.Add(new Music
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Artista = (string)sqlDataReader["Artista"],
                    Premium = (bool)sqlDataReader["Premium"]
                });
            }

            await sqlConnection.CloseAsync();

            return Musica;
        }

        public async Task<Music> Obter(Guid id)
        {
            Music musica = null;

            var comando = $"select * from Musica where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                musica = new Music
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Artista = (string)sqlDataReader["Artista"],
                    Premium = (bool)sqlDataReader["Premium"]
                };
            }

            await sqlConnection.CloseAsync();

            return musica;
        }

        public async Task<List<Music>> Obter(string nome, string Artista)
        {
            var Musica = new List<Music>();

            var comando = $"select * from Musica where Nome = '{nome}' and Artista = '{Artista}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                Musica.Add(new Music
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Artista = (string)sqlDataReader["Artista"],
                    Premium = (bool)sqlDataReader["Premium"]
                });
            }

            await sqlConnection.CloseAsync();

            return Musica;
        }

        public async Task Inserir(Music musica)
        {
            var comando = $"insert Musica (Id, Nome, Artista, Premium) values ('{musica.Id}', '{musica.Nome}', '{musica.Artista}', {musica.Premium.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Music musica)
        {
            var comando = $"update Musica set Nome = '{musica.Nome}', Artista = '{musica.Artista}', Premium = {musica.Premium.ToString().Replace(",", ".")} where Id = '{musica.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Musica where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
