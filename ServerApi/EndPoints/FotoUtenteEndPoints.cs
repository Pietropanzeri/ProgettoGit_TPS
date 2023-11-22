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
        endpoint.MapGet("fotoUtente/{utenteId}",  (RicettarioDbContext db, int utenteId) =>
        {
            string folderPath = "ImagesProfilo";
            string[] fileNames = Directory.GetFiles(folderPath);

            string? fotoProfilo = fileNames.Where(f => f.Contains($"_{utenteId}")).FirstOrDefault();

            if (string.IsNullOrEmpty(fotoProfilo))
                return Results.NotFound();

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
    }
}