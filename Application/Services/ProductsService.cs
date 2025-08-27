using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Models;

namespace Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<List<Product>> GetProductList()
        {
            return await _productsRepository.GetProductList();
        }
        public async Task<Product> GetProduct(Guid id)
        {
            return await _productsRepository.GetProduct(id);
        }

        public async Task<List<Product>> GetInventoryProductList(Guid inventoryId)
        {
            return await _productsRepository.GetInventoryProductList(inventoryId);
        }

        public async Task<List<Product>> GetUserProductList(Guid userId)
        {
            return await _productsRepository.GetUserProductList(userId);
        }

        public async Task<Guid> CreateProduct(Product product)
        {
            return await _productsRepository.CreateProduct(product);
        }

        public async Task<Guid> DeleteProduct(Guid id)
        {
            return await _productsRepository.DeleteProduct(id);
        }

        public async Task<Guid> UpdateProduct(Product product)
        {
            return await _productsRepository.UpdateProduct(product);
        }
    }
}