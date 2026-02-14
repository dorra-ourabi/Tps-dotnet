using Microsoft.EntityFrameworkCore;
using tp4.Data;
using tp4.Models;
using tp4.Repositories.RepositoryContracts;

namespace tp4.Repositories.Repositories;

public class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    private readonly ApplicationDbContext _context;

    public GenreRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
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