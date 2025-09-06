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
        public ValueController(IValuesService valuesService)
        {
            _valuesService = valuesService;
        }

        [HttpPost]
        [Route("/api/inventories/{inventoryId}/products/{productId}/values")]
        public async Task<ActionResult<Guid>> CreateFieldValueOfProduct(Guid inventoryId, Guid productId, [FromBody] ValueRequest valueRequest)
        {
            var value = await _valuesService.CreateValue(valueRequest.fieldId, productId, Value.Create(Guid.Empty, valueRequest.value, valueRequest.fieldId, productId).value);
            return Ok(value);
        }

    }
}