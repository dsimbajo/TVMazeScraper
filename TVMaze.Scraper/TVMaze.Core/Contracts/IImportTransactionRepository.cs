using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVMaze.Core.Entities;

namespace TVMaze.Core.Contracts
{
    public interface IImportTransactionRepository
    {
        Task<ImportTransaction> GetLastTransaction();

        Task<int> AddTransaction(ImportTransaction transaction);
    }
}
