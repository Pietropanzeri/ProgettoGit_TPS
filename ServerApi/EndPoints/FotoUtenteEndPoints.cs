using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerApi.Data;
using ServerApi.Model;
using ServerApi.ModelDTO;

namespace ServerApi.EndPoints;

public class FotoUtenteEndPoints
{
    public static void MapFotoUtenteEndPoints(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("fotoUtente/{utenteId}", (RicettarioDbContext db, int utenteId) =>
        {
            string folderPath = "ImagesProfilo";
            string[] fileNames = Directory.GetFiles(folderPath);

            string? fotoProfilo = fileNames.Where(f => f.Contains($"_{utenteId}")).FirstOrDefault();

            Utente utente = db.Utenti.Find(utenteId);

            if (string.IsNullOrEmpty(fotoProfilo) && utente == null)
                return Results.NotFound();
            else if(!(utente == null) && string.IsNullOrEmpty(fotoProfilo))
                fotoProfilo = fileNames.Where(f => f.Contains($"default")).FirstOrDefault();

            var imm = File.OpenRead(fotoProfilo);

            return Results.File(imm, "image/jpeg");
        });
        endpoint.MapPost("/fotoUtente", async (RicettarioDbContext db, FotoUtenteDTO fotoUtenteDTO) =>
        {
            string folderPath = "ImagesProfilo";
            Image image;
                
            using (MemoryStream ms = new MemoryStream(fotoUtenteDTO.FotoData))
            {
                image = Image.Load(ms);
            }

            var foto = new FotoUtente()
            {
                FotoId = fotoUtenteDTO.FotoId,
                FotoData = fotoUtenteDTO.FotoData,
                UtenteId = fotoUtenteDTO.UtenteId,
            };
            await db.FotosUtenti.AddAsync(foto);
            await db.SaveChangesAsync();
            string filePath = Path.Combine(folderPath, $"fotoProfilo{foto.FotoId}_{foto.UtenteId}.jpg");

            image.Save(filePath);
            
            return Results.Created($"/fotoUtente/{foto.FotoId}", new FotoUtenteDTO(foto));
        });
        endpoint.MapPut("/changeimage", async (RicettarioDbContext db, FotoUtenteDTO fotoDto) =>
        {
            FotoUtente foto = await db.FotosUtenti.Where(f => f.UtenteId == fotoDto.UtenteId).FirstOrDefaultAsync();
            if (foto == null)
                return Results.NotFound();
            foto.FotoData = fotoDto.FotoData;
            await db.SaveChangesAsync();
            
            Image image;
            string folderPath = "ImagesProfilo";

            using (MemoryStream ms = new MemoryStream(fotoDto.FotoData))
            {
                image = Image.Load(ms);
            }
            string filePath = Path.Combine(folderPath, $"fotoProfilo{foto.FotoId}_{foto.UtenteId}.jpg");
            image.Save(filePath);
            
            var imm = File.OpenRead(filePath);

            return Results.Ok();

        });
    }
}