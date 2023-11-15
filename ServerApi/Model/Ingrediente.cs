using System.ComponentModel.DataAnnotations.Schema;

namespace ServerApi.Model;

public class Ingrediente
{
    public int IngredienteId { get; set; }

    [Column(TypeName = "nvarchar(30)")]
    public string Nome { get; set; }

    [Column(TypeName = "nvarchar(800)")]
    public string Descrizione { get; set; }
    public DateTime? DataInizio { get; set; }
    public DateTime? DataFine { get; set; }
    public ICollection<RicettaIngrediente> RicettaIngredienti { get; set; } = null!;

}