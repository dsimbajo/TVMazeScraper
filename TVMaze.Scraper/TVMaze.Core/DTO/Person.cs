namespace TVMaze.Core.DTO
{
    public class Person
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public string Birthday { get; set; }
        public string Deathday { get; set; }
        public string Gender { get; set; }
        public Image Image { get; set; }
        public int Updated { get; set; }
        public Links _Links { get; set; }
    }


}
