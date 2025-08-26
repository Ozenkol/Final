namespace Core.Models;

public class Value
{
        public int ValueId {get;set;}
        public string FieldValue { get; set; } = string.Empty;

        public int ProductId { get; set; }
        public Product ?Product { get; set; }

        public int FieldId { get; set; }
        public Field ?Field { get; set; }

        public Value(int id, string value, int productId, int fieldId) {
                ValueId = id;
                FieldValue = value;
                ProductId = productId;
                FieldId = fieldId;
        }
        public static (Value value, string error) Create(int id, string fieldValue, int productId, int fieldId) {
        var error = string.Empty;
        if (string.IsNullOrEmpty(fieldValue)) {
            error = "Value must be filled";
        }
        var value = new Value(id, fieldValue, productId, fieldId);
        return (value, error);
    }
}
