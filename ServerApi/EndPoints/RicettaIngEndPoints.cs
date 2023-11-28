using ServerApi.Data;
using ServerApi.Model;

namespace ServerApi.EndPoints
{
    public static class RicettaIngEndPoints
    {
        public static void MapRicIngEndPoints(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("/ricettaInd/{ricettaId}/{ingredienteId}", async (RicettarioDbContext db, int ricettaId, int ingredienteId) =>
            {
                if (ricettaId == 0 && ingredienteId == 0)
                    return Results.NotFound();

                RicettaIngrediente ricettaIng = new RicettaIngrediente()
                {
                    RicettaId = ricettaId,
                    IngredienteId = ingredienteId
                };

                await db.RicetteIngredienti.AddAsync(ricettaIng);
                await db.SaveChangesAsync();
                return Results.Ok("Ricetta e ingrediente collegati correttamente");
            });
        }
    }
}
