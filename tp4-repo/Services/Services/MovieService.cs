using tp4.Models;
using tp4.Repositories.RepositoryContracts;
using tp4.Services.ServiceContracts;

namespace tp4.Services.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public IEnumerable<Movies> GetAllMovies()
    {
        return _movieRepository.GetAllWithGenre();
    }

    public Movies GetMovieById(int id)
    {
        return _movieRepository.GetByIdWithGenre(id);
    }

    public void AddMovie(Movies movie)
    {
        _movieRepository.Add(movie);
        _movieRepository.Save();
    }

    public void UpdateMovie(Movies movie)
    {
        _movieRepository.Update(movie);
        _movieRepository.Save();
    }

    public void DeleteMovie(int id)
    {
        var movie = _movieRepository.GetById(id);
        if (movie != null)
        {
            _movieRepository.Remove(movie);
            _movieRepository.Save();
        }
    }

    public IEnumerable<Movies> GetActionMoviesInStock()
    {
        return _movieRepository.GetActionMoviesInStock();
    }

    public IEnumerable<Movies> GetMoviesOrderedByDateAndTitle()
    {
        return _movieRepository.GetMoviesOrderedByDateAndTitle();
    }

    public int GetTotalMoviesCount()
    {
        return _movieRepository.GetAll().Count();
    }

    public IEnumerable<object> GetMoviesWithGenre()
    {
        return _movieRepository.GetAllWithGenre()
            .Select(m => new
            {
                MovieTitle = m.Name,
                GenreName = m.Genre.GenreName,
                Stock = m.Stock,
                DateAdded = m.DateAjoutMovie
            });
    }
}