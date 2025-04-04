namespace DesafioDev.API.Models
{
    public class Competicao
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Type { get; set; }
        public string? Area { get; set; }
        public string? SeasonStart { get; set; }
        public string? SeasonEnd { get; set; }
        public int? CurrentMatchday { get; set; }
    }
}