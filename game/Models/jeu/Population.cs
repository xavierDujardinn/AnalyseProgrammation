namespace game.Models
{
    internal class Population
    {
        public int PopulationId { get; set; }
        public int PartieId { get; set; }
        public int JoueurId { get; set; }
        public int? Total { get; set; }
        public int? Moral { get; set; }
        public string? Besoins { get; set; }
        public string? Specialisation { get; set; }
    }
}