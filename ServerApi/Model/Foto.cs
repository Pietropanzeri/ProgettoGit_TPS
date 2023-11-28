namespace ServerApi.Model;

public class Foto
{
    public int FotoId { get; set; }
    public string Descrizione { get; set; }
    public byte[] FotoData { get; set; }

    public int? RicettaId { get; set; }
    public Ricetta Ricetta { get; set; }
}