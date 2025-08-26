using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Services
{

    public class ValuesService : IValuesService
    {
        private readonly IValuesRepository _valuesRepository;
        public ValuesService(IValuesRepository valuesRepository)
        {
            _valuesRepository = valuesRepository;
        }
        public async Task<Value> GetFieldValueOfProduct(int fieldId, int productId)
        {
            return await _valuesRepository.GetFieldValueOfProduct(fieldId, productId);
        }
    }
};