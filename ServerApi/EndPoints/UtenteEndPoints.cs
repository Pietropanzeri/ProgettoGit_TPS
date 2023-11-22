using System.Security.Cryptography;
using System.Text;
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
                    Username = utenteDto.Username,
                    Password = HashPassword(utenteDto.Password),
                    FotoId = 0
                };
                List<Utente> utentiConNomeUguale = await db.Utenti.Where(u => u.Username == utenteDto.Username).ToListAsync();
                if (!utentiConNomeUguale.IsNullOrEmpty())
                {
                    return Results.Conflict("Utente già esistente");
                }
                
                
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
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Assicurati di avere aggiunto la direttiva using System.Text;
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Converte l'hash in una stringa esadecimale
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
