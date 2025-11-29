
namespace game.Models
{
    internal class EvenementInstan
    {
        public int EveninstId { get; set; }
        public int PartieId { get; set; }
        public int Tour { get; set; }
        public int EventId { get; set; }
        public string? CibleType { get; set; }
        public int? CibleId { get; set; }
        public string? Decision { get; set; }
        public int? Resolu { get; set; }
        public string? CreatedAt { get; set; }
    }
}