using System.Collections.Generic;

namespace TVMaze.Core.DTO
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Schedule
    {
        public string Time { get; set; }
        public List<string> Days { get; set; }
    }


}
