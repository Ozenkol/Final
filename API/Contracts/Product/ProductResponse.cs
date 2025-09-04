namespace API.Contracts;

public record ProductResponse(
    Guid id,
    string title,
    string description
);