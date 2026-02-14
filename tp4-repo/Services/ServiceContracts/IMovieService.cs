using tp4.Models;

namespace tp4.Services.ServiceContracts;

public interface IMovieService
{
    IEnumerable<Movies> GetAllMovies();
    Movies GetMovieById(int id);
    void AddMovie(Movies movie);
    void UpdateMovie(Movies movie);
    void DeleteMovie(int id);
    
    // LINQ Methods (we'll implement these next)
    IEnumerable<Movies> GetActionMoviesInStock();
    IEnumerable<Movies> GetMoviesOrderedByDateAndTitle();
    int GetTotalMoviesCount();
    IEnumerable<object> GetMoviesWithGenre();
}