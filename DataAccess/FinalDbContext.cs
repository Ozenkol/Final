using DataAccess.Configurations;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class FinalDbContext: IdentityDbContext<UserEntity>
{
    public FinalDbContext(DbContextOptions<FinalDbContext> options) : base(options) { }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<InventoryEntity> Inventories {get; set; }
    public DbSet<FieldEntity> Fields { get; set; }
    public DbSet<ValueEntity> Values { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new ValueEntityConfiguration());        
        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new FieldEntityConfiguration());

    }
}
