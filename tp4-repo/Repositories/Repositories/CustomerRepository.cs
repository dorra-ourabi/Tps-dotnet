using Microsoft.EntityFrameworkCore;
using tp4.Data;
using tp4.Models;
using tp4.Repositories.RepositoryContracts;

namespace tp4.Repositories.Repositories;

public class CustomerRepository : GenericRepository<Customers>, ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Customers> GetAllWithMembership()
    {
        return _context.customers
            .Include(c => c.membershiptypes)
            .ToList();
    }

    public Customers GetByIdWithMembership(int id)
    {
        return _context.customers
            .Include(c => c.membershiptypes)
            .FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Customers> GetSubscribedCustomersWithHighDiscount()
    {
        return _context.customers
            .Include(c => c.membershiptypes)
            .Where(c => c.IsSubscribed == true && c.membershiptypes.DiscountRate > 10)
            .ToList();
    }
}