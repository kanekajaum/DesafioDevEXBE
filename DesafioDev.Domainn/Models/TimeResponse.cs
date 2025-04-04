namespace DesafioDev.API.Models
{
    public class TimeResponse
    {
        public int Count { get; set; }
        public List<Time> Teams { get; set; } = new();
    }
}