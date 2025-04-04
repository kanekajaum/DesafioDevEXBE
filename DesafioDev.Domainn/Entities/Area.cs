namespace DesafioDev.API.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public int? ParentAreaId { get; set; }
        public string ParentArea { get; set; }
    }
}