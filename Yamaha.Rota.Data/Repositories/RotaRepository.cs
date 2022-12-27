using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaha.Rota.Domain.Dominio.Rota.Interfaces;

namespace Yamaha.Rota.Data.Repositories
{
    public class RotaRepository : IRotaRepository
    {
        private DbSession _session;

        public RotaRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<Domain.Dominio.Rota.Rota> ObterRotaAsync(Domain.Dominio.Rota.Rota entidade)
        {

            var rota= await _session.Connection.QueryFirstOrDefaultAsync<Domain.Dominio.Rota.Rota>("SELECT * FROM Rota WHERE Origem=@origem AND Destino=@destino",
                                                                 new
                                                                 {
                                                                    
                                                                     entidade.Origem,
                                                                     entidade.Destino
                                                                     
                                                                 }, transaction: _session.Transaction);
            return rota;
        }

        public async Task<IEnumerable<Domain.Dominio.Rota.Rota>> ObterRotasAsync()
        {

            var rotas = await _session.Connection.QueryAsync<Domain.Dominio.Rota.Rota>("SELECT * FROM Rota",
                                                                 new
                                                                 {
                                                                 }, transaction: _session.Transaction);


            return rotas;
        }      

        public async Task<bool> IncluirAsync(Domain.Dominio.Rota.Rota entidade)
        {
            await _session.Connection.QueryAsync<bool>("INSERT INTO Rota VALUES (@origem, @destino, @valor)",
            new
            {
                entidade.Origem,
                entidade.Destino,
                entidade.Valor,
            }, transaction: _session.Transaction);

            return true;
        }

        public async Task<bool> AtualizarAsync(Domain.Dominio.Rota.Rota entidade)
        {
            await _session.Connection.QueryAsync<bool>("UPDATE Rota SET Valor=@valor WHERE Id=@id",
            new
            {
                entidade.Valor,
                entidade.Id
            }, transaction: _session.Transaction);

            return true;
        }
    }
}
