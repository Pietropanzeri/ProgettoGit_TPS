using System.Threading.Tasks.Dataflow;
using ServerApi.Model;

namespace ServerApi.ModelDTO;

public class IngredienteDTO
{
    public int IngredienteId { get; set; }
    public string Nome { get; set; }
    public string Descrizione { get; set; }
    public DateTime? DataInizio { get; set; }
    public DateTime? DataFine { get; set; }

    public IngredienteDTO(){}
    public IngredienteDTO(Ingrediente ingrediente)
    {
        IngredienteId = ingrediente.IngredienteId;
        Nome = ingrediente.Nome;
        Descrizione = ingrediente.Descrizione;
        DataInizio = ingrediente.DataInizio;
        DataFine = ingrediente.DataFine;
    }
}