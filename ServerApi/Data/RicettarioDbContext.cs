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
        public DbSet<FotoUtente> FotosUtenti => Set<FotoUtente>();

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

            modelBuilder.Entity<Utente>()
                .HasOne(u => u.FotoUtente)
                .WithOne(f => f.Utente)
                .HasForeignKey<FotoUtente>(f => f.UtenteId)
                .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<FotoUtente>()
                .HasKey(f => f.FotoId);

            
            
            
            //DATI DI PARTENZA
            modelBuilder.Entity<Ingrediente>().HasData(
            new Ingrediente
            {
                IngredienteId = 1,
                Nome = "Farina",
                Descrizione = "Prodotto derivato dalla macinazione dei cereali.",
                DataInizio = new DateTime(2023, 1, 1),
                DataFine = new DateTime(2023, 12, 31)
            },
            new Ingrediente
            {
                IngredienteId = 2,
                Nome = "Pomodoro",
                Descrizione = "Frutto comunemente utilizzato in cucina.",
                DataInizio = new DateTime(2023, 3, 15),
                DataFine = new DateTime(2023, 9, 30)
            },
            new Ingrediente
            {
                IngredienteId = 3,
                Nome = "Cipolla",
                Descrizione = "Vegetale dal sapore caratteristico.",
                DataInizio = new DateTime(2023, 2, 10),
                DataFine = new DateTime(2023, 11, 30)
            },
            new Ingrediente
            {
                IngredienteId = 4,
                Nome = "Aglio",
                Descrizione = "Spezia aromatica utilizzata in molti piatti.",
                DataInizio = new DateTime(2023, 4, 5),
                DataFine = new DateTime(2023, 10, 20)
            },
            new Ingrediente
            {
                IngredienteId = 5,
                Nome = "Peperoncino",
                Descrizione = "Spezia piccante usata in diverse cucine del mondo.",
                DataInizio = new DateTime(2023, 5, 20),
                DataFine = new DateTime(2023, 8, 30)
            },
            new Ingrediente
            {
                IngredienteId = 6,
                Nome = "Salmone",
                Descrizione = "Pesce ricco di omega-3.",
                DataInizio = new DateTime(2023, 6, 10),
                DataFine = new DateTime(2023, 12, 15)
            },
            new Ingrediente
            {
                IngredienteId = 7,
                Nome = "Zucchine",
                Descrizione = "Vegetale verde e versatile in cucina.",
                DataInizio = new DateTime(2023, 7, 1),
                DataFine = new DateTime(2023, 9, 15)
            },
            new Ingrediente
            {
                IngredienteId = 8,
                Nome = "Pepe nero",
                Descrizione = "Spezia comune per condire i piatti.",
                DataInizio = new DateTime(2023, 8, 10),
                DataFine = new DateTime(2023, 11, 30)
            },
            new Ingrediente
            {
                IngredienteId = 9,
                Nome = "Riso basmati",
                Descrizione = "Varietà di riso aromatica.",
                DataInizio = new DateTime(2023, 9, 5),
                DataFine = new DateTime(2023, 12, 31)
            },
            new Ingrediente
            {
                IngredienteId = 10,
                Nome = "Melanzane",
                Descrizione = "Vegetale viola ampiamente utilizzato in cucina.",
                DataInizio = new DateTime(2023, 10, 1),
                DataFine = new DateTime(2023, 11, 30)
            },
            new Ingrediente
            {
                IngredienteId = 11,
                Nome = "Pollo",
                Descrizione = "Carne di pollame comunemente consumata.",
                DataInizio = new DateTime(2023, 11, 5),
                DataFine = new DateTime(2023, 12, 31)
            },
            new Ingrediente
            {
                IngredienteId = 12,
                Nome = "Curry in polvere",
                Descrizione = "Miscela di spezie utilizzata nella cucina indiana.",
                DataInizio = new DateTime(2023, 12, 1),
                DataFine = new DateTime(2024, 2, 28)
            },
            new Ingrediente
            {
                IngredienteId = 13,
                Nome = "Spinaci",
                Descrizione = "Vegetale a foglia verde ricco di nutrienti.",
                DataInizio = new DateTime(2024, 1, 10),
                DataFine = new DateTime(2024, 4, 30)
            },
            new Ingrediente
            {
                IngredienteId = 14,
                Nome = "Salsa di soia",
                Descrizione = "Condimento utilizzato in molte cucine asiatiche.",
                DataInizio = new DateTime(2024, 2, 15),
                DataFine = new DateTime(2024, 6, 30)
            },
            new Ingrediente
            {
                IngredienteId = 15,
                Nome = "Pasta di miso",
                Descrizione = "Pasta fermentata utilizzata nella cucina giapponese.",
                DataInizio = new DateTime(2024, 3, 1),
                DataFine = new DateTime(2024, 5, 31)
            },
            new Ingrediente
            {
                IngredienteId = 16,
                Nome = "Pepe verde",
                Descrizione = "Varietà di pepe meno maturo, spesso utilizzato fresco.",
                DataInizio = new DateTime(2024, 4, 5),
                DataFine = new DateTime(2024, 8, 31)
            },
            new Ingrediente
            {
                IngredienteId = 17,
                Nome = "Lenticchie",
                Descrizione = "Legume ricco di proteine.",
                DataInizio = new DateTime(2024, 5, 10),
                DataFine = new DateTime(2024, 9, 30)
            },
            new Ingrediente
            {
                IngredienteId = 18,
                Nome = "Funghi porcini",
                Descrizione = "Varietà di funghi apprezzata in cucina.",
                DataInizio = new DateTime(2024, 6, 1),
                DataFine = new DateTime(2024, 10, 15)
            },
            new Ingrediente
            {
                IngredienteId = 19,
                Nome = "Arancia",
                Descrizione = "Frutto agrumato ricco di vitamina C.",
                DataInizio = new DateTime(2024, 7, 15),
                DataFine = new DateTime(2024, 12, 31)
            },
            new Ingrediente
            {
                IngredienteId = 20,
                Nome = "Cioccolato fondente",
                Descrizione = "Varietà di cioccolato con alto contenuto di cacao.",
                DataInizio = new DateTime(2024, 8, 1),
                DataFine = new DateTime(2024, 11, 30)
            });
            
            modelBuilder.Entity<Ricetta>().HasData(
                new Ricetta
                {
                    RicettaId = 1,
                    UtenteId = 1,
                    Nome = "Pasta al Pomodoro",
                    Preparazione = "Cuocere la pasta e condirla con pomodoro fresco, olio e basilico.",
                    Tempo = 30,
                    Difficolta = 2,
                    DataAggiunta = DateTime.Now,
                    Piatto = 0
                },
                new Ricetta
                {
                    RicettaId = 2,
                    UtenteId = 1,
                    Nome = "Salmone alla Griglia",
                    Preparazione = "Marinare il salmone e cuocerlo alla griglia con limone e erbe aromatiche.",
                    Tempo = 45,
                    Difficolta = 3,
                    DataAggiunta = DateTime.Now,
                    Piatto = 1
                },
                new Ricetta
                {
                    RicettaId = 3,
                    UtenteId = 2,
                    Nome = "Insalata di Riso",
                    Preparazione = "Cuocere il riso, mescolarlo con verdure e condire con olio e aceto.",
                    Tempo = 20,
                    Difficolta = 1,
                    DataAggiunta = DateTime.Now,
                    Piatto = 7
                },
                new Ricetta
                {
                    RicettaId = 4,
                    UtenteId = 2,
                    Nome = "Pollo al Curry",
                    Preparazione = "Cuocere il pollo e aggiungere una salsa di curry speziata.",
                    Tempo = 40,
                    Difficolta = 3,
                    DataAggiunta = DateTime.Now,
                    Piatto = 1
                },
                new Ricetta
                {
                    RicettaId = 5,
                    UtenteId = 3,
                    Nome = "Muffin al Cioccolato",
                    Preparazione = "Preparare l'impasto con farina, cioccolato fondente e zucchero.",
                    Tempo = 25,
                    Difficolta = 2,
                    DataAggiunta = DateTime.Now,
                    Piatto = 2
                }
            );
            
            modelBuilder.Entity<RicettaIngrediente>().HasData(
                new RicettaIngrediente { RicettaId = 1, IngredienteId = 1 }, // Pasta al Pomodoro con Farina
                new RicettaIngrediente { RicettaId = 1, IngredienteId = 2 }, // Pasta al Pomodoro con Pomodoro
                new RicettaIngrediente { RicettaId = 1, IngredienteId = 3 }, // Pasta al Pomodoro con Cipolla

                new RicettaIngrediente { RicettaId = 2, IngredienteId = 6 }, // Salmone alla Griglia con Salmone
                new RicettaIngrediente { RicettaId = 2, IngredienteId = 7 }, // Salmone alla Griglia con Zucchine

                new RicettaIngrediente { RicettaId = 3, IngredienteId = 8 }, // Insalata di Riso con Pepe Nero
                new RicettaIngrediente { RicettaId = 3, IngredienteId = 9 }, // Insalata di Riso con Riso Basmati
                new RicettaIngrediente { RicettaId = 3, IngredienteId = 10 }, // Insalata di Riso con Melanzane

                new RicettaIngrediente { RicettaId = 4, IngredienteId = 11 }, // Pollo al Curry con Pollo
                new RicettaIngrediente { RicettaId = 4, IngredienteId = 12 }, // Pollo al Curry con Curry in Polvere
                
                new RicettaIngrediente { RicettaId = 5, IngredienteId = 20 } // Muffin al Cioccolato con Cioccolato Fondente
            );
            modelBuilder.Entity<Utente>().HasData(
                new Utente
                {
                    UtenteId = 1,
                    Username = "admin",
                    Password = "pIbvhgmpVHahDBTYUgQvew==" //admin
                }, 
                new Utente
                {
                    UtenteId = 2,
                    Username = "Bob456",
                    Password = "pIbvhgmpVHahDBTYUgQvew" //admin
                },
                new Utente
                {
                    UtenteId = 3,
                    Username = "Charlie789",
                    Password = "pIbvhgmpVHahDBTYUgQvew" //admin
                }
            );
            modelBuilder.Entity<UtenteRicettaSalvata>().HasData(
                new UtenteRicettaSalvata { UtenteId = 1, RicettaId = 1 },
                new UtenteRicettaSalvata { UtenteId = 1, RicettaId = 2 },
                new UtenteRicettaSalvata { UtenteId = 2, RicettaId = 2 },
                new UtenteRicettaSalvata { UtenteId = 2, RicettaId = 3 },
                new UtenteRicettaSalvata { UtenteId = 3, RicettaId = 3 },
                new UtenteRicettaSalvata { UtenteId = 3, RicettaId = 4 }
            );
            modelBuilder.Entity<UtenteUtenteSeguito>().HasData(
                new UtenteUtenteSeguito { UtenteId = 1, UtenteSeguitoId = 2 },
                new UtenteUtenteSeguito { UtenteId = 1, UtenteSeguitoId = 3 },
                new UtenteUtenteSeguito { UtenteId = 2, UtenteSeguitoId = 3 }
            );
            modelBuilder.Entity<Foto>().HasData(
                new Foto
                {
                    FotoId = 1,
                    Descrizione = "Immagine della Pasta al Pomodoro",
                    FotoData = new byte[] { },
                    RicettaId = 1
                },
                new Foto
                {
                    FotoId = 2,
                    Descrizione = "Immagine del Salmone alla Griglia",
                    FotoData = new byte[] { },
                    RicettaId = 2
                },
                new Foto
                {
                    FotoId = 3,
                    Descrizione = "Immagine dell'Insalata di Riso",
                    FotoData = new byte[] { },
                    RicettaId = 3
                },
                new Foto
                {
                    FotoId = 4,
                    Descrizione = "Immagine del Pollo al Curry",
                    FotoData = new byte[] { },
                    RicettaId = 4
                },
                new Foto
                {
                    FotoId = 5,
                    Descrizione = "Immagine del Muffin al Cioccolato",
                    FotoData = new byte[] { },
                    RicettaId = 5
                },
                new Foto
                {
                    FotoId = 6,
                    Descrizione = "Immagine del Muffin al Cioccolato 2",
                    FotoData = new byte[] { },
                    RicettaId = 5
                }
            );
            modelBuilder.Entity<FotoUtente>().HasData(
                new FotoUtente
                {
                    FotoId = 1,
                    FotoData = new byte[] { },
                    UtenteId = 1
                },
                new FotoUtente
                {
                    FotoId = 2,
                    FotoData = new byte[] { },
                    UtenteId = 2
                },
                new FotoUtente
                {
                    FotoId = 3,
                    FotoData = new byte[] { },
                    UtenteId = 3
                }
            );
        }
    }
}
