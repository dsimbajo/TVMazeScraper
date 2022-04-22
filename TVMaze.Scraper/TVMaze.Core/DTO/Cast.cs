using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVMaze.Core.DTO
{

    public class Cast
    {
        public Person Person { get; set; }
        public Character Character { get; set; }
        public bool Self { get; set; }
        public bool Voice { get; set; }
    }


}
