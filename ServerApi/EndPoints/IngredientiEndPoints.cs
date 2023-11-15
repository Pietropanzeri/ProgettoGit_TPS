using Microsoft.EntityFrameworkCore;
using ServerApi.Data;

namespace ServerApi.EndPoints
{
    public static class IngredientiEndPoints
    {
        public static void MapRicettarioEndpoints(this WebApplication app)
        {
            app.MapGet("/ingredienti", async(RicettarioDbContext db) =>
                Results.Ok(await db.Ingredienti.ToListAsync()));
        }

    }
}
