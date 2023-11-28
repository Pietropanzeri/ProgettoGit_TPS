using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerApi.Data;
using ServerApi.Model;
using ServerApi.ModelDTO;

namespace ServerApi.EndPoints;

public static class RicettaEndPoints
{
    private static Tuple<string, int> _ricettaDelGiorno = Tuple.Create("1990-1-1", 0);
    public static void MapRicetteEndPoints(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/ricette", async (RicettarioDbContext db) =>
             Results.Ok(await db.Ricette.Select(r => new RicettaDTO(r)).ToListAsync()));
        
        endpoint.MapGet("ricette/{utenteId}", async (RicettarioDbContext db, int utenteId) =>
        {
            return Results.Ok(await db.Ricette
                .Where(r => r.UtenteId == utenteId)
                .Select(r => new RicettaDTO(r))
                .ToListAsync());
        });

        endpoint.MapGet("/ricette/novita/{indicePartenza}/{countRicette}", async (RicettarioDbContext db, int indicePartenza, int countRicette) =>
        {
            var dataLimite = DateTime.Now.AddDays(-7);

            var ricettaDtos = await db.Ricette
                .Where(r => r.DataAggiunta > dataLimite)
                .Select(r => new RicettaDTO(r))
                .ToListAsync();

            if (ricettaDtos.IsNullOrEmpty())
                return Results.NoContent();
            var risultato = new List<RicettaDTO>();
            try
            {
                risultato = ricettaDtos.GetRange(indicePartenza, Math.Min(ricettaDtos.Count(), countRicette));

            }
            catch (Exception e)
            {
                risultato = ricettaDtos.GetRange(indicePartenza, ricettaDtos.Count() - indicePartenza);
            }
            return Results.Ok(risultato);
        });

        endpoint.MapGet("ricette/{idIngrediente}/{indicePartenza}/{countIngredienti}", async (RicettarioDbContext db, int idIngrediente, int indicePartenza, int countIngredienti) =>
        {
            var ingredientiRicetta = await db.RicetteIngredienti.Where(ri => ri.IngredienteId == idIngrediente).Join(db.Ricette,
                ri => ri.RicettaId,
                r => r.RicettaId,
                (ri, r) => r).ToListAsync(); ;
            if (ingredientiRicetta.IsNullOrEmpty())
                return Results.NotFound();
            List<RicettaDTO> listaDTORes = new List<RicettaDTO>();
            foreach (var i in ingredientiRicetta)
                listaDTORes.Add(new RicettaDTO(i));

            var risultato = new List<RicettaDTO>();
            try
            {
                risultato = listaDTORes.GetRange(indicePartenza, Math.Min(listaDTORes.Count(), countIngredienti));
            }
            catch (Exception e)
            {
                risultato = listaDTORes.GetRange(indicePartenza, listaDTORes.Count() - indicePartenza);
            }

            return Results.Ok(risultato);
        });

        endpoint.MapGet("ricette/nome/{nomeRicetta}/{indicePartenza}/{countIngredienti}", async (RicettarioDbContext db, string nomeRicetta, int indicePartenza, int countRicette) =>
        {
            var listaRis = await db.Ricette.Where(i => i.Nome.Contains(nomeRicetta)).ToListAsync();
            if (listaRis.IsNullOrEmpty())
                return Results.NotFound();
            List<RicettaDTO> listaDTORes = new List<RicettaDTO>();
            foreach (var i in listaRis)
                listaDTORes.Add(new RicettaDTO(i));

            var risultato = new List<RicettaDTO>();
            try
            {
                risultato = listaDTORes.GetRange(indicePartenza, Math.Min(listaDTORes.Count(), countRicette));
            }
            catch (Exception e)
            {
                risultato = listaDTORes.GetRange(indicePartenza, listaDTORes.Count() - indicePartenza);
            }

            return Results.Ok(risultato);
        });

        endpoint.MapGet("ricette/nome/{nomeRicetta}", async (RicettarioDbContext db, string nomeRicetta) =>
        {
            var listaRis = db.Ricette.Where(i => i.Nome.Contains(nomeRicetta)).ToListAsync();
            if (listaRis.Result.IsNullOrEmpty())
                return Results.NotFound();
            List<RicettaDTO> listaDTORes = new List<RicettaDTO>();
            foreach (var i in listaRis.Result)
                listaDTORes.Add(new RicettaDTO(i));
            return Results.Ok(listaDTORes);

        });

        endpoint.MapGet("ricette/ricettadelgiorno", async (RicettarioDbContext db) =>
        {
            RicettaDTO ricetta;
            Ricetta ricettaCasuale;
            if (_ricettaDelGiorno.Item1 != DateTime.Now.ToShortDateString())
            {
                do
                {
                    ricettaCasuale = db.Ricette.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                } while (ricettaCasuale.RicettaId == _ricettaDelGiorno.Item2);
                ricetta = new RicettaDTO(ricettaCasuale);
                _ricettaDelGiorno = Tuple.Create(DateTime.Now.ToShortDateString(), ricetta.RicettaId);
                return Results.Ok(ricetta);

            }
            ricetta = new RicettaDTO(db.Ricette.Where(r => r.RicettaId == _ricettaDelGiorno.Item2).First());
            return Results.Ok(ricetta);
        });

        endpoint.MapPost("/ricetta", async (RicettarioDbContext db, RicettaDTO ricettaDto) =>
        {
            var ricetta = new Ricetta()
            {
                Nome = ricettaDto.Nome,
                Preparazione = ricettaDto.Preparazione,
                Tempo = ricettaDto.Tempo,
                Difficolta = ricettaDto.Difficolta,
                DataAggiunta = DateTime.Now,
                Piatto = (int)ricettaDto.Piatto,
                UtenteId = ricettaDto.UtenteId,
                RicettaId = ricettaDto.RicettaId
            };
            await db.Ricette.AddAsync(ricetta);
            await db.SaveChangesAsync();
            return Results.Created($"/ricetta/{ricetta.RicettaId}", new RicettaDTO(ricetta));
        });

        endpoint.MapDelete("ricetta/{ricettaId}", async (RicettarioDbContext db, int ricettaId) =>
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

    }
}