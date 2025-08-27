namespace Core.Models;

public class Field
{
    public Guid FieldId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid InventoryId { get; set; }
    public Field(Guid fieldId, string fieldName, Guid inventoryId)
    {
        InventoryId = fieldId;
        Name = fieldName;
        InventoryId = inventoryId;
    }

    public static (Field field, string Error) Create(Guid id, string fieldName, Guid inventoryId)
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
