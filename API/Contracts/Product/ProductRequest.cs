namespace API.Contracts.Product;

public record ProductRequest(
    string Title,
    string Description,
    Guid InventoryId
);