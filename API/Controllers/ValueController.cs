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
        private readonly ILogger<ValueController> _logger;

        public ValueController(IValuesService valuesService, ILogger<ValueController> logger)
        {
            _valuesService = valuesService;
            _logger = logger;
        }

        [HttpPost]
        [Route("/api/inventories/{inventoryId}/products/{productId}/values")]
        public async Task<ActionResult<Guid>> CreateFieldValueOfProduct(Guid inventoryId, Guid productId, [FromBody] ValueRequest valueRequest)
        {
            _logger.LogInformation("Creating value");
            _logger.LogCritical("Product Id {productId}", productId);
            _logger.LogCritical("Inventory Id {inventoryId}", inventoryId);
            var existedValue = await _valuesService.GetFieldValueOfProduct(valueRequest.fieldId, productId);
            if (existedValue != null) {
                var deletedValue = await _valuesService.DeleteValue(existedValue.ValueId);
                var value = await _valuesService.CreateValue(valueRequest.fieldId, productId, Value.Create(Guid.Empty, valueRequest.value, valueRequest.fieldId, productId).value);
                _logger.LogInformation("Creating value {value}", value);
                return Ok(value);
            }
            else {
                var value = await _valuesService.CreateValue(valueRequest.fieldId, productId, Value.Create(Guid.Empty, valueRequest.value, valueRequest.fieldId, productId).value);
                _logger.LogInformation("Creating value {value}", value);
                return Ok(value);
            }
            return Forbid();
        }

    }
}