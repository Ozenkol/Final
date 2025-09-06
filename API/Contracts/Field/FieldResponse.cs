namespace API.Contracts.Field;

public record FieldResponse(
    Guid id,
    string name,
    string? value
);
