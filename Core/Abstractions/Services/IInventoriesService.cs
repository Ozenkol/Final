using System.Diagnostics.Metrics;
using Core.Models;

namespace Core.Abstractions.Services;

public interface IInventoriesService
{
    Task<Inventory> GetInventory(Guid id);
    Task<List<Inventory>> GetInventoryList();
    Task<List<Inventory>> GetUserInventoryList(Guid userId);
    Task<Inventory> CreateInventory(Inventory inventory);
    Task<Guid> DeleteInventory(Guid id);
    Task<Guid> UpdateInventory(Inventory inventory);
}
