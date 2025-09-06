using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts;
using API.Contracts.Product;
using Core.Abstractions.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using API.Contracts.Field;

namespace API.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IInventoriesService _inventoryService;

        public ProductController(IProductsService productsService, IInventoriesService inventoriesService)
        {
            _productsService = productsService;
            _inventoryService = inventoriesService;
        }

        [Authorize]
        [HttpGet]
        [Route("/api/products")]
        public async Task<ActionResult<List<ProductResponse>>> GetProductList()
        {
            var productList = await _productsService.GetProductList();
            var productListResponse = productList.Select(p => new ProductResponse(p.ProductId, p.Title, p.Description,
                p.Inventory.Fields.Select(f => new FieldResponse(
                    f.FieldId,
                    f.Name,
                    p.Values.Where(v => v.FieldId == f.FieldId)
                    .Select(v => v.FieldValue)
                    .FirstOrDefault()
                )).ToList()
            )).ToList();
            return productListResponse;
        }

        [Authorize]
        [HttpGet]
        [Route("/api/inventories/{id}/products")]
        public async Task<ActionResult<List<ProductResponse>>> GetInventoryProductList(Guid id)
        {
            var productList = await _productsService.GetInventoryProductList(id);
            var inventory = await _inventoryService.GetInventory(id);
            var productListResponse = productList.Select(
                p => new ProductResponse(
                    p.InventoryId,
                    p.Title,
                    p.Description,
                    inventory.Fields.Select(f => new FieldResponse(
                        f.FieldId,
                        f.Name,
                        p.Values.Where(v => v.FieldId == f.FieldId)
                        .Select(v => v.FieldValue)
                        .FirstOrDefault()
                    )).ToList()
                )
            ).ToList();
            return productListResponse;
        }

        [Authorize]
        [HttpGet]
        [Route("/api/inventories/{inventoryId}/products/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
        {
            var product = await _productsService.GetProduct(id);
            var productResponse = new ProductResponse(product.ProductId, product.Title, product.Description, product.Inventory.Fields.Select(
                f => new FieldResponse(
                    f.FieldId,
                    f.Name,
                    product.Values.Where(v => v.FieldId == f.FieldId)
                    .Select(v => v.FieldValue)
                    .FirstOrDefault()
                )
            ).ToList());
            return Ok(productResponse);
        }

        [Authorize]
        [HttpPost]
        [Route("/api/inventories/{inventoryId}/products")]
        public async Task<ActionResult<ProductResponse>> CreateProduct(Guid inventoryId, [FromBody] ProductRequest productRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var inventory = await _inventoryService.GetInventory(inventoryId);
            if (inventory.UserId == new Guid(userId))
            {
                var productResponse = await _productsService.CreateProduct(Product.Create(Guid.Empty, productRequest.title, productRequest.description, new Guid(userId), inventoryId).product);
                return Ok(productResponse);
            }
            return Forbid();
        }

        [Authorize]
        [HttpDelete]
        [Route("/api/inventories/{inventoryId}/products/{productId}")]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(Guid inventoryId, Guid productId, [FromBody] ProductRequest productRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var inventory = await _inventoryService.GetInventory(inventoryId);
            if (inventory.UserId == new Guid(userId))
            {
                var deletedProduct = await _productsService.DeleteProduct(productId);
                if (deletedProduct != null) {
                    return Ok();
                }
            }
            return Forbid();
        }

    }
}