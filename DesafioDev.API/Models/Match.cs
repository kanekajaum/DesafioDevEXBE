namespace DesafioDev.API.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime UtcDate { get; set; }
        public string Status { get; set; }
        public int Matchday { get; set; }
        public string Stage { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Score Score { get; set; }
        public Competition Competition { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Tla { get; set; }
        public string Crest { get; set; }
    }

    public class ScoreDetails
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }

    public class Score
    {
        public string Winner { get; set; }
        public ScoreDetails FullTime { get; set; }
        public ScoreDetails HalfTime { get; set; }
        public ScoreDetails RegularTime { get; set; }
        public ScoreDetails ExtraTime { get; set; }
        public ScoreDetails Penalties { get; set; }
    }

    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Emblem { get; set; }
    }

    public class PartidaResponse
    {
        public List<Match> Matches { get; set; }
    }
}
