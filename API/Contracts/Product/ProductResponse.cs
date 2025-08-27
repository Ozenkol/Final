namespace API.Contracts;

public record ProductResponse(
    Guid ProuctId,
    string Title,
    string Description
);