using Microsoft.EntityFrameworkCore;
using System.Text.Json; // Ajout nécessaire pour le JSON

namespace TP3.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Movies>? movies { get; set; }
    public DbSet<Genres> genres { get; set; }
    public DbSet<Customers> customers { get; set; }
    public DbSet<Membershiptypes> membershiptypes { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Appel de la méthode de base
        base.OnModelCreating(modelBuilder);

        // --- CONFIGURATION DES RELATIONS ---
        modelBuilder.Entity<Customers>()
            .HasMany(c => c.movies)
            .WithMany(m => m.clients);

        modelBuilder.Entity<Customers>()
            .HasOne(c => c.membershiptypes)
            .WithMany(m => m.customers);

        modelBuilder.Entity<Movies>()
            .HasOne(m => m.Genre)
            .WithMany(g => g.movies)
            .HasForeignKey(m => m.GenresId); // Indique la clé étrangère

        modelBuilder.Entity<Membershiptypes>()
            .HasMany(m => m.customers)
            .WithOne(c => c.membershiptypes);


        // --- SEED DATA DEPUIS JSON ---
        try 
        {
            // 1. Lire le fichier JSON
            string MovJson = System.IO.File.ReadAllText("Movies.json");

            // 2. Désérialiser
            var moviesList = JsonSerializer.Deserialize<List<Movies>>(MovJson);

            // 3. Injecter
            if (moviesList != null)
            {
                foreach (Movies m in moviesList)
                {
                    // On s'assure de ne pas injecter les propriétés de navigation (le Genre) 
                    // mais seulement les données brutes
                    modelBuilder.Entity<Movies>().HasData(new Movies {
                        Id = m.Id,
                        Name = m.Name,
                        GenresId = m.GenresId,
                        DateAjoutMovie = m.DateAjoutMovie,
                        ImageFile = m.ImageFile
                    });
                }
            }
        }
        catch (Exception ex)
        {
            // En cas d'erreur de lecture du fichier pendant la migration
            Console.WriteLine("Erreur Seed JSON: " + ex.Message);
        }
    }
}