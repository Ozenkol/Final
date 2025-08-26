using System.Text.Json.Serialization;
using Core.Models;

namespace Core.Abstractions.Repositories;

public interface IAccountRepository
{
    Task<int> Register(User user);
    Task<string> Login(User user, string password);
}
