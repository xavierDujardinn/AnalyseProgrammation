namespace game.Models
{
    internal class JoueurRessource
    {
        public int JrId { get; set; }
        public int PartieId { get; set; }
        public int JoueurId { get; set; }
        public int RessourceId { get; set; }
        public double? Qty { get; set; }

    }
}