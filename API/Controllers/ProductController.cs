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

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IInventoriesService _inventoryService;

        public ProductController(IProductsService productsService, IInventoriesService inventoriesService) {
            _productsService = productsService;
            _inventoryService = inventoriesService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetProductList() {
            var productList = await _productsService.GetProductList();
            var productListResponse = productList.Select(i => new ProductResponse(i.ProductId, i.Title, i.Description)).ToList();
            return productListResponse;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id) {
            var product = await _productsService.GetProduct(id);
            var productResponse = new ProductResponse(product.ProductId, product.Title, product.Description);
            return Ok(productResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] ProductRequest productRequest) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var inventory =  await _inventoryService.GetInventory(productRequest.InventoryId);
            if (inventory.UserId == new Guid(userId)) {
                var productResponse = await _productsService.CreateProduct(Product.Create(Guid.Empty, productRequest.Title, productRequest.Description, new Guid(userId), productRequest.InventoryId).product);
                return Ok(productResponse);
            }
            return Forbid();
        }

    }
}