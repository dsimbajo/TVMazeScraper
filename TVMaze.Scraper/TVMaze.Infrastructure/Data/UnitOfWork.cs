using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVMaze.Core.Contracts;
using TVMaze.Core.Response;

namespace TVMaze.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IShowRepository ShowRepository => _serviceProvider.GetService<IShowRepository>();

        public ICastRepository CastRepository => _serviceProvider.GetService<ICastRepository>();

        public IImportTransactionRepository TransactionRepository => _serviceProvider.GetService<IImportTransactionRepository>();

        public async Task AddShows(List<TVMaze.Core.Entities.Show> shows)
        {
            try
            {
                foreach (var show in shows)
                {
                    await ShowRepository.AddShow(show);

                    await AddCasts(show);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task AddCasts(TVMaze.Core.Entities.Show show)
        {
            try
            {
                var casts = show.Casts;

                foreach (var cast in casts)
                {
                    await CastRepository.AddCast(cast);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

          

        }

        public async Task<int> GetLastPage()
        {
            var lastTransaction = await TransactionRepository.GetLastTransaction();

            if (lastTransaction == null)
            {
                return 1;
            }
            else
            {
                return lastTransaction.NextPage;
            }
        }

        public async Task<int> AddTransaction(Core.Entities.ImportTransaction transaction)
        {
            return await TransactionRepository.AddTransaction(transaction);
        }

        public async Task<TVShowResponse> GetShowByShowId(int showId)
        {
            try
            {
                var show = await ShowRepository.GetShowById(showId);

                var cast = await CastRepository.Query($"WHERE ShowId = {showId}");

                return new TVShowResponse()
                {
                    Id = show.Id,
                    Url = show.Url,
                    Name = show.Name,
                    Type = show.Type,
                    Language = show.Language,
                    Genre = show.Genre,
                    Network = show.Network,
                    Summary = show.Summary,
                    Casts = cast.ToList()
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }    

          
        }

        public async Task<IEnumerable<TVShowResponse>> GetAllShowsPerPage(int page)
        {
            List<TVShowResponse> tvShowList = new List<TVShowResponse>();

            var shows = await ShowRepository.GetAllShowsByPage(page);

            foreach (var show in shows)
            {
                var cast = await CastRepository.Query($"WHERE ShowId = {show.Id}");

                var tvShowResponse = new TVShowResponse()
                {
                    Id = show.Id,
                    Url = show.Url,
                    Name = show.Name,
                    Type = show.Type,
                    Language = show.Language,
                    Genre = show.Genre,
                    Network = show.Network,
                    Summary = show.Summary,
                    Casts = cast.ToList()
                };

                tvShowList.Add(tvShowResponse);
            }

            return tvShowList;
        }
    }
}
