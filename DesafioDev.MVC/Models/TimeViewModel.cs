namespace DesafioDev.MVC.Models
{
    public class TimeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Tla { get; set; }
        public string Crest { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public int Founded { get; set; }
        public string Venue { get; set; }
        public string Escudo { get; set; }
        public CoachViewModel Coach { get; set; }
        public List<JogadorViewModel> Squad { get; set; }
        public List<MatchViewModel> JogosRecentes { get; set; } = new List<MatchViewModel>();
    }
}