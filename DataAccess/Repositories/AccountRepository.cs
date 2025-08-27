using Core.Abstractions.Repositories;
using Core.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;   
using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;

namespace DataAccess.Repositories;

public class AccountRepository: IAccountRepository
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IConfiguration _configuration;

    public AccountRepository(UserManager<UserEntity> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> Login(User user, string password)
    {
        var userEntity = await _userManager.FindByNameAsync(user.UserName);

        if (userEntity == null || !await _userManager.CheckPasswordAsync(userEntity, password))
        {
            throw new Exception("Invalid login attempt.");
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"])),
            SecurityAlgorithms.HmacSha256
        );

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userEntity.UserName),
            new Claim(ClaimTypes.NameIdentifier, userEntity.Id),
        };

        var jwtObject = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(5), // 300 seconds
            signingCredentials: signingCredentials
        );

        var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtObject);

        return jwtString;
    }

    public async Task<Guid> Register(User user, string password)
    {
        var userEntity = new UserEntity
        {
            UserName = user.UserName,
            Email = user.Email
        };
        var result = await _userManager.CreateAsync(
            userEntity, password);   
        if (result.Succeeded)            
        {
            return new Guid(userEntity.Id);
        }
        else {
            throw new Exception(
            string.Format("Error: {0}", string.Join(" ", 
                result.Errors.Select(e => e.Description))));
        }
    }
}
