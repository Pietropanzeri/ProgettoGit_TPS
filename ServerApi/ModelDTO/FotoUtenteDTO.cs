using ServerApi.Model;

namespace ServerApi.ModelDTO;

public class FotoUtenteDTO
{
    public int FotoId { get; set; }
    public byte[] FotoData { get; set; }
    public int? UtenteId { get; set; }

    public FotoUtenteDTO(){}
    public FotoUtenteDTO(FotoUtente foto)
    {
        FotoId = foto.FotoId;
        FotoData = foto.FotoData;
        UtenteId = foto.UtenteId;
    }
}