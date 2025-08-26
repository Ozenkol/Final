using System.Collections.Generic;

namespace DataAccess.Entities;

public class ProductEntity
{
    public int ProductId {get;set;}
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int InventoryId { get; set; }
    public int UserId { get; set; }
    public UserEntity User = null!;
    public InventoryEntity ?Inventory { get; set; }
    public ICollection<ValueEntity> Values = new List<ValueEntity>();

}
