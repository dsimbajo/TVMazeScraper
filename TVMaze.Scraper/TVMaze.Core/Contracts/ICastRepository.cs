using System.Collections.Generic;
using System.Threading.Tasks;
using TVMaze.Core.Entities;

namespace TVMaze.Core.Contracts
{
    public interface ICastRepository : IAsyncGenericRepository<Cast>
    {
        Task<int> AddCast(Cast cast);

        Task<IEnumerable<Cast>> GetAllCastsByShowId(int showId);

        Task<Cast> GetCastById(int castId);
    }
}