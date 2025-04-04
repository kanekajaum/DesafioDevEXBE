using System.Text.Json.Serialization;

namespace DesafioDev.MVC.Models
{
    public class JogosRecentesViewModel
    {
        public int Count { get; set; }
        public Competition Competition { get; set; }

        [JsonPropertyName("matches")]
        public List<MatchViewModel> MatchesVM { get; set; }
    }

    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Emblem { get; set; }
    }

    public class MatchViewModel
    {
        public Competition Competition { get; set; }
        public string UtcDate { get; set; }
        public string Status { get; set; }
        public int Matchday { get; set; }
        public TimeViewModel HomeTeam { get; set; }
        public TimeViewModel AwayTeam { get; set; }
        public ScoreViewModel Score { get; set; }
    }

    public class ScoreViewModel
    {
        public string Winner { get; set; }
        public string Duration { get; set; }
        public ScoreDetailViewModel FullTime { get; set; }
        public ScoreDetailViewModel HalfTime { get; set; }
        public ScoreDetailViewModel RegularTime { get; set; }
        public ScoreDetailViewModel ExtraTime { get; set; }
        public ScoreDetailViewModel Penalties { get; set; }
    }

    public class ScoreDetailViewModel
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }
}
