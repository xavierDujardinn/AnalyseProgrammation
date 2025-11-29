namespace game.Models
{
    internal class Exploration
    {
        public int ExplorationId { get; set; }
        public int PartieId { get; set; }
        public int JoueurId { get; set; }
        public int ZoneId { get; set; }
        public string? Status { get; set; }
        public string? Resultats { get; set; }
        public string? CreatedAt { get; set; }
    }
}