using ServerApi.Model;

namespace ServerApi.ModelDTO;

public class FotoDTO
{
    public int FotoId { get; set; }
    public string Descrizione { get; set; }
    public byte[] FotoData { get; set; }
    public int? RicettaId { get; set; }

    public FotoDTO(){}
    public FotoDTO(Foto foto)
    {
        FotoId = foto.FotoId;
        Descrizione = foto.Descrizione;
        FotoData = foto.FotoData;
        RicettaId = foto.RicettaId;
    }
}

