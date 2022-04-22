using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVMaze.Core.Response;

namespace TVMaze.Core.Contracts
{
    public interface ITVMazeService
    {
        Task<IEnumerable<TVShowResponse>> GetShowsByPage(int page);

        Task<TVShowResponse> GetShowByShowId(int showId);
    }
}
