namespace game.Models
{
    internal class Zone
    {
        public int ZoneId { get; set; }
        public int PartieId { get; set; }
        public string Coord { get; set; }
        public string? Type { get; set; }
        public int? Danger { get; set; }
        public int? DecouvertPar { get; set; }
    }
}