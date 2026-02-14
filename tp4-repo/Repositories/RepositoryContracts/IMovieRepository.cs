using tp4.Models;

namespace tp4.Repositories.RepositoryContracts;

public interface IMovieRepository : IGenericRepository<Movies>
{
    IEnumerable<Movies> GetAllWithGenre();
    Movies GetByIdWithGenre(int id);
    IEnumerable<Movies> GetActionMoviesInStock();
    IEnumerable<Movies> GetMoviesOrderedByDateAndTitle();
}