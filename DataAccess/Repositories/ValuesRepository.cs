using Core.Abstractions.Repositories;
using Core.Models;
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
        public async Task<Value> GetFieldValueOfProduct(Field field, Product product)
        {
            var valueEntity = await _context.Values.AsNoTracking().FirstAsync(v => v.FieldId == field.FieldId && v.ProductId == product.ProductId);
            return Value.Create(valueEntity.ValueId, valueEntity.Value, valueEntity.ProductId, valueEntity.FieldId).value;
        }
    }
}