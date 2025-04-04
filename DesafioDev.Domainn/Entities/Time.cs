namespace DesafioDev.API.Models
{
    public class Time
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Tla { get; set; }
        public string? Crest { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public int? Founded { get; set; }
        public string? ClubColors { get; set; }
        public string? Venue { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class EquipeResponse
    {
        public int Count { get; set; }
        public Competition? Competition { get; set; }
        public List<Time> Teams { get; set; } = new();
        public List<EquipeInfo> TeamsResponse { get; set; } = new();
        public List<MatchPorTime>? Matches { get; set; }
    }
    public class TimeDetalhado
    {
        public Area? Area { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Tla { get; set; }
        public string? Crest { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public int? Founded { get; set; }
        public List<Competicao>? RunningCompetitions { get; set; }
        public Treinador? Coach { get; set; }
        public List<Jogador>? Squad { get; set; }
    }

    public class Treinador
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public Contrato? Contract { get; set; }
    }

    public class Contrato
    {
        public string? Start { get; set; }
        public string? Until { get; set; }
    }

    public class Jogador
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
