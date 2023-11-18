using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerApi.Data;
using ServerApi.Model;
using ServerApi.ModelDTO;

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

            endpoint.MapPost("/foto", async (RicettarioDbContext db, FotoDTO fotoDto) =>
            {
                var foto = new Foto()
                {
                    FotoId = fotoDto.FotoId,
                    Descrizione = fotoDto.Descrizione,
                    FotoData = fotoDto.FotoData,
                    RicettaId = fotoDto.RicettaId
                };
                await db.Fotos.AddAsync(foto);
                await db.SaveChangesAsync();
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
            });
        }
    }
}
