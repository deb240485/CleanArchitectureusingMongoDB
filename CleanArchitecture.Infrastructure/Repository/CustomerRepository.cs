using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CleanArchitecture.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerReposiotry
    {
        private readonly IMongoCollection<Customer> _customer;
        private readonly DatabaseConfiguration _settings;
        
        public CustomerRepository(IOptions<DatabaseConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _customer = database.GetCollection<Customer>(_settings.CustomerCollection);
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customer.Find(c => true).ToListAsync();
        }
        public async Task<Customer> GetByIdAsync(string id)
        {
            return await _customer.Find<Customer>(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(Customer customer)
        {
           await _customer.InsertOneAsync(customer);
        }
        public async Task UpdateAsync(string id, Customer customer)
        {
            await _customer.ReplaceOneAsync(c => c.Id == id, customer);
        }
        public async Task DeleteAsync(string id)
        {
            await _customer.DeleteOneAsync(c => c.Id == id);
        }
    }
}
