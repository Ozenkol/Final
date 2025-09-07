namespace Core.Models;

public class Value
{
    public Guid ValueId { get; set; }
    public string FieldValue { get; set; } = string.Empty;

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public Guid FieldId { get; set; }
    public Field? Field { get; set; }

    public Value(Guid id, string value, Guid productId, Guid fieldId)
    {
        ValueId = id;
        FieldValue = value;
        ProductId = productId;
        FieldId = fieldId;
    }
    public static (Value value, string error) Create(Guid id, string fieldValue, Guid productId, Guid fieldId)
    {
        var error = string.Empty;
        if (string.IsNullOrEmpty(fieldValue))
        {
            error = "Value must be filled";
        }
        var value = new Value(id, fieldValue, productId, fieldId);
        return (value, error);
    }
}
