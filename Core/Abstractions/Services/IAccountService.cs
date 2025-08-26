namespace Core.Abstractions.Services;

public interface IAccountService
{
    Task<int> Register();
    Task<string> Login();
}
