using API.Contracts;
using Application.Services;
using Core.Abstractions.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using API.Contracts.Value;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValueController : ControllerBase
    {
        private readonly IValuesService _valuesService;
        public ValueController(IValuesService valuesService) {
            _valuesService = valuesService;
        }

        [HttpGet("products/{productId}/field/{fieldId}")]
        public async Task<ActionResult<ValueResponse>> GetFieldValueOfProduct(Guid fieldId, Guid productId) {
            var valueModel = await _valuesService.GetFieldValueOfProduct(fieldId, productId);
            return new ValueResponse(valueModel.ValueId, valueModel.FieldValue);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateFieldValueOfProduct([FromBody] ValueRequest valueRequest) {
            var value = await _valuesService.CreateValue(valueRequest.fieldId, valueRequest.productId, Value.Create(Guid.Empty, valueRequest.value, valueRequest.fieldId, valueRequest.productId).value);
            return Ok(value);
        }

    }
}