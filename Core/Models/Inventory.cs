namespace Core.Models;

public class Inventory
{

        public int InventoryId { get; set; }
        public string InventoryName { get; set; } = string.Empty;

        public int UserId { get; set; }

    public Inventory(int id, string inventoryName) {
            InventoryId = id;
            InventoryName = inventoryName;
        }

        public static (Inventory inventory, string Error) Create(int inventoryId, string name) {
        var error = string.Empty;
        if (string.IsNullOrEmpty(name)) {
            error = "Title cannot be empty";
        }
        var inventory = new Inventory(inventoryId, name);
        return (inventory, error);
    }
}
