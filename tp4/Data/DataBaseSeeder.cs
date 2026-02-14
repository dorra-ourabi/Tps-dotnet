using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using tp4.Data;
using tp4.Models;

namespace tp4.Data;

public class DatabaseSeeder
{
    public static void SeedData(ApplicationDbContext context)
    {
        if (context.genres.Any())
        {
            Console.WriteLine("La base de données contient déjà des données. Aucune insertion.");
            return;
        }

        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData.json");
        
        if (!File.Exists(jsonFilePath))
        {
            Console.WriteLine("Le fichier SeedData.json est introuvable!");
            return;
        }

        var jsonData = File.ReadAllText(jsonFilePath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var seedData = JsonSerializer.Deserialize<SeedDataModel>(jsonData, options);

        if (seedData == null)
        {
            Console.WriteLine("Erreur lors de la lecture du fichier JSON.");
            return;
        }

        // ✅ Utiliser une connexion SQL directe pour IDENTITY_INSERT
        var connectionString = context.Database.GetConnectionString();
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Insérer Genres
            if (seedData.Genres != null && seedData.Genres.Any())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SET IDENTITY_INSERT genres ON";
                    command.ExecuteNonQuery();
                }

                foreach (var genre in seedData.Genres)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO genres (Id, GenreName) VALUES (@Id, @GenreName)";
                        command.Parameters.AddWithValue("@Id", genre.Id);
                        command.Parameters.AddWithValue("@GenreName", genre.GenreName);
                        command.ExecuteNonQuery();
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SET IDENTITY_INSERT genres OFF";
                    command.ExecuteNonQuery();
                }
                Console.WriteLine($"{seedData.Genres.Count} genres insérés.");
            }

            // Insérer MembershipTypes
            if (seedData.MembershipTypes != null && seedData.MembershipTypes.Any())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SET IDENTITY_INSERT membershiptypes ON";
                    command.ExecuteNonQuery();
                }

                foreach (var membership in seedData.MembershipTypes)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO membershiptypes (Id, SignUpFee, DurationInMonth, DiscountRate) VALUES (@Id, @SignUpFee, @DurationInMonth, @DiscountRate)";
                        command.Parameters.AddWithValue("@Id", membership.Id);
                        command.Parameters.AddWithValue("@SignUpFee", membership.SignUpFee);
                        command.Parameters.AddWithValue("@DurationInMonth", membership.DurationInMonth);
                        command.Parameters.AddWithValue("@DiscountRate", membership.DiscountRate);
                        command.ExecuteNonQuery();
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SET IDENTITY_INSERT membershiptypes OFF";
                    command.ExecuteNonQuery();
                }
                Console.WriteLine($"{seedData.MembershipTypes.Count} membership types insérés.");
            }

            // Insérer Movies
            if (seedData.Movies != null && seedData.Movies.Any())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SET IDENTITY_INSERT movies ON";
                    command.ExecuteNonQuery();
                }

                foreach (var movie in seedData.Movies)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO movies (Id, Name, ImageFile, DateAjoutMovie, Stock, GenresId) VALUES (@Id, @Name, @ImageFile, @DateAjoutMovie, @Stock, @GenresId)";
                        command.Parameters.AddWithValue("@Id", movie.Id);
                        command.Parameters.AddWithValue("@Name", movie.Name);
                        command.Parameters.AddWithValue("@ImageFile", movie.ImageFile ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DateAjoutMovie", movie.DateAjoutMovie ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Stock", movie.Stock);
                        command.Parameters.AddWithValue("@GenresId", movie.GenresId);
                        command.ExecuteNonQuery();
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SET IDENTITY_INSERT movies OFF";
                    command.ExecuteNonQuery();
                }
                Console.WriteLine($"{seedData.Movies.Count} films insérés.");
            }

            // Insérer Customers
            if (seedData.Customers != null && seedData.Customers.Any())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SET IDENTITY_INSERT customers ON";
                    command.ExecuteNonQuery();
                }

                foreach (var customer in seedData.Customers)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO customers (Id, Name, IsSubscribed, membershiptypesId) VALUES (@Id, @Name, @IsSubscribed, @MembershipTypesId)";
                        command.Parameters.AddWithValue("@Id", customer.Id);
                        command.Parameters.AddWithValue("@Name", customer.Name);
                        command.Parameters.AddWithValue("@IsSubscribed", customer.IsSubscribed);
                        command.Parameters.AddWithValue("@MembershipTypesId", customer.membershiptypesId);
                        command.ExecuteNonQuery();
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SET IDENTITY_INSERT customers OFF";
                    command.ExecuteNonQuery();
                }
                Console.WriteLine($"{seedData.Customers.Count} clients insérés.");
            }

            connection.Close();
        }

        Console.WriteLine("✅ Base de données remplie avec succès!");
    }
}

public class SeedDataModel
{
    public List<Genre>? Genres { get; set; }
    public List<MembershipTypes>? MembershipTypes { get; set; }
    public List<Movies>? Movies { get; set; }
    public List<Customers>? Customers { get; set; }
}