using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaha.Rota.Domain.Dominio.Rota.Arguments;

namespace Yamaha.Rota.Domain.Dominio.Rota.Interfaces
{
    public interface IRotaService
    {
        Task<IEnumerable<Rota>> SalvarRotaAsync(string caminhoArquivo);

        Task<string> ObterMelhorRotaAsync(RotaRequest request);

        Task<IEnumerable<Rota>> ObterRotasAsync();
    }
}
