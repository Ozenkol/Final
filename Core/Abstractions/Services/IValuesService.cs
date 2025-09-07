using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Abstractions.Services
{
    public interface IValuesService
    {
        Task<Value> GetFieldValueOfProduct(Guid fieldId, Guid productId);
        Task<Guid> CreateValue(Guid fieldId, Guid productId, Value value);

        Task<Guid> DeleteValue(Guid id);
    }
}