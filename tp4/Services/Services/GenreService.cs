using tp4.Data;
using tp4.Models;
using tp4.Services.ServiceContracts;
using Microsoft.EntityFrameworkCore;
namespace tp4.Services.Services;

public class GenreService : IGenreService
{
    private readonly ApplicationDbContext _context;

    public GenreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Genre> GetAllGenres()
    {
        return _context.genres.ToList();
    }

    public Genre GetGenreById(int id)
    {
        return _context.genres.Find(id);
    }

    public IEnumerable<object> GetTopThreePopularGenres()
    {
        return _context.genres
            .Include(g => g.movies)
            .Select(g => new
            {
                GenreName = g.GenreName,
                MovieCount = g.movies.Count
            })
            .OrderByDescending(g => g.MovieCount)
            .Take(3)
            .ToList();
    }
}