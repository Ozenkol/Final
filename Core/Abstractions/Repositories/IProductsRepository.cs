using Core.Models;

namespace Core.Abstractions.Repositories;

public interface IProductsRepository
{
    Task<Product> GetProduct(Guid id);
    Task<List<Product>> GetProductList();
    Task<List<Product>> GetInventoryProductList(Guid inventoryId);
    Task<List<Product>> GetUserProductList(Guid userId);
    Task<Guid> CreateProduct(Product product);
    Task<Guid> DeleteProduct(Guid id);
    Task<Guid> UpdateProduct(Product product);
}