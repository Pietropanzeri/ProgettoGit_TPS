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
                };
                List<Utente> utentiConNomeUguale =
                    await db.Utenti.Where(u => u.Username == utenteDto.Username).ToListAsync();
                if (!utentiConNomeUguale.IsNullOrEmpty())
                {
                    return Results.Conflict("Utente già esistente");
                }
                await db.Utenti.AddAsync(utente);
                await db.SaveChangesAsync();
                utente.Password = "";
                return Results.Created($"/utente/{utente.UtenteId}", new UtenteDTO(utente));

            });

            endpoint.MapPut("/utente/{utenteId}",
                async (RicettarioDbContext db, UtenteDTO updateUtente, int utenteId) =>
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
            endpoint.MapPost("login",async (RicettarioDbContext db, UtenteDTO utenteDto) =>
            {
                var utente = await db.Utenti.Where(u => u.Username == utenteDto.Username && u.Password == HashPassword(utenteDto.Password)).FirstOrDefaultAsync();
                if (utente is null)
                    return Results.Unauthorized();
                //TODO: Gestire JWT
                utente.Password = "";
                return Results.Ok(new UtenteDTO(utente));
            });
            endpoint.MapGet("ricetteSalvate/{utenteId}", async (RicettarioDbContext db, int utenteId) =>
            {
                var utente = await db.Utenti.FindAsync(utenteId);
                if (utente is null)
                    return Results.NotFound();
                var ricetteSalvateUtente = await db.UtentiRicetteSalvate.Where(urs => urs.UtenteId == utenteId).Join(db.Ricette,
                    urs => urs.RicettaId,
                    r => r.RicettaId,
                    (urs, r) => r).Select(r=> new RicettaDTO(r)).ToListAsync();
                return Results.Ok(ricetteSalvateUtente);
            });
            endpoint.MapPost("salvaricetta/{ricettaId}", async(RicettarioDbContext db, UtenteDTO utenteDTO, int ricettaId) =>
            {
                var ricetta = await db.Ricette.FindAsync(ricettaId);
                var utenteRicettaSalvata = await db.UtentiRicetteSalvate.Where(urs => urs.UtenteId == utenteDTO.UtenteId && urs.RicettaId == ricettaId).FirstOrDefaultAsync();
                if (ricetta != null)
                {
                    if (utenteRicettaSalvata == null)
                    {
                        await db.UtentiRicetteSalvate.AddAsync(new UtenteRicettaSalvata()
                            { UtenteId = utenteDTO.UtenteId, RicettaId = ricettaId });
                        await db.SaveChangesAsync();
                        return Results.Ok("Ricetta Salvata");
                    }
                    else
                    {
                        db.UtentiRicetteSalvate.Remove(utenteRicettaSalvata);
                        await db.SaveChangesAsync();
                        return Results.Ok("Ricetta Rimossa dai Salvati");
                    }
                }
                return Results.Conflict("Ricetta non esistente");
            });
        }
        
        private static string HashPassword(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}
