using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace tp2.Models
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = "server=localhost;port=3307;database=mydb;user=root;password=;";

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8,0,33)));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}