using System.Collections.Generic;
using System.Threading.Tasks;
using TVMaze.Core.Entities;

namespace TVMaze.Core.Contracts
{
    public interface IShowRepository
    {
        Task<int> AddShow(Show show);

        Task<IEnumerable<Show>> GetAllShowsByPage(int page);

        Task<Show> GetShowById(int castId);

    }
}