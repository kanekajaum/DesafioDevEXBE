namespace DesafioDev.MVC.Models
{
    public class TopJogadorViewModel
    {
        public PlayerViewModel Player { get; set; }
        public TeamViewModel Team { get; set; }
        public int PlayedMatches { get; set; }
        public int Goals { get; set; }
        public int? Assists { get; set; }
        public int? Penalties { get; set; }
    }

    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Section { get; set; }
    }

    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Crest { get; set; }
    }
}