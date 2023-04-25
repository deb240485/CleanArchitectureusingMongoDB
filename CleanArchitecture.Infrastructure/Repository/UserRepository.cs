using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CleanArchitecture.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _user;
        private readonly DatabaseConfiguration _settings;

        public UserRepository(IOptions<DatabaseConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _user = database.GetCollection<User>(_settings.UserCollection);
        }

        public async Task CreateUserAsync(User? user)
        {
           await _user.InsertOneAsync(user!);
        }

        public async Task<User?> GetUsersByNameAsync(string userName)
        {
            return await _user.Find<User>(u => u.userName == userName).FirstOrDefaultAsync();
        }

        public async Task<bool> UserAlreadyExistsAsync(string userName)
        {
            return await _user.CountDocumentsAsync(u => u.userName == userName) >= 1 ? true : false;

            //return await _user.Find<User>(u => u.userName == userName).AnyAsync();
        }
    }
}
