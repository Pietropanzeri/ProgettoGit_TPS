namespace ServerApi.Model;

public class RicettaIngrediente
{
    public int RicettaId { get; set; }
    public Ricetta Ricetta { get; set; }

    public int IngredienteId { get; set; }
    public Ingrediente Ingrediente { get; set; }
}
