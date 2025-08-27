using Core.Abstractions.Repositories;
using Core.Models;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ValuesRepository : IValuesRepository
    {
        private readonly FinalDbContext _context;
        public ValuesRepository(FinalDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateFieldValueOfProduct(Value value, Guid fieldId, Guid productId)
        {
            var valueEntity = new ValueEntity
            {
                Value = value.FieldValue,
                FieldId = fieldId,
                ProductId = productId
            };
            await _context.Values.AddAsync(valueEntity);
            await _context.SaveChangesAsync();
            return valueEntity.ValueId;
        }

        public async Task<Guid> DeleteFieldValueOfProduct(Guid valueId)
        {
            throw new NotImplementedException();
        }

        public async Task<Value> GetFieldValueOfProduct(Guid fieldId, Guid productId)
        {
            var valueEntity = await _context.Values.AsNoTracking().FirstAsync(v => v.FieldId == fieldId && v.ProductId == productId);
            return Value.Create(valueEntity.ValueId, valueEntity.Value, valueEntity.ProductId, valueEntity.FieldId).value;
        }

        public async Task<Guid> UpdateFieldValueOfProduct(Value value)
        {
            throw new NotImplementedException();
        }
    }
}