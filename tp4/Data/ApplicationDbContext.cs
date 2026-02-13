using Microsoft.EntityFrameworkCore;
using tp4.Models;
namespace tp4.Data;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Movies>? movies { get; set; }
    public DbSet<Genre> genres { get; set; }
    public DbSet<Customers> customers { get; set; }
    public DbSet<MembershipTypes> membershiptypes { get; set; }

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

        modelBuilder.Entity<MembershipTypes>()
            .HasMany(m => m.customers)
            .WithOne(c => c.membershiptypes);
    }

}