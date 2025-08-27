using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Abstractions.Services
{
    public interface IProductsService
    {
        Task<Product> GetProduct(Guid id);
        Task<List<Product>> GetProductList();
        Task<List<Product>> GetInventoryProductList(Guid inventoryId);
        Task<List<Product>> GetUserProductList(Guid userId);
        Task<int> CreateProduct(Product product);
        Task<int> DeleteProduct(Guid id);
        Task<int> UpdateProduct(Product product);
    }

}