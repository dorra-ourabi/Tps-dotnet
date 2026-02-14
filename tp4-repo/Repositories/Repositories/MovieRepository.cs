using Microsoft.EntityFrameworkCore;
using tp4.Data;
using tp4.Models;
using tp4.Repositories.RepositoryContracts;

namespace tp4.Repositories.Repositories;

public class MovieRepository : GenericRepository<Movies>, IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Movies> GetAllWithGenre()
    {
        return _context.movies
            .Include(m => m.Genre)
            .ToList();
    }

    public Movies GetByIdWithGenre(int id)
    {
        return _context.movies
            .Include(m => m.Genre)
            .FirstOrDefault(m => m.Id == id);
    }

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
            .OrderBy(m => m.DateAjoutMovie)
            .ThenBy(m => m.Name)
            .ToList();
    }
}