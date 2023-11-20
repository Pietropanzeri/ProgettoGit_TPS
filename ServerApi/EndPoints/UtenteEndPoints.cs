using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerApi.Data;
using ServerApi.Model;
using ServerApi.ModelDTO;

namespace ServerApi.EndPoints
{
    public static class UtenteEndPoints
    {
        public static void MapUtenteEndPoints(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("/utente", async (RicettarioDbContext db) =>
             Results.Ok(await db.Utenti.Select(u => new UtenteDTO(u)).ToListAsync()));

            endpoint.MapGet("/utente/{utenteId}", async (RicettarioDbContext db, int utenteId) =>
                await db.Utenti.FindAsync(utenteId)
                    is Utente utente
                    ? Results.Ok(new UtenteDTO(utente))
                    : Results.NotFound());

            endpoint.MapPost("/utente", async (RicettarioDbContext db, UtenteDTO utenteDto) =>
            {
                var utente = new Utente()
                {
                    UtenteId = utenteDto.UtenteId,
                    Username = utenteDto.Username
                };
                await db.Utenti.AddAsync(utente);
                await db.SaveChangesAsync();
                return Results.Created($"/utente/{utente.UtenteId}", new UtenteDTO(utente));

            });

            endpoint.MapPut("/utente/{utenteId}", async (RicettarioDbContext db, UtenteDTO updateUtente, int utenteId) =>
            {
                var utente = await db.Utenti.FindAsync(utenteId);
                if (utente is null) return Results.NotFound();

                if (!updateUtente.Username.IsNullOrEmpty())
                    utente.Username = updateUtente.Username;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            endpoint.MapDelete("utente/{utenteId}", async (RicettarioDbContext db, int utenteId) =>
            {
                var utente = await db.Utenti.FindAsync(utenteId);
                if (utente is null)
                    return Results.NotFound();

                db.Utenti.Remove(utente);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}
