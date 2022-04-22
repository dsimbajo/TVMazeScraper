using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVMaze.Core.Response;

namespace TVMaze.Core.Contracts
{
    public interface IUnitOfWork
    {
        IShowRepository ShowRepository { get; }

        ICastRepository CastRepository { get; }

        IImportTransactionRepository TransactionRepository { get; }

        Task AddShows(List<TVMaze.Core.Entities.Show> shows);

        Task AddCasts(TVMaze.Core.Entities.Show show);

        Task<int> GetLastPage();

        Task<int> AddTransaction(TVMaze.Core.Entities.ImportTransaction transaction);
        Task<TVShowResponse> GetShowByShowId(int showId);
        Task<IEnumerable<TVShowResponse>> GetAllShowsPerPage(int page);
    }
}
