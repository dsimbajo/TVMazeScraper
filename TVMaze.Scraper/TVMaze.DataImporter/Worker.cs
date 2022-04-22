using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TVMaze.Core.Contracts;

namespace TVMaze.DataImporter
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDataImporterService _dataImporterService;
        private bool _isRunning;

        public Worker(ILogger<Worker> logger, IDataImporterService dataImporterService)
        {
            _logger = logger;
            _dataImporterService = dataImporterService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            
                await _dataImporterService.Import();
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
