using TVMaze.Core.Contracts;

namespace TVMaze.Core.Entities
{
    public class Cast : EntityBase
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Deathday { get; set; }
        public string Gender { get; set; }
        public string Character { get; set; }
        public string CharacterUrl { get; set; }

        //public Show Show { get; set; }
    }


}
