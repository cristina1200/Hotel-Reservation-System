using HotelReservationSystem.Data;
using HotelReservationSystem.Models;

namespace HotelReservationSystem.ResourceAccess
{
    public class CustomerResourceAccess
    {
        private readonly HotelDbContext dbContext;

        public CustomerResourceAccess(HotelDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int GetNextId()
        {
            if (!dbContext.Customers.Any())
            {
                return 1;
            }

            return dbContext.Customers.Max(customer => customer.Id) + 1;
        }

        public void AddCustomer(Customer customer)
        {
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
        }

        public Customer? GetCustomerById(int id)
        {
            return dbContext.Customers.FirstOrDefault(customer => customer.Id == id);
        }

        public List<Customer> GetAllCustomers()
        {
            return dbContext.Customers.ToList();
        }
    }
}