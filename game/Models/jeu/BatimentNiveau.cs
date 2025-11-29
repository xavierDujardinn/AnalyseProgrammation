namespace game.Models
{
    internal class BatimentNiveau
    {
        public int BnId { get; set; }
        public int BatimentId { get; set; }
        public int Niveau { get; set; }
        public string? Couts { get; set; }
        public string? Prod { get; set; }
        public string? Prerequis { get; set; }
    }
}