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
    public class ImportTransactionRepository : RepositoryBase<ImportTransaction>, IImportTransactionRepository
    {
        public ImportTransactionRepository(IConfiguration configuration)
            : base(configuration)
        {

        }

        public async Task<int> AddTransaction(ImportTransaction transaction)
        {
            return await AddAsync(transaction);
        }

        public async Task<ImportTransaction> GetLastTransaction()
        {
            var transactions = await GetAllAsync();

            if (transactions.Count() > 0)
            {
                return transactions.OrderByDescending(x => x.LastRun).First();
            }

            return null;
           
        }
    }
}
