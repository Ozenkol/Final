using Core.Models;

namespace Core.Abstractions.Repositories;

public interface IInventoriesRepository
{
    Task<Inventory> GetInventory(Guid id);
    Task<List<Inventory>> GetInventoryList();
    Task<List<Inventory>> GetUserInventoryList(Guid userId);
    Task<Guid> CreateInventory(Inventory inventory);
    Task<Guid> DeleteInventory(Guid id);
    Task<Guid> UpdateInventory(Inventory inventory);

}
