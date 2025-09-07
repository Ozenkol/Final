namespace Core.Models;

public class Inventory
{

    public Guid InventoryId { get; set; }
    public string InventoryName { get; set; } = string.Empty;
    public Guid UserId { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Field> Fields { get; set; } = new List<Field>();

    public Inventory(Guid inventoryId, string inventoryName, Guid userId, ICollection<Field> fields)
    {
        InventoryId = inventoryId;
        InventoryName = inventoryName;
        UserId = userId;
        Fields = fields;
    }

    public static (Inventory inventory, string Error) Create(Guid inventoryId, string name, Guid userId, ICollection<Field>? fields = null)
    {
        var error = string.Empty;
        if (string.IsNullOrEmpty(name))
        {
            error = "Title cannot be empty";
        }
        
        var inventory = new Inventory(
            inventoryId, 
            name, 
            userId,
            fields);
        return (inventory, error);
    }
}
