using Microsoft.EntityFrameworkCore;
using ServerApi.Model;

namespace ServerApi.Data
{
    public class RicettarioDbContext : DbContext
    {
        public RicettarioDbContext(DbContextOptions<RicettarioDbContext> options) : base(options) { }
        public DbSet<Ricetta> Ricette => Set<Ricetta>();
        public DbSet<Utente> Utenti => Set<Utente>();
        public DbSet<Ingrediente> Ingredienti => Set<Ingrediente>();
        public DbSet<Foto> Fotos => Set<Foto>();
        public DbSet<RicettaIngrediente> RicetteIngredienti => Set<RicettaIngrediente>();
        public DbSet<UtenteRicettaSalvata> UtentiRicetteSalvate => Set<UtenteRicettaSalvata>();
        public DbSet<UtenteUtenteSeguito> UtenteUtentiSeguiti => Set<UtenteUtenteSeguito>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingrediente>()
                .HasKey(i => i.IngredienteId);

            modelBuilder.Entity<Utente>()
                .HasKey(u => u.UtenteId);
            modelBuilder.Entity<Utente>()
                .HasMany(u => u.Ricetta)
                .WithOne(r => r.Utente)
                .HasForeignKey(r => r.UtenteId);

            modelBuilder.Entity<Ricetta>()
                .HasKey(r => r.RicettaId);
            modelBuilder.Entity<Ricetta>()
                .HasMany(r => r.Fotos)
                .WithOne(f => f.Ricetta)
                .HasForeignKey(f => f.RicettaId);

            modelBuilder.Entity<Foto>()
                .HasKey(r => r.FotoId);

            modelBuilder.Entity<RicettaIngrediente>()
                .HasKey(ri => new { ri.RicettaId, ri.IngredienteId });
            modelBuilder.Entity<RicettaIngrediente>()
                .HasOne(ri => ri.Ricetta)
                .WithMany(r => r.RicettaIngredienti)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RicettaIngrediente>()
                .HasOne(ri => ri.Ingrediente)
                .WithMany(i => i.RicettaIngredienti)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UtenteRicettaSalvata>()
                .HasKey(ri => new { ri.RicettaId, ri.UtenteId });
            modelBuilder.Entity<UtenteRicettaSalvata>()
                .HasOne(ri => ri.Ricetta)
                .WithMany(r => r.UtenteRicetteSalvate)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UtenteRicettaSalvata>()
                .HasOne(ri => ri.Utente)
                .WithMany(u => u.UtenteRicetteSalvate)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UtenteUtenteSeguito>()
                .HasKey(us => new { us.UtenteId, us.UtenteSeguitoId });
            modelBuilder.Entity<UtenteUtenteSeguito>()
                .HasOne(us => us.Utente)
                .WithMany(u => u.UtenteUtentiSeguiti)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UtenteUtenteSeguito>()
                .HasOne(us => us.UtenteSeguito)
                .WithMany(u => u.UtenteUtentiChetiSeguono)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
