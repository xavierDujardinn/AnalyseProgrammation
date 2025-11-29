namespace game.Models
{
    internal class Joueur
    {
        public int JoueurId { get; set; }
        public int PartieId { get; set; }
        public string? Pseudo { get; set; }
        public string? couleur { get; set; }
        public int? OrdreTour { get; set; }
        public int? Actif { get; set; }
        public string? CreatedAt { get; set; }
    }
}
