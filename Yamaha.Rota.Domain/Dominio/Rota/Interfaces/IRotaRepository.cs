using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaha.Rota.Domain.Interfaces;

namespace Yamaha.Rota.Domain.Dominio.Rota.Interfaces
{

    public interface IRotaRepository : IRepositoryBase<Rota>
    { 
        Task<Rota> ObterRotaAsync(Rota entidade);

        Task<IEnumerable<Rota>> ObterRotasAsync();
    }
}
