public class ActionLog
{
    public int LogId {get; set;}
    public int PartieId {get; set;}
    public int? JoueurId {get; set;}
    public string? Type {get; set;}
    public string? Payload {get; set;}
    public string? Ts {get; set;}
}