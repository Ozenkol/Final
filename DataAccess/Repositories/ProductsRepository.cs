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
        var products = productsEntites.Select(p => Product.Create(p.ProductId, p.Title, p.Description, p.UserId, p.InventoryId).product).ToList();
        return products;
    }
    public async Task<Guid> CreateProduct(Product product)
    {
        var productEntity = new ProductEntity
        {
            Title = product.Title,
            Description = product.Description,
            UserId = product.UserId,
            InventoryId = product.InventoryId,
        };
        await _context.Products.AddAsync(productEntity);
        await _context.SaveChangesAsync();
        return productEntity.ProductId;
    }

    public async Task<Product> GetProduct(Guid id)
    {
        var productEntity = await _context.Products.AsNoTracking().FirstAsync(i => i.ProductId == id);
        var product = Product.Create(productEntity.ProductId, productEntity.Title, productEntity.Description, productEntity.UserId, productEntity.InventoryId).product;
        return product;
    }

    public async Task<List<Product>> GetInventoryProductList(Guid inventoryId)
    {
        var productEntityList = await _context.Products.AsNoTracking().Where(p => p.InventoryId == inventoryId).ToListAsync();
        var productList = productEntityList.Select(i => Product.Create(i.InventoryId, i.Title, i.Description, i.UserId, i.InventoryId).product).ToList();
        return productList;
    }

    public async Task<List<Product>> GetUserProductList(Guid userId)
    {
        var productEntityList = await _context.Products.AsNoTracking().Where(p => p.UserId == userId).ToListAsync();
        var productList = productEntityList.Select(i => Product.Create(i.InventoryId, i.Title, i.Description, i.UserId, i.InventoryId).product).ToList();
        return productList;
    }

    public async Task<Guid> DeleteProduct(Guid id)
    {
        var product = await _context.Products.AsNoTracking().FirstAsync(i => i.ProductId == id);
        if (product != null) {
            await _context.Products.AsNoTracking().Where(i => i.UserId == id).ExecuteDeleteAsync();
            return product.ProductId;
        }
        return Guid.Empty;
    }

    public async Task<Guid> UpdateProduct(Product product)
    {
        var productEntity = new ProductEntity
        {
            ProductId = product.ProductId,
            Title = product.Title,
            Description = product.Description
        };
        await _context.Products.Where(i => i.ProductId == productEntity.ProductId).ExecuteUpdateAsync(s => s.SetProperty(i => i.Title, i=>productEntity.Title).SetProperty(i => i.Description, i => productEntity.Description));
        return productEntity.ProductId;
    }
}
