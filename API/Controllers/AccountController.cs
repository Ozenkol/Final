using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts.Account;
using Core.Abstractions.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")] 
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        // [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<ActionResult<Guid>> Register([FromBody] RegisterRequest registerRequest)   
        {
            var user = new User{
                UserName = registerRequest.UserName,
                Email = registerRequest.Email
            };
            return await _accountService.Register(user, registerRequest.Password);
        }
        [HttpPost]
        // [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)    
        {
            var user = new User
            {
                UserName = loginRequest.UserName
            };
            return await _accountService.Login(user, loginRequest.Password);
        }
    }
}