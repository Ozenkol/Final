namespace API.Contracts.Value;

public record ValueRequest
(
    Guid fieldId,
    string value
);