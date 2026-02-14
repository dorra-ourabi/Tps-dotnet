using tp4.Models;

namespace tp4.Repositories.RepositoryContracts;

public interface ICustomerRepository : IGenericRepository<Customers>
{
    IEnumerable<Customers> GetAllWithMembership();
    Customers GetByIdWithMembership(int id);
    IEnumerable<Customers> GetSubscribedCustomersWithHighDiscount();
}