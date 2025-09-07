using Microsoft.EntityFrameworkCore;
using Core.Abstractions.Repositories;
using Core.Models;
using DataAccess.Entities;

namespace DataAccess.Repositories;

public class InventoriesRepository : IInventoriesRepository
{
    private readonly FinalDbContext _context;
    public InventoriesRepository(FinalDbContext context)
    {
        _context = context;
    }
    public async Task<List<Inventory>> GetInventoryList()
    {
        var inventoryEntities = await _context.Inventories.Include(i => i.Fields).AsNoTracking().ToListAsync();
        var inventories = inventoryEntities.Select(i =>
            Inventory.Create(
                i.InventoryId,
                i.InventoryName,
                i.UserId,
                (i.Fields ?? new List<FieldEntity>())
                .Select(f => Field.Create(f.FieldId, f.Name, f.InventoryId).field)
                .ToList()
                ).inventory
        ).ToList();
        return inventories;
    }
    public async Task<Inventory> GetInventory(Guid id)
    {
        var inventoryEntity = await _context.Inventories.Include(i => i.Fields).AsNoTracking().FirstAsync(i => i.InventoryId == id);
        var inventory = Inventory.Create(
            inventoryEntity.InventoryId, 
            inventoryEntity.InventoryName, 
            inventoryEntity.UserId,
            inventoryEntity.Fields
                .Select(f => Field.Create(f.FieldId, f.Name, f.InventoryId).field)
                .ToList()
            ).inventory;
        return inventory;
    }
    public async Task<Guid> CreateInventory(Inventory inventory)
    {
        var inventoryEntity = new InventoryEntity
        {
            InventoryName = inventory.InventoryName,
            UserId = inventory.UserId
        };
        await _context.Inventories.AddAsync(inventoryEntity);
        await _context.SaveChangesAsync();
        return inventoryEntity.InventoryId;
    }
    public async Task<Guid> DeleteInventory(Guid id)
    {
        var inventory = await _context.Inventories.AsNoTracking().FirstAsync(i => i.InventoryId == id);
        if (inventory != null)
        {
            await _context.Inventories.AsNoTracking().Where(i => i.InventoryId == id).ExecuteDeleteAsync();
            return inventory.InventoryId;
        }
        return Guid.Empty;
    }

    public async Task<Guid> UpdateInventory(Inventory inventory)
    {
        var inventoryEntity = new InventoryEntity
        {
            UserId = inventory.UserId,
            InventoryId = inventory.InventoryId,
            InventoryName = inventory.InventoryName
        };
        await _context.Inventories.Where(i => i.InventoryId == inventoryEntity.InventoryId).ExecuteUpdateAsync(s => s.SetProperty(i => i.InventoryName, i => inventoryEntity.InventoryName));
        return inventoryEntity.InventoryId;
    }

    public async Task<List<Inventory>> GetUserInventoryList(Guid userId)
    {
        var inventoryEntities = await _context.Inventories.Include(i => i.Fields).AsNoTracking().Where(i => i.UserId == userId).ToListAsync();
        var inventories = inventoryEntities.Select(i => Inventory.Create(i.InventoryId, i.InventoryName, i.UserId).inventory).ToList();
        return inventories;
    }
}
