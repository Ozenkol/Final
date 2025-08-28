using API.Contracts;
using Application.Services;
using Core.Abstractions.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace API.Controllers;
[Route("[controller]")] 
[ApiController]

public class InventoryController: ControllerBase
{
    private readonly IInventoriesService _inventoriesService;
    public InventoryController(IInventoriesService inventoriesService) {
        _inventoriesService = inventoriesService;
    }

    [Authorize]
    [HttpGet("users/{userId}/inventories")]
    public async Task<ActionResult<List<InventoryResponse>>> GetUserInventoryList(Guid userId)
    {
        var inventories = await _inventoriesService.GetUserInventoryList(userId);
        var inventoriesResponse = inventories.Select(i => new InventoryResponse(i.InventoryId, i.InventoryName));
        return Ok(inventoriesResponse);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryResponse>> GetInventory(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var inventory = await _inventoriesService.GetInventory(id);
        if(inventory.UserId == new Guid(userId)) {
            var inventoryResponse = new InventoryResponse(inventory.InventoryId, inventory.InventoryName);
            return Ok(inventoryResponse);
        }
        return Unauthorized();
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Post([FromBody]InventoryRequest inventoryRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null) {
            var ok = await _inventoriesService.CreateInventory(Inventory.Create(Guid.Empty, inventoryRequest.Title, new Guid(userId)).inventory);
            return Ok(ok);
        }
        return Forbid();
    }
}
