using tp4.Models;
using tp4.Repositories.RepositoryContracts;
using tp4.Services.ServiceContracts;

namespace tp4.Services.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public IEnumerable<Genre> GetAllGenres()
    {
        return _genreRepository.GetAll();
    }

    public Genre GetGenreById(int id)
    {
        return _genreRepository.GetById(id);
    }

    public IEnumerable<object> GetTopThreePopularGenres()
    {
        return _genreRepository.GetTopThreePopularGenres();
    }
}