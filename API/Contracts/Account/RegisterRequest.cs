namespace API.Contracts.Account;

public record RegisterRequest(
    string UserName,
    string Email,
    string Password
);