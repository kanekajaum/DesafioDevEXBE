namespace DesafioDev.API.Models
{
    public class CompeticaoResponse
    {
        public List<CompeticaoInfo> Competitions { get; set; } = new();
    }

    public class CompeticaoInfo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Type { get; set; }
        public AreaInfo? Area { get; set; }
        public SeasonInfo? CurrentSeason { get; set; }
        public List<EquipeInfo>? Teams { get; set; }
        public List<PartidaInfo>? Matches { get; set; }
    }

    public class AreaInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public object Flag { get; set; }
        public int? ParentAreaId { get; set; }
        public string ParentArea { get; set; }
    }

    public class SeasonInfo
    {
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? CurrentMatchday { get; set; }
    }

    public class EquipeInfo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Tla { get; set; }
        public string? Crest { get; set; }
    }

    public class PartidaInfo
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string? Competicao { get; set; }
        public string? TimeCasa { get; set; }
        public string? TimeVisitante { get; set; }
    }

    public class TimeInfo
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Tla { get; set; }
        public List<PartidaInfo>? Partidas { get; set; }
    }
}