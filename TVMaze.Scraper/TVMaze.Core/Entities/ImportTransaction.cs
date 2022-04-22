using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVMaze.Core.Contracts;

namespace TVMaze.Core.Entities
{
    public class ImportTransaction : EntityBase
    {
        public DateTime LastRun { get; set; }

        public int NextPage { get; set; }
    }
}
