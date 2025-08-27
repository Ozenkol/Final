namespace API.Contracts.Field;

public record FieldRequest(
    string Name,
    Guid InventoryId
);
