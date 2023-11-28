using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerApi.Data;
using ServerApi.Model;
using ServerApi.ModelDTO;
using SixLabors.ImageSharp.Formats;
using System.Drawing;
using System.Drawing.Imaging;

namespace ServerApi.EndPoints
{
    public static class FotoEndPoints
    {
        public static void MapFotoEndPoints(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("/foto", async (RicettarioDbContext db) =>
             Results.Ok(await db.Fotos.Select(r => new FotoDTO(r)).ToListAsync()));

            endpoint.MapGet("/foto/{fotoId}", async (RicettarioDbContext db, int fotoId) =>
                await db.Fotos.FindAsync(fotoId)
                    is Foto foto
                    ? Results.Ok(new FotoDTO(foto))
                    : Results.NotFound());

            endpoint.MapGet("foto/ricetta/{ricettaId}", async (RicettarioDbContext db, int ricettaId) =>
            {
                var fotoRicetta = await db.Fotos.Where(f => f.RicettaId == ricettaId).ToListAsync();
                if (fotoRicetta.IsNullOrEmpty())
                    return Results.NotFound();
                List<FotoDTO> listaDTORes = new List<FotoDTO>();
                foreach (var i in fotoRicetta)
                    listaDTORes.Add(new FotoDTO(i));
                return Results.Ok(listaDTORes);
            });

            endpoint.MapGet("foto/ricetta/{ricettaId}/primaimmagine",  (RicettarioDbContext db, int ricettaId) =>
            {
                string folderPath = "Images";
                string[] fileNames = Directory.GetFiles(folderPath);

                string? immagineDellaRicetta = fileNames.Where(f => f.Contains($"_{ricettaId}")).FirstOrDefault();

                if (string.IsNullOrEmpty(immagineDellaRicetta))
                    return Results.NotFound();

                var imm = File.OpenRead(immagineDellaRicetta);

                return Results.File(imm, "image/jpeg");
            });
            endpoint.MapGet("foto/ricetta/{ricettaId}/numero",  (RicettarioDbContext db, int ricettaId) =>
            {
                string folderPath = "Images";
                string[] fileNames = Directory.GetFiles(folderPath);

                List<string?> immaginiDellaRicetta = fileNames.Where(f => f.Contains($"_{ricettaId}")).ToList();

                if (immaginiDellaRicetta.IsNullOrEmpty())
                    return Results.NotFound();

                return Results.Ok(immaginiDellaRicetta.Count);
            });
            endpoint.MapGet("foto/ricetta/{ricettaId}/{numeroFoto}",  (RicettarioDbContext db, int ricettaId, int numerofoto) =>
            {
                string folderPath = "Images";
                string[] fileNames = Directory.GetFiles(folderPath);

                List<string?> immagineDellaRicetta = fileNames.Where(f => f.Contains($"_{ricettaId}")).ToList();

                if (immagineDellaRicetta.IsNullOrEmpty())
                    return Results.NotFound();

                FileStream imm;
                try
                {
                    imm = File.OpenRead(immagineDellaRicetta[numerofoto - 1]);

                    return Results.File(imm, "image/jpeg");
                }
                catch
                {
                    return Results.NotFound();
                }
            });
            
            endpoint.MapPost("/foto", async (RicettarioDbContext db, FotoDTO fotoDto) =>
            {
                string folderPath = "Images";
                Image image;
                
                using (MemoryStream ms = new MemoryStream(fotoDto.FotoData))
                {
                    image = Image.Load(ms);
                }

                var foto = new Foto()
                {
                    FotoId = fotoDto.FotoId,
                    Descrizione = fotoDto.Descrizione,
                    FotoData = fotoDto.FotoData,
                    RicettaId = fotoDto.RicettaId
                };
                await db.Fotos.AddAsync(foto);
                await db.SaveChangesAsync();
                string filePath = Path.Combine(folderPath, $"immagine{foto.FotoId}_{foto.RicettaId}.jpg"); // Sostituisci con il percorso e il nome del tuo file desiderato

                // Salva l'immagine come file JPEG
                image.Save(filePath);
                
                
                return Results.Created($"/foto/{foto.FotoId}", new FotoDTO(foto));
            });

            endpoint.MapPut("/foto/{fotoId}", async (RicettarioDbContext db, FotoDTO updateFoto, int fotoId) =>
            {
                var foto = await db.Fotos.FindAsync(fotoId);
                if (foto is null) return Results.NotFound();

                if (!updateFoto.Descrizione.IsNullOrEmpty())
                    foto.Descrizione = updateFoto.Descrizione;
                if (!updateFoto.FotoData.IsNullOrEmpty())
                    foto.FotoData = updateFoto.FotoData;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            endpoint.MapDelete("foto/{fotoId}", async (RicettarioDbContext db, int fotoId) =>
            {
                var foto = await db.Fotos.FindAsync(fotoId);
                if (foto is null)
                    return Results.NotFound();

                db.Fotos.Remove(foto);
                await db.SaveChangesAsync();
                return Results.Ok();
                //TODO: delete file immagine
            });
        }
    }
}
