using tp4.Models;

namespace tp4.Repositories.RepositoryContracts;

public interface IGenreRepository : IGenericRepository<Genre>
{
    IEnumerable<object> GetTopThreePopularGenres();
}