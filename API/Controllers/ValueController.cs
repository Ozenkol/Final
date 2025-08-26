using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts.Value;
using Core.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Core.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValueController : ControllerBase
    {
        private readonly IValuesService _valuesService;
        public ValueController(IValuesService valuesService) {
            _valuesService = valuesService;
        }
        [HttpGet("products/{productId}/field/{fieldId}")]
        public async Task<ValueResponse> GetFieldValueOfProduct(int fieldId, int productId) {
            var valueModel = await _valuesService.GetFieldValueOfProduct(fieldId, productId);
            return new ValueResponse(valueModel.ValueId, valueModel.FieldValue);
        }

    }
}