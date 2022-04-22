using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TVMaze.Core.Contracts;
using TVMaze.Core.Entities;

namespace TVMaze.Infrastructure.Data
{
    public class ShowRepository : RepositoryBase<Show>, IShowRepository
    {
        public ShowRepository(IConfiguration configuration) : base(configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddShow(Show show)
        {
            try
            {
                var foundShow = await GetShowById(show.Id);

                if (foundShow == null)
                {
                    return await AddAsync(show);
                }

                return 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
                     
        }

        public async Task<IEnumerable<Show>> GetAllShowsByPage(int page)
        {
            return await QueryPerPage(page);
        }

        public Task<Show> GetShowById(int showId)
        {
            return GetByIdAsync(showId);
        }

    }
}
