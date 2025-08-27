using Core.Models;

namespace Core.Abstractions.Services;

public interface IAccountService
{
    Task<int> Register(User user, string password);
    Task<string> Login(User user, string password);
}
