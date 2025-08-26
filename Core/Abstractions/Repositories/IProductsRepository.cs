using Core.Models;

namespace Core.Abstractions.Repositories;

public interface IProductsRepository
{
    Task<Product> GetProduct(int id);
    Task<List<Product>> GetProductList();
    Task<List<Product>> GetInventoryProductList(int inventoryId);
    Task<List<Product>> GetUserProductList(int userId);
    Task<int> CreateProduct(Product product);
    Task<int> DeleteProduct(int id);
    Task<int> UpdateProduct(Product product);
}