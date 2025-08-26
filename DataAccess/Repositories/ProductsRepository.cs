using Microsoft.EntityFrameworkCore;
using Core.Models;
using Microsoft.EntityFrameworkCore.Internal;
using DataAccess.Entities;
using Core.Abstractions.Repositories;

namespace DataAccess.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly FinalDbContext _context;
    public ProductsRepository(FinalDbContext context)
    {
        _context = context;
    }
    public async Task<List<Product>> GetProductList()
    {
        var productsEntites = await _context.Products.AsNoTracking().ToListAsync();
        var products = productsEntites.Select(p => Product.Create(p.ProductId, p.Title, p.Description, p.InventoryId).product).ToList();
        return products;
    }
    public async Task<int> CreateProduct(Product product)
    {
        var productEntity = new ProductEntity
        {
            Title = product.Title,
            Description = product.Description,
            InventoryId = product.InventoryId,
        };
        await _context.Products.AddAsync(productEntity);
        await _context.SaveChangesAsync();
        return productEntity.ProductId;
    }

    public async Task<Product> GetProduct(int id)
    {
        var productEntity = await _context.Products.AsNoTracking().FirstAsync(i => i.ProductId == id);
        var product = Product.Create(productEntity.ProductId, productEntity.Title, productEntity.Description, productEntity.InventoryId).product;
        return product;
    }

    public async Task<List<Product>> GetInventoryProductList(int inventoryId)
    {
        var productEntityList = await _context.Products.AsNoTracking().Where(p => p.InventoryId == inventoryId).ToListAsync();
        var productList = productEntityList.Select(i => Product.Create(i.InventoryId, i.Title, i.Description, i.InventoryId).product).ToList();
        return productList;
    }

    public Task<List<Product>> GetUserProductList(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}
