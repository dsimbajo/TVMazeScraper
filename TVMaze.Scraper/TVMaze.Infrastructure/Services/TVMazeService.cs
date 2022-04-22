using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVMaze.Core.Contracts;
using TVMaze.Core.Response;

namespace TVMaze.Infrastructure.Services
{
    public class TVMazeService : ITVMazeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TVMazeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TVShowResponse> GetShowByShowId(int showId)
        {
            return await _unitOfWork.GetShowByShowId(showId);  
        }

        public async Task<IEnumerable<TVShowResponse>> GetShowsByPage(int page)
        {
            return await _unitOfWork.GetAllShowsPerPage(page); 
        }
    }
}
