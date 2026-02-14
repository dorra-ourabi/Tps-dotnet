using tp4.Models;
using tp4.Repositories.RepositoryContracts;
using tp4.Services.ServiceContracts;

namespace tp4.Services.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IEnumerable<Customers> GetAllCustomers()
    {
        return _customerRepository.GetAllWithMembership();
    }

    public Customers GetCustomerById(int id)
    {
        return _customerRepository.GetByIdWithMembership(id);
    }

    public IEnumerable<Customers> GetSubscribedCustomersWithHighDiscount()
    {
        return _customerRepository.GetSubscribedCustomersWithHighDiscount();
    }
}