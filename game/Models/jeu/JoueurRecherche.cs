namespace game.Models
{
    internal class JoueurRecherche
    {
        public int JrId { get; set; }
        public int PartieId { get; set; }
        public int JoueurId { get; set; }
        public int RechercheId { get; set; }
        public string? Etat { get; set; }
        public double? Progression { get; set; }
    }
}