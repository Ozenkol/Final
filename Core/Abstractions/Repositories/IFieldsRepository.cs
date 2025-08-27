using Core.Models;

namespace Core.Abstractions.Repositories;

public interface IFieldsRepository
{
    Task<List<Field>> GetInventoryFields(Guid inventoryId);
    Task<Guid> CreateField(Guid inventoryId, Field field);

    Task<Guid> UpdateField(Field field);

    Task<Guid> DeleteField(Guid id);


}
