using CustomerAPI.Interfaces;

namespace CustomerAPI.Models
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customerRepo.GetAllCustomers();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _customerRepo.GetCustomerById(id);
        }

        public async Task CreateCustomer(Customer customer)
        {
            await _customerRepo.AddCustomer(customer);
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            return await _customerRepo.UpdateCustomer(customer);
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            return await _customerRepo.DeleteCustomer(id);
        }
    }
}
