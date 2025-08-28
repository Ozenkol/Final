using API.Contracts.Field;
using Application.Services;
using Core.Abstractions.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FieldController : ControllerBase
{
    private readonly IFieldsService _fieldsService;
    private readonly IInventoriesService _inventoriesService;
    public FieldController(IFieldsService fieldsService, IInventoriesService inventoriesService) {
        _fieldsService = fieldsService;
        _inventoriesService = inventoriesService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<FieldResponse>>> GetInventoryFields(Guid key)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var inventory =  await _inventoriesService.GetInventory(key);
        var fields = await _fieldsService.GetInventoryFields(inventory.InventoryId);
        var fieldResponse = fields.Select(f => new FieldResponse(f.Name));
        return Ok(fieldResponse);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] FieldRequest fieldRequest)
    {
        var inventory = await _inventoriesService.GetInventory(fieldRequest.InventoryId);
        if (inventory != null) {
            var field = new Field(
            fieldId: Guid.Empty,
            fieldName: fieldRequest.Name,
            inventoryId: fieldRequest.InventoryId
            );

            var id = await _fieldsService.CreateField(inventory.InventoryId, field);
            return Ok(id);
        }
        else {
            return Forbid();
        }
    }
}
