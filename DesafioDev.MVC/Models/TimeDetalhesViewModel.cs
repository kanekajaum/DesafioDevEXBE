namespace DesafioDev.MVC.Models
{
    public class TimeDetalhesViewModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Venue { get; set; }
        public int? Founded { get; set; }
        public string? Crest { get; set; }
        public CoachViewModel? Coach { get; set; }
        public List<JogadorViewModel>? Squad { get; set; }
    }

    public class CoachViewModel
    {
        public string? Name { get; set; }
    }

    public class JogadorViewModel
    {
        public string? Name { get; set; }
        public string? Position { get; set; }
    }
}