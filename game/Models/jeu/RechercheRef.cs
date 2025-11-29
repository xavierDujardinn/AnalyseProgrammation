namespace game.Models
{
    internal class RechercheRef
    {
        public int RechercheId { get; set; }
        public string Code { get; set; }
        public string? Libelle { get; set; }
        public string? Prerequis { get; set; }
        public string? Effets { get; set; }
    }
}