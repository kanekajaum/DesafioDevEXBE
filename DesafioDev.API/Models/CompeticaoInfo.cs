namespace DesafioDev.API.Models
{
    public class CompeticaoInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Type { get; set; }
        public string AreaCode { get; set; }
        public Area Area { get; set; }
        public SeasonInfo? CurrentSeason { get; set; }
        public List<EquipeInfo>? Teams { get; set; }
        public List<Match>? Matches { get; set; }
    }
}