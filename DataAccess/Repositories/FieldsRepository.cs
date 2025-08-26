using Microsoft.EntityFrameworkCore;
using Core.Abstractions.Repositories;
using Core.Models;
using DataAccess.Entities;

namespace DataAccess.Repositories;

public class FieldsRepository: IFieldsRepository
{
    private readonly FinalDbContext _context;
    public FieldsRepository(FinalDbContext context) {
        _context = context;
    }
    public async Task<List<Field>> GetInventoryFields(int inventoryId) {
        var fieldEntities = await _context.Fields.AsNoTracking().ToListAsync();
        var inventoriesField = fieldEntities.Where(f => f.InventoryId == inventoryId).Select(i => Field.Create(i.FieldId, i.Name, i.InventoryId).field).ToList();
        return inventoriesField;
    }
    public async Task<int> CreateField(int inventoryId, Field field) {
        var fieldEntity = new FieldEntity
        {
            Name = field.Name,
            InventoryId = inventoryId
        };
        await _context.Fields.AddAsync(fieldEntity);
        await _context.SaveChangesAsync();
        return fieldEntity.FieldId;
    }

    public async Task<int> UpdateField(Field field)
    {
        var fieldEntity = new FieldEntity
        {
            FieldId = field.FieldId,
            Name = field.Name
        };
        await _context.Fields.Where(i => i.FieldId == fieldEntity.FieldId).ExecuteUpdateAsync(s => s.SetProperty(i => i.Name, i=>fieldEntity.Name));
        return fieldEntity.FieldId;
    }

    public async Task<int> DeleteField(int id)
    {
        var fieldEntity = await _context.Fields.AsNoTracking().FirstAsync(i => i.FieldId == id);
        if (fieldEntity != null) {
            await _context.Fields.AsNoTracking().Where(i => i.FieldId == id).ExecuteDeleteAsync();
            return fieldEntity.FieldId;
        }
        return -1;
    }
}
