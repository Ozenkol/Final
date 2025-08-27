namespace Core.Models;

public class Inventory
{

        public Guid InventoryId { get; set; }
        public string InventoryName { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public Inventory(Guid inventoryId, string inventoryName, Guid userId) {
            InventoryId = inventoryId;
            InventoryName = inventoryName;
            UserId = userId;
        }

        public static (Inventory inventory, string Error) Create(Guid inventoryId, string name, Guid userId) {
        var error = string.Empty;
        if (string.IsNullOrEmpty(name)) {
            error = "Title cannot be empty";
        }
        var inventory = new Inventory(inventoryId, name, userId);
        return (inventory, error);
    }
}
