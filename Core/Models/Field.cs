namespace Core.Models;

public class Field
{
    public int FieldId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int InventoryId { get; set; }
    public Field(int id, string fieldName, int inventoryId)
    {
        InventoryId = id;
        Name = fieldName;
        InventoryId = inventoryId;
    }

    public static (Field field, string Error) Create(int id, string fieldName, int inventoryId)
    {
        var error = string.Empty;
        if (string.IsNullOrEmpty(fieldName))
        {
            error = "Title cannot be empty";
        }
        var inventory = new Field(id, fieldName, inventoryId);
        return (inventory, error);
    }
}
