using Core.Models;

namespace Core.Abstractions.Services;

public interface IFieldsService
{
    Task<List<Field>> GetInventoryFields(int inventoryId);
    Task<int> CreateField(int inventoryId, Field field);

    Task<int> UpdateField(Field field);

    Task<int> DeleteField(int id);


}
