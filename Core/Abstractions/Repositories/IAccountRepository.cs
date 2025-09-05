using System.Text.Json.Serialization;
using Core.Models;

namespace Core.Abstractions.Repositories;

public interface IAccountRepository
{
    Task<Guid> Register(User user, string password);
    Task<User> Login(User user, string password);
}
