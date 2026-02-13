using tp4.Data;
using tp4.Models;
using tp4.Services.ServiceContracts;
using Microsoft.EntityFrameworkCore;
namespace tp4.Services.Services;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Customers> GetAllCustomers()
    {
        return _context.customers.ToList();
    }

    public Customers GetCustomerById(int id)
    {
        return _context.customers.Find(id);
    }

    public IEnumerable<Customers> GetSubscribedCustomersWithHighDiscount()
    {
        return _context.customers
            .Include(c => c.membershiptypes)
            .Where(c => c.IsSubscribed == true && c.membershiptypes.DiscountRate > 10)
            .ToList();
    }
}