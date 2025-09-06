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
public class FieldController : ControllerBase
{
    private readonly IFieldsService _fieldsService;
    private readonly IInventoriesService _inventoriesService;
    public FieldController(IFieldsService fieldsService, IInventoriesService inventoriesService)
    {
        _fieldsService = fieldsService;
        _inventoriesService = inventoriesService;
    }

    // [Authorize]
    // [HttpGet]
    // public async Task<ActionResult<List<FieldResponse>>> GetInventoryFields(Guid key)
    // {
    //     var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //     var inventory = await _inventoriesService.GetInventory(key);
    //     var fields = await _fieldsService.GetInventoryFields(inventory.InventoryId);
    //     var fieldResponse = fields.Select(f => new FieldResponse(f.FieldId, f.Name));
    //     return Ok(fieldResponse);
    // }

    [HttpPost]
    [Route("/api/inventories/{inventoryId}/fields")]
    public async Task<ActionResult<int>> Post(Guid inventoryId, [FromBody] FieldRequest fieldRequest)
    {
        var inventory = await _inventoriesService.GetInventory(inventoryId);
        if (inventory != null)
        {
            var field = new Field(
            fieldId: Guid.Empty,
            fieldName: fieldRequest.name,
            inventoryId: inventoryId
            );

            var id = await _fieldsService.CreateField(inventory.InventoryId, field);
            return Ok(id);
        }
        else
        {
            return Forbid();
        }
    }
    [HttpDelete]
    [Route("/api/inventories/{inventoryId}/fields/{fieldId}")]
    public async Task<ActionResult<int>> DeleteField(Guid fieldId)
    {
        var deletedField = await _fieldsService.DeleteField(fieldId);
        if (deletedField != null)
        {
            return Ok();
        }
        else
        {
            return Forbid();
        }
    }
}
