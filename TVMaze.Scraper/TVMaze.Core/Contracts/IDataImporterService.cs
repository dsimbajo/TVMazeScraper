using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVMaze.Core.DTO;

namespace TVMaze.Core.Contracts
{
    public interface IDataImporterService
    {
        Task Import();

    }
}
