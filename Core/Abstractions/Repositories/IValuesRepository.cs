using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Abstractions.Repositories
{
    public interface IValuesRepository
    {
        Task<Value> GetFieldValueOfProduct(Guid fieldId, Guid productId);
        Task<Guid> CreateFieldValueOfProduct(Value value, Guid fieldId, Guid productId);
        Task<Guid> UpdateFieldValueOfProduct(Value value);
        Task<Guid> DeleteFieldValueOfProduct(Guid valueId);

    }

}