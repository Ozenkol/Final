using System.Collections.Generic;

namespace DataAccess.Entities;

public class ProductEntity
{
    public Guid ProductId { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid InventoryId { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User = null!;
    public InventoryEntity? Inventory { get; set; }
    public ICollection<ValueEntity> Values { get; set; }  = new List<ValueEntity>();

}
