using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Models;

namespace Application.Services;

public class FieldsService : IFieldsService
{
    private readonly IFieldsRepository _fieldsRepository;
    public FieldsService(IFieldsRepository fieldsRepository) {
        _fieldsRepository = fieldsRepository;
    }
    public async Task<int> CreateField(int inventoryId, Field field)
    {
        return await _fieldsRepository.CreateField(inventoryId, field);
    }
    public async Task<int> DeleteField(int id)
    {
        return await _fieldsRepository.DeleteField(id);
    }

    public async Task<List<Field>> GetInventoryFields(int inventoryId)
    {
        return await _fieldsRepository.GetInventoryFields(inventoryId);
    }

    public async Task<int> UpdateField(Field field)
    {
        return await _fieldsRepository.UpdateField(field);
    }
}
