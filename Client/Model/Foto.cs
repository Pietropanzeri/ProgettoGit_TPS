namespace Client.Model;

public class Foto
{
    public int FotoId { get; set; }
    public string Descrizione { get; set; }
    public string FotoData { get; set; }
    public int? RicettaId { get; set; }
}