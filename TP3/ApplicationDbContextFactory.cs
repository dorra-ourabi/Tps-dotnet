

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TP3.Models
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
          public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                // Vérifiez bien le port (XAMPP est souvent 3306, vous avez mis 3307)
                var connectionString = "server=localhost;port=3307;database=tp3;user=root;password=;";

                optionsBuilder.UseMySql(
                    connectionString, 
                    new MySqlServerVersion(new Version(8, 0, 33))
                );

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
    }
