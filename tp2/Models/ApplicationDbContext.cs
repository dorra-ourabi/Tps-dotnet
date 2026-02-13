using Microsoft.EntityFrameworkCore;
namespace tp2.Models;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions options): base(options){}
    public DbSet<Movie>? movies { get; set; }
    public DbSet<Genre> genres { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Genre)
            .WithMany(g => g.Movies)
            .HasForeignKey(m => m.Id);
    }
}   