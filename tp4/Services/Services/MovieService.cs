using Microsoft.EntityFrameworkCore;
using tp4.Data;
using tp4.Models;
using tp4.Services.ServiceContracts;
namespace tp4.Services.Services;

public class MovieService : IMovieService
{
    private readonly ApplicationDbContext _context;

    public MovieService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Movies> GetAllMovies()
    {
        return _context.movies
            .Include(m => m.Genre)  // ✅ Charger le genre
            .ToList();
    }

    public Movies GetMovieById(int id)
    {
        return _context.movies
            .Include(m => m.Genre)  // ✅ Charger le genre
            .FirstOrDefault(m => m.Id == id);
    }

    public void AddMovie(Movies movie)
    {
        _context.movies.Add(movie);
        _context.SaveChanges();
    }

    public void UpdateMovie(Movies movie)
    {
        _context.movies.Update(movie);
        _context.SaveChanges();
    }

    public void DeleteMovie(int id)
    {
        var movie = _context.movies.Find(id);
        if (movie != null)
        {
            _context.movies.Remove(movie);
            _context.SaveChanges();
        }
    }

    // LINQ Methods - We'll implement these step by step
    public IEnumerable<Movies> GetActionMoviesInStock()
    {
        return _context.movies
            .Include(m => m.Genre)
            .Where(m => m.Genre.GenreName == "Action" && m.Stock > 0)
            .ToList();
    }

    public IEnumerable<Movies> GetMoviesOrderedByDateAndTitle()
    {
        return _context.movies
            .Include(m => m.Genre)
            .OrderBy(m => m.DateAjoutMovie)      // ✅ Ordre croissant par date
            .ThenBy(m => m.Name)                  // ✅ Puis par titre alphabétique
            .ToList();
    }

    public int GetTotalMoviesCount()
    {
        return _context.movies.Count();
    }

    public IEnumerable<object> GetMoviesWithGenre()
    {
        return _context.movies
            .Join(_context.genres,
                movie => movie.GenresId,
                genre => genre.Id,
                (movie, genre) => new
                {
                    MovieTitle = movie.Name,
                    GenreName = genre.GenreName,
                    Stock = movie.Stock,
                    DateAdded = movie.DateAjoutMovie
                })
            .ToList();
    }
}