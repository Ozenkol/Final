using API.Contracts;
using Application.Services;
using Core.Abstractions.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;
[Route("[controller]")] 
[ApiController]

public class InventoryController: ControllerBase
{
    private readonly IInventoriesService _inventoriesService;
    public InventoryController(IInventoriesService inventoriesService) {
        _inventoriesService = inventoriesService;
    }
        
    [HttpGet("users/{id}/inventories")]
    public async Task<ActionResult<List<InventoryResponse>>> GetUserInventoryList(Guid id)
    {
        var inventories = await _inventoriesService.GetUserInventoryList(id);
        var inventoriesResponse = inventories.Select(i => new InventoryResponse(i.InventoryId, i.InventoryName));
        return Ok(inventoriesResponse);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryResponse>> GetInventory(Guid id)
    {
        
        var inventory = await _inventoriesService.GetInventory(id);
        var inventoryResponse = new InventoryResponse(inventory.InventoryId, inventory.InventoryName);
        return Ok(inventoryResponse);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Post([FromBody]InventoryRequest inventoryRequest)
    {
        var ok = await _inventoriesService.CreateInventory(Inventory.Create(Guid.Empty,inventoryRequest.Title).inventory);
        return Ok(ok);
    }
}
