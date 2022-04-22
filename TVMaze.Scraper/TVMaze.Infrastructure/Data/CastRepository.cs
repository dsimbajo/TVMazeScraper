using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVMaze.Core.Contracts;
using TVMaze.Core.Entities;

namespace TVMaze.Infrastructure.Data
{
    public class CastRepository : RepositoryBase<Cast>, ICastRepository
    {
        public CastRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }

        public async Task<int> AddCast(Cast cast)
        {
            try
            {
                var foundCast = await GetCastById(cast.Id);

                if (foundCast == null)
                {
                    return await AddAsync(cast);
                }

                return 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task<IEnumerable<Cast>> GetAllCastsByShowId(int showId)
        {
            return await Query("");
        }

        public async Task<Cast> GetCastById(int castId)
        {
            return await GetByIdAsync(castId);
        }
    }
}
