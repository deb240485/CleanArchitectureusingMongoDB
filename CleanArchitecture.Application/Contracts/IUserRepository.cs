using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetUsersByNameAsync(string userName);
        Task<bool> UserAlreadyExistsAsync(string userName);
        Task CreateUserAsync(User? user);
    }
}
