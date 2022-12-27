using System.Threading.Tasks;

namespace Yamaha.Rota.Domain.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<bool> IncluirAsync(T entidade);

        Task<bool> AtualizarAsync(T entidade);
    }
}
