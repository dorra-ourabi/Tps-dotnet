
namespace tp4.Repositories.RepositoryContracts;

public interface IGenericRepository<T> where T : class
{
    // Lecture
    IEnumerable<T> GetAll();
    T GetById(int id);
    IEnumerable<T> Find(Func<T, bool> predicate);
    
    // Écriture
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    
    // Sauvegarde
    void Save();
}
