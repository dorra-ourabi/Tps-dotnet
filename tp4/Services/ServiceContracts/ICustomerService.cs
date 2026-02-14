using tp4.Models;

namespace tp4.Services.ServiceContracts;

public interface ICustomerService
{
    IEnumerable<Customers> GetAllCustomers();
    Customers GetCustomerById(int id);
    IEnumerable<Customers> GetSubscribedCustomersWithHighDiscount();
}