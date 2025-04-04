namespace DesafioDev.API.Models
{
    public class MatchesResponse
    {
        public Filters Filters { get; set; }
        public ResultSet ResultSet { get; set; }
        public List<Match> Matches { get; set; }
    }

    public class Filters
    {
        public string Competitions { get; set; }
        public string Permission { get; set; }
        public int Limit { get; set; }
    }

    public class ResultSet
    {
        public int Count { get; set; }
        public string Competitions { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public int Played { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
    }

    public class MatchPorTime
    {
        public Area Area { get; set; }
        public Competition Competition { get; set; }
        public Season Season { get; set; }
        public int Id { get; set; }
        public string UtcDate { get; set; }
        public string Status { get; set; }
        public int Matchday { get; set; }
        public string Stage { get; set; }
        public string Group { get; set; }
        public string LastUpdated { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Score Score { get; set; }
        public Odds Odds { get; set; }
        public List<Referee> Referees { get; set; }
    }

    public class Season
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int CurrentMatchday { get; set; }
        public object Winner { get; set; } // Pode ser null, por isso 'object'
    }

    public class TimeScore
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }

    public class Odds
    {
        public string Msg { get; set; }
    }

    public class Referee
    {
        public int? Id { get; set; } // Pode ser null
        public string Name { get; set; }
        public string Nationality { get; set; }
    }

}
