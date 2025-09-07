using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Models;

namespace Application.Services;

public class FieldsService : IFieldsService
{
    private readonly IFieldsRepository _fieldsRepository;
    public FieldsService(IFieldsRepository fieldsRepository)
    {
        _fieldsRepository = fieldsRepository;
    }
    public async Task<Guid> CreateField(Guid inventoryId, Field field)
    {
        return await _fieldsRepository.CreateField(inventoryId, field);
    }
    public async Task<Guid> DeleteField(Guid id)
    {
        return await _fieldsRepository.DeleteField(id);
    }

    public async Task<List<Field>> GetInventoryFields(Guid inventoryId)
    {
        return await _fieldsRepository.GetInventoryFields(inventoryId);
    }

    public async Task<Guid> UpdateField(Field field)
    {
        return await _fieldsRepository.UpdateField(field);
    }
}
