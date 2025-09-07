using System.Reflection.Metadata.Ecma335;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Models;
using DataAccess.Repositories;

namespace Application.Services;

public class InventoriesService : IInventoriesService
{
    private readonly IInventoriesRepository _inventoriesRepository;
    public InventoriesService(IInventoriesRepository inventoriesRepository)
    {
        _inventoriesRepository = inventoriesRepository;
    }

    public async Task<Guid> CreateInventory(Inventory inventory)
    {
        return await _inventoriesRepository.CreateInventory(inventory);
    }

    public async Task<Guid> DeleteInventory(Guid id)
    {
        return await _inventoriesRepository.DeleteInventory(id);
    }

    public async Task<Inventory> GetInventory(Guid id)
    {
        return await _inventoriesRepository.GetInventory(id);
    }

    public async Task<List<Inventory>> GetInventoryList()
    {
        return await _inventoriesRepository.GetInventoryList();
    }

    public async Task<List<Inventory>> GetUserInventoryList(Guid userId)
    {
        return await _inventoriesRepository.GetUserInventoryList(userId);
    }

    public async Task<Guid> UpdateInventory(Inventory inventory)
    {
        return await _inventoriesRepository.UpdateInventory(inventory);
    }
}
