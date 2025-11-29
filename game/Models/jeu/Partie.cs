namespace game.Models
{
    internal class Partie
    {
        public int PartieId { get; set; }
        public string? Nom { get; set; }
        public int? Seed { get; set; }
        public string? Statut { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
    }
}