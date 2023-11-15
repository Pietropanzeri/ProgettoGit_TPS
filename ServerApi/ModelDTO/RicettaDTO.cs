using ServerApi.Model;

namespace ServerApi.ModelDTO;

public class RicettaDTO
{
    public int RicettaId { get; set; }
    public int UtenteId { get; set; }
    public string Nome { get; set; }
    public string Preparazione { get; set; }
    public int? Tempo { get; set; }
    public int? Difficolta { get; set; }
    public TipoPiatto Piatto { get; set; }

    public RicettaDTO(){}
    public RicettaDTO(Ricetta ricetta)
    {
        RicettaId = ricetta.RicettaId;
        UtenteId = ricetta.UtenteId;
        Nome = ricetta.Nome;
        Preparazione = ricetta.Preparazione;
        Tempo = ricetta.Tempo;
        Difficolta = ricetta.Difficolta;
        Piatto = ricetta.Piatto;
    }
}