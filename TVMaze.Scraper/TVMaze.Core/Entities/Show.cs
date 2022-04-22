using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVMaze.Core.Contracts;

namespace TVMaze.Core.Entities
{

    public class Show : EntityBase
    {
        public Show()
        {
            this.Casts = new List<Cast>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string Network { get; set; }
        public string Summary { get; set; }

        public List<Cast> Casts { get; set; }
    }


}
