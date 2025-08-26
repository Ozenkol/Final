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

    public Task<int> UpdateField(Field field)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteField(int id)
    {
        throw new NotImplementedException();
    }
}
