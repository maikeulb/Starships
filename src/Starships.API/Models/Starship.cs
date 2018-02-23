using System.Collections.Generic;

namespace Starships.API.Model
{
    public class Starship
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string StarshipClass { get; set; }
        public string Manufactuerer { get; set; }
        public string HyperdriveRating { get; set; }
    }
}
