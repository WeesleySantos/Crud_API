using API.Data;
using Dapper;
using Microsoft.AspNetCore.Connections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private DbSession _db;
        public TarefaRepository(DbSession dbSession)
        {
            _db = dbSession;
        }
        public async Task<int> DeleteAsync(int id)
        {
            using (var conn = _db.Connection)
            {
                string command = @"DELETE FROM Tarefas WHERE Id = @Id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
        }

        public async Task<Tarefa> GetTarefaByIdAsync(int id)
        {
            using (var conn = _db.Connection)
            {
                var query = "SELECT * FROM Tarefas WHERE Id = @Id";
                Tarefa tarefa = await conn.QueryFirstOrDefaultAsync<Tarefa>(sql: query, param: new { id });
                return tarefa;
            }
        }

        public async Task<List<Tarefa>> GetTarefasAsync()
        {
            using (var coon = _db.Connection)
            {
                string query = "SELECT * FROM Tarefas";
                List<Tarefa> tarefas = (await coon.QueryAsync<Tarefa>(sql: query)).ToList();
                return tarefas;
            }
        }

        public async Task<TarefaContainer> GetTarefasEContadorAsync()
        {
            using (var conn = _db.Connection)
            {
                string query = @"SELECT COUNT(*) FROM Tarefas
                                 SELECT * FROM Tarefas";
                var reader = await conn.QueryMultipleAsync(sql: query);
                return new TarefaContainer
                {
                    Contador = (await reader.ReadAsync<int>()).FirstOrDefault(),
                    Tarefas = (await reader.ReadAsync<Tarefa>()).ToList()
                };
            }
        }

        public async Task<int> SaveAsync(Tarefa novaTarefa)
        {
            using (var conn = _db.Connection)
            {
                string command = @"INSERT INTO Tarefas(DESCRICAO, ISCOMPLETA) 
                                               VALUES(@DESCRICAO, @ISCOMPLETA)";
                var resultado = await conn.ExecuteAsync(sql: command, param: novaTarefa);
                return resultado;
            }
        }

        public async Task<int> UpdateTarefaStatusAsync(Tarefa atualizaTarefa)
        {
            try
            {
                using (var conn = _db.Connection)
                {
                    string command = @"UPDATE Tarefas SET DESCRICAO = @Descricao, ISCOMPLETA = @IsCompleta
                                                      WHERE Id = @Id";
                    var resultado = await conn.ExecuteAsync(command, param: new { atualizaTarefa.Descricao, atualizaTarefa.IsCompleta, atualizaTarefa.Id});
                    return resultado;
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
