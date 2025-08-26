using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Abstractions.Services
{
    public interface IValuesService
    {
        Task<Value> GetFieldValueOfProduct(int fieldId, int productId);
    }
}