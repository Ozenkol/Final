namespace Core.Models;

public class Product
{
    public Guid ProductId {get;set;}
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid InventoryId { get; set; }
    public Inventory ?Inventory { get; set; }
    public ICollection<Value> Values = new List<Value>();

    public Product(Guid id, string title, string description, Guid inventoryId) {
        ProductId = id;
        Title = title;
        Description = description;
        InventoryId = inventoryId;
    }

    public static (Product product, string error) Create(Guid productId, string title, string description, Guid inventoryId) {
        var error = string.Empty;
        if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(description)) {
            error = "Title and description are empty";
        }
        var product = new Product(productId, title, description, inventoryId);
        return (product, error);
    }


}
