using DesafioDev.MVC.Models;

namespace DesafioDev.MVC
{
    public class JogoViewModel
    {
        public int Id { get; set; }
        public string UtcDate { get; set; }
        public string Status { get; set; }
        public int Matchday { get; set; }
        public EquipeViewModel HomeTeam { get; set; }
        public EquipeViewModel AwayTeam { get; set; }
        public PlacarViewModel Score { get; set; }
    }

    public class EquipeViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Crest { get; set; }
    }

    public class PlacarViewModel
    {
        public string Winner { get; set; }
        public ResultadoViewModel FullTime { get; set; }
    }

    public class ResultadoViewModel
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }

    public class HomeViewModel
    {
        public List<JogoViewModel> JogosFinalizados { get; set; }
        public List<JogoViewModel> JogosDeHoje { get; set; }
        public List<JogoViewModel> JogosChampions { get; set; }
        public List<TopJogadorViewModel> TopJogadores { get; set; }
    }
}