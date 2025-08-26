using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts;
using Core.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductController(IProductsService productsService) {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetProductList() {
            var productList = await _productsService.GetProductList();
            var productListResponse = productList.Select(i => new ProductResponse(i.ProductId, i.Title, i.Description)).ToList();
            return productListResponse;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(int id) {
            var product = await _productsService.GetProduct(id);
            var productResponse = new ProductResponse(product.ProductId, product.Title, product.Description);
            return Ok(productResponse);
        }
    }
}