using Core.Models;

namespace Core.Abstractions.Repositories;

public interface IFieldsRepository
{
    Task<List<Field>> GetInventoryFields(int inventoryId);
    Task<int> CreateField(int inventoryId, Field field);

    Task<int> UpdateField(Field field);

    Task<int> DeleteField(int id);


}
