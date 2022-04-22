using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVMaze.Core.Contracts;
using TVMaze.Core.Response;


namespace TVMaze.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVMazeController : ControllerBase
    {
        private readonly ITVMazeService _tvMazeService;
        private readonly ILoggerManager _logger;

        public TVMazeController(ITVMazeService tvMazeService, ILoggerManager logger)
        {
            _tvMazeService = tvMazeService;
            _logger = logger;
        }

        [HttpGet("shows")]
        public async Task<IActionResult> GetAllShowsByPage(int page)
        {
          
                _logger.LogInfo($"Retrieving shows on page {page}");

                var shows = await _tvMazeService.GetShowsByPage(page);

                _logger.LogInfo($"{shows.ToList().Count} shows are retrieved");

                return Ok(shows);                  
        }


        [HttpGet("show/{id}")]
        public async Task<IActionResult> GetShowById(int id)
        {
         
                _logger.LogInfo($"Retrieving show with Id - {id}");

                var show = await _tvMazeService.GetShowByShowId(id);

                _logger.LogInfo($"Retrieved Show with Id - {id}");

                return Ok(show);  
          
        }

        
    }
}
