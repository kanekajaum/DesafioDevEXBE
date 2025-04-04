namespace DesafioDev.API.Models
{
    public class AreaResponse
    {
        public int Count { get; set; }
        public object Filters { get; set; }
        public List<AreaInfo> Areas { get; set; }
    }
}