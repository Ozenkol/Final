using API.Contracts;
using Application.Services;
using Core.Abstractions.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Runtime.CompilerServices;


namespace API.Controllers;
[Route("api/inventories")]
[ApiController]

public class InventoryController : ControllerBase
{
    private readonly IInventoriesService _inventoriesService;
    public InventoryController(IInventoriesService inventoriesService)
    {
        _inventoriesService = inventoriesService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<InventoryResponse>>> GetUserInventoryList()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var inventories = await _inventoriesService.GetUserInventoryList(new Guid(userId));
        var inventoriesResponse = inventories.Select(i => new InventoryResponse(i.InventoryId, i.InventoryName));
        return Ok(inventoriesResponse);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryResponse>> GetInventory(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var inventory = await _inventoriesService.GetInventory(id);
        if (inventory.UserId == new Guid(userId))
        {
            var inventoryResponse = new InventoryResponse(inventory.InventoryId, inventory.InventoryName);
            return Ok(inventoryResponse);
        }
        return Unauthorized();
    }

    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteInventory(Guid id) {
        var deletedInventory = await _inventoriesService.DeleteInventory(id);
        if (deletedInventory != null) {
            return Ok();
        }
        return Forbid();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult> UpdateInventory(Guid id) {
        var deletedInventory = await _inventoriesService.DeleteInventory(id);
        if (string.IsNullOrEmpty(deletedInventory.ToString())) {
            return Ok();
        }
        return Forbid();
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<InventoryResponse>> CreateInventory([FromBody] InventoryRequest inventoryRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            var createdInventory = await _inventoriesService.CreateInventory(Inventory.Create(Guid.Empty, inventoryRequest.title, new Guid(userId)).inventory);
            var createdInventoryResponse = new InventoryResponse(
                createdInventory.InventoryId,
                createdInventory.InventoryName
            );
            return Ok(createdInventoryResponse);
        }
        return Forbid();
    }
}
