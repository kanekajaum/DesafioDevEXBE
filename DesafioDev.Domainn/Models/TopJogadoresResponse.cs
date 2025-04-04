namespace DesafioDev.API.Models
{
    public class TopJogadoresResponse
    {
        public class TopScorersResponse
        {
            public int Count { get; set; }
            public Filters Filters { get; set; }
            public Competition Competition { get; set; }
            public Season Season { get; set; }
            public List<Scorer> Scorers { get; set; }
        }

        public class Filters
        {
            public string Season { get; set; }
            public int Limit { get; set; }
        }

        public class Competition
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Type { get; set; }
            public string Emblem { get; set; }
        }

        public class Season
        {
            public int Id { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int CurrentMatchday { get; set; }
            public object Winner { get; set; }
        }

        public class Scorer
        {
            public Player Player { get; set; }
            public Team Team { get; set; }
            public int PlayedMatches { get; set; }
            public int Goals { get; set; }
            public int? Assists { get; set; }
            public int? Penalties { get; set; }
        }

        public class Player
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Nationality { get; set; }
            public string Section { get; set; }
            public string Position { get; set; }
            public int? ShirtNumber { get; set; }
            public DateTime LastUpdated { get; set; }
        }

        public class Team
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ShortName { get; set; }
            public string Tla { get; set; }
            public string Crest { get; set; }
            public string Address { get; set; }
            public string Website { get; set; }
            public int Founded { get; set; }
            public string ClubColors { get; set; }
            public string Venue { get; set; }
            public DateTime LastUpdated { get; set; }
        }

    }
}