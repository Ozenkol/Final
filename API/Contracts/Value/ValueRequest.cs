namespace API.Contracts.Value;

public record ValueRequest
(
    string value,
    Guid productId,
    Guid fieldId
);