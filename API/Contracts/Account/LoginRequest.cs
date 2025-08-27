namespace API.Contracts.Account;

public record LoginRequest(
    string UserName,
    string Password
);