using Core.Models;

namespace Core.Abstractions.Services;

public interface IAccountService
{
    Task<Guid> Register(User user, string password);
    Task<User> Login(User user, string password);
}
