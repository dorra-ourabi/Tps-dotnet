using tp4.Models;

namespace tp4.Services.ServiceContracts;

public interface IGenreService
{
    IEnumerable<Genre> GetAllGenres();
    Genre GetGenreById(int id);
    IEnumerable<object> GetTopThreePopularGenres();
}