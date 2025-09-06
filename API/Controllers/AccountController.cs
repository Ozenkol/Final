using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts;
using API.Contracts.Account;
using Core.Abstractions.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<ActionResult<Guid>> Register([FromBody] RegisterRequest registerRequest)
        {
            var user = new User
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email
            };
            return await _accountService.Register(user, registerRequest.Password);
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var user = new User
            {
                UserName = loginRequest.UserName
            };
            var loginUser = await _accountService.Login(user, loginRequest.Password);
            if (!string.IsNullOrEmpty(loginUser.Token))
            {
                HttpContext.Response.Cookies.Append(".AspNetCore.Application.Id", loginUser.Token,
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromMinutes(60)
                });
                var userRecord = new UserResponse(loginUser.UserName);
                return Ok(userRecord);
            }
            return Forbid();
        }
    }
}