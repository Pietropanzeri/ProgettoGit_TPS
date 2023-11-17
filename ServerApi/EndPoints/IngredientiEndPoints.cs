using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerApi.Data;
using ServerApi.Model;
using ServerApi.ModelDTO;

namespace ServerApi.EndPoints
{
    public static class IngredientiEndPoints
    {
        public static void MapIngredientiEndpoints(this WebApplication app)
        {
            app.MapGet("/ingredienti", async (RicettarioDbContext db) =>
                Results.Ok(await db.Ingredienti.Select(i => new IngredienteDTO(i)).ToListAsync()));
            app.MapPost("/ingredienti", async (RicettarioDbContext db, IngredienteDTO ingredienteDTO) =>
            {
                var ingrediente = new Ingrediente
                {
                    Nome = ingredienteDTO.Nome, 
                    Descrizione = ingredienteDTO.Descrizione, 
                    IngredienteId = ingredienteDTO.IngredienteId, 
                    DataInizio = ingredienteDTO.DataInizio, 
                    DataFine = ingredienteDTO.DataFine
                };
                await db.Ingredienti.AddAsync(ingrediente);
                await db.SaveChangesAsync();
                return Results.Created($"/ingredienti/{ingrediente.IngredienteId}", new IngredienteDTO(ingrediente));
            });
            app.MapGet("/ingredienti/{ingredienteId}", async (RicettarioDbContext db, int ingredienteId) =>
                await db.Ingredienti.FindAsync(ingredienteId)
                    is Ingrediente ingrediente
                    ? Results.Ok(new IngredienteDTO(ingrediente))
                    : Results.NotFound()
            );
            app.MapDelete("/ingredienti/{ingredienteId}", async (RicettarioDbContext db, int ingredienteId) =>
            {
                var ingrediente = await db.Ingredienti.FindAsync(ingredienteId);
                if (ingrediente is null)
                {
                    return Results.NotFound();
                }
                db.Ingredienti.Remove(ingrediente);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
            app.MapPut("/ingredienti/{ingredienteId}", async (RicettarioDbContext db, IngredienteDTO updateIngrediente, int ingredienteId) =>
            {
                var ingrediente = await db.Ingredienti.FindAsync(ingredienteId);
                if (ingrediente is null) return Results.NotFound();

                if (!updateIngrediente.Nome.IsNullOrEmpty())
                    ingrediente.Nome = updateIngrediente.Nome;
                if (!updateIngrediente.Descrizione.IsNullOrEmpty())
                    ingrediente.Descrizione = updateIngrediente.Descrizione;
                if (updateIngrediente.DataInizio.HasValue)
                    ingrediente.DataInizio = updateIngrediente.DataInizio;
                if (updateIngrediente.DataFine.HasValue)
                    ingrediente.DataFine = updateIngrediente.DataFine;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapGet("ingredienti/nome/{nomeIngrediente}", async (RicettarioDbContext db, string nomeIngrediente) =>
            {
                var listaRis =await db.Ingredienti.Where(i => i.Nome.Contains(nomeIngrediente)).ToListAsync();
                if (listaRis.IsNullOrEmpty())
                    return Results.NotFound();
                List<IngredienteDTO> listaDTORes = new List<IngredienteDTO>();
                foreach (var i in listaRis)
                    listaDTORes.Add(new IngredienteDTO(i));
                return Results.Ok(listaDTORes);
            });
            app.MapGet("ingredienti/nome/{nomeIngrediente}/{indicePartenza}/{countIngredienti}", async (RicettarioDbContext db, string nomeIngrediente, int indicePartenza, int countIngredienti) =>
            {
                var listaRis =await db.Ingredienti.Where(i => i.Nome.Contains(nomeIngrediente)).ToListAsync();
                if (listaRis.IsNullOrEmpty())
                    return Results.NotFound();
                List<IngredienteDTO> listaDTORes = new List<IngredienteDTO>();
                foreach (var i in listaRis)
                    listaDTORes.Add(new IngredienteDTO(i));

                var risultato = new List<IngredienteDTO>();
                try
                {
                    risultato = listaDTORes.GetRange(indicePartenza, Math.Min(listaDTORes.Count(), countIngredienti));
                }
                catch (Exception e)
                {
                    risultato = listaDTORes.GetRange(indicePartenza, listaDTORes.Count()-indicePartenza);
                }

                return Results.Ok(risultato);
            });
            app.MapGet("ingrediente/ricetta/{ricettaId}", async (RicettarioDbContext db, int ricettaId) =>
            {
                var ingredientiRicetta = await db.RicetteIngredienti.Where(ri => ri.RicettaId == ricettaId).Join(db.Ingredienti,
                    ri => ri.IngredienteId, 
                    i => i.IngredienteId,
                    (ri,i)=> i).ToListAsync();
                
                if (ingredientiRicetta.IsNullOrEmpty())
                    return Results.NotFound();
                List<IngredienteDTO> listaDTORes = new List<IngredienteDTO>();
                foreach (var i in ingredientiRicetta)
                    listaDTORes.Add(new IngredienteDTO(i));
                return Results.Ok(listaDTORes);
            });
        }   
    }
}
