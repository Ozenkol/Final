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

        public async Task<int> CreateFieldValueOfProduct(Value value, int fieldId, int productId)
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

        public async Task<int> DeleteFieldValueOfProduct(int valueId)
        {
            throw new NotImplementedException();
        }

        public async Task<Value> GetFieldValueOfProduct(int fieldId, int productId)
        {
            var valueEntity = await _context.Values.AsNoTracking().FirstAsync(v => v.FieldId == fieldId && v.ProductId == productId);
            return Value.Create(valueEntity.ValueId, valueEntity.Value, valueEntity.ProductId, valueEntity.FieldId).value;
        }

        public async Task<int> UpdateFieldValueOfProduct(Value value)
        {
            throw new NotImplementedException();
        }
    }
}