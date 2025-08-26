using Core.Models;

namespace Core.Abstractions.Repositories;

public interface IInventoriesRepository
{
    Task<Inventory> GetInventory(int id);
    Task<List<Inventory>> GetInventoryList();
    Task<List<Inventory>> GetUserInventoryList(int userId);
    Task<int> CreateInventory(Inventory inventory);
    Task<int> DeleteInventory(int id);
    Task<int> UpdateInventory(Inventory inventory);

}
