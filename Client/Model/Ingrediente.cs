namespace Client.Model;

public class Ingrediente
{
    public int IngredienteId { get; set; }
    public string Nome { get; set; }
    public string Descrizione { get; set; }
    public DateTime? DataInizio { get; set; }
    public DateTime? DataFine { get; set; }
}