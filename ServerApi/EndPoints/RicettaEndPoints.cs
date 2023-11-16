using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerApi.Data;
using ServerApi.Model;
using ServerApi.ModelDTO;

namespace ServerApi.EndPoints;

public static class RicettaEndPoints
{
    public static void MapRicetteEndPoints(this WebApplication app)
    {
        app.MapGet("/ricette", async (RicettarioDbContext db ) =>
             Results.Ok(await db.Ricette.Select(r => new RicettaDTO(r)).ToListAsync()));
        
        app.MapPost("/ricetta", async (RicettarioDbContext db, RicettaDTO ricettaDto) =>
        {
            var ricetta = new Ricetta()
            {
                Nome = ricettaDto.Nome, 
                Preparazione = ricettaDto.Preparazione, 
                Tempo = ricettaDto.Tempo, 
                Difficolta = ricettaDto.Difficolta, 
                Piatto = (int)ricettaDto.Piatto,
                UtenteId = ricettaDto.UtenteId,
                RicettaId = ricettaDto.RicettaId
            };
            await db.Ricette.AddAsync(ricetta);
            await db.SaveChangesAsync();
            return Results.Created($"/ricetta/{ricetta.RicettaId}", new RicettaDTO(ricetta));
        });
        app.MapDelete("ricetta/{ricettaId}", async (RicettarioDbContext db, int ricettaId) =>
        {
            var ricetta = await db.Ricette.FindAsync(ricettaId);
            if (ricetta is null)
            {
                return Results.NotFound();
            }
            db.Ricette.Remove(ricetta);
            await db.SaveChangesAsync();
            return Results.Ok();
        });
        app.MapGet("ricette/nome/{nomeRicetta}", async (RicettarioDbContext db, string nomeRicetta) =>
        {
            var listaRis = db.Ricette.Where(i => i.Nome.Contains(nomeRicetta)).ToListAsync();
            if (listaRis.Result.IsNullOrEmpty())
                return Results.NotFound();
            List<RicettaDTO> listaDTORes = new List<RicettaDTO>();
            foreach (var i in listaRis.Result)
                listaDTORes.Add(new RicettaDTO(i));
            return Results.Ok(listaDTORes);
        });
    }
    
}