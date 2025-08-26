using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Abstractions.Repositories
{
    public interface IValuesRepository
    {
        Task<Value> GetFieldValueOfProduct(int fieldId, int productId);
        Task<int> CreateFieldValueOfProduct(Value value, int fieldId, int productId);
        Task<int> UpdateFieldValueOfProduct(Value value);
        Task<int> DeleteFieldValueOfProduct(int valueId);

    }

}