using Microsoft.EntityFrameworkCore;
using Core.Abstractions.Repositories;
using Core.Models;
using DataAccess.Entities;

namespace DataAccess.Repositories;

public class InventoriesRepository: IInventoriesRepository
{
    private readonly FinalDbContext _context;
    public InventoriesRepository(FinalDbContext context) {
        _context = context;
    }
    public async Task<List<Inventory>> GetInventoryList() {
        var inventoryEntities = await _context.Inventories.AsNoTracking().ToListAsync();
        var inventories = inventoryEntities.Select(i => Inventory.Create(i.InventoryId, i.InventoryName).inventory).ToList();
        return inventories;
    }
    public async Task<Inventory> GetInventory(int id) {
        var inventoryEntity = await _context.Inventories.AsNoTracking().FirstAsync(i => i.InventoryId == id);
        var inventory = Inventory.Create(inventoryEntity.InventoryId, inventoryEntity.InventoryName).inventory;
        return inventory;
    }
    public async Task<int> Create(Inventory inventory) {
        var inventoryEntity = new InventoryEntity
        {
            InventoryName = inventory.InventoryName
        };
        await _context.Inventories.AddAsync(inventoryEntity);
        await _context.SaveChangesAsync();
        return inventoryEntity.InventoryId;
    }

    public Task<List<Inventory>> GetUserInventory(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteInventory(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateInventory(Inventory inventory)
    {
        throw new NotImplementedException();
    }
}
