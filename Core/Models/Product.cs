namespace Core.Models;

public class Product
{
    public Guid ProductId { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid InventoryId { get; set; }
    public Inventory? Inventory { get; set; }
    public ICollection<Value> Values { get; set; } = new List<Value>();

    public Product(Guid id, string title, string description, Guid userId, Guid inventoryId, ICollection<Value> values)
    {
        ProductId = id;
        Title = title;
        Description = description;
        UserId = userId;
        InventoryId = inventoryId;
        Values = values;
    }

    public static (Product product, string error) Create(Guid productId, string title, string description, Guid userId, Guid inventoryId, ICollection<Value>? values = null)
    {
        var error = string.Empty;
        if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(description))
        {
            error = "Title and description are empty";
        }
        var product = new Product(productId, title, description, userId, inventoryId, values);
        return (product, error);
    }


}
