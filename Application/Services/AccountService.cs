using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<string> Login(User user, string password)
    {
        return await _accountRepository.Login(user, password);
    }

    public async Task<Guid> Register(User user, string password)
    {
        return await _accountRepository.Register(user, password);
    }
}
