using CleanArchitecture.Application.Dto;

namespace CleanArchitecture.Application.Contracts
{
    public interface IUserService
    {
        Task<UserDto?> AuthenticateAsync(string userName, string passwordText);
        Task RegisterAsync(string userName, string password);
        Task<bool> UserAlreadyExistsAsync(string userName);

    }
}
