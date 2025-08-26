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
    public async Task<int> CreateInventory(Inventory inventory) {
        var inventoryEntity = new InventoryEntity
        {
            InventoryName = inventory.InventoryName
        };
        await _context.Inventories.AddAsync(inventoryEntity);
        await _context.SaveChangesAsync();
        return inventoryEntity.InventoryId;
    }
    public async Task<int> DeleteInventory(int id)
    {
        var inventory = await _context.Inventories.AsNoTracking().FirstAsync(i => i.InventoryId == id);
        if (inventory != null) {
            await _context.Inventories.AsNoTracking().Where(i => i.InventoryId == id).ExecuteDeleteAsync();
            return inventory.InventoryId;
        }
        return -1;
    }

    public async Task<int> UpdateInventory(Inventory inventory)
    {
        var inventoryEntity = new InventoryEntity
        {
            InventoryId = inventory.InventoryId,
            InventoryName = inventory.InventoryName
        };
        await _context.Inventories.Where(i => i.InventoryId == inventoryEntity.InventoryId).ExecuteUpdateAsync(s => s.SetProperty(i => i.InventoryName, i=>inventoryEntity.InventoryName));
        return inventoryEntity.InventoryId;
    }

    public async Task<List<Inventory>> GetUserInventoryList(int userId)
    {
        var inventoryEntities = await _context.Inventories.AsNoTracking().Where(i => i.UserId == userId).ToListAsync();
        var inventories = inventoryEntities.Select(i => Inventory.Create(i.InventoryId, i.InventoryName).inventory).ToList();
        return inventories;
    }
}
