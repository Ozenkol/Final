using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ProductEntityConfiguration: IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder) {
        builder.HasKey(i => i.ProductId);
        builder.HasMany(i => i.Values).WithOne(v => v.Product).HasForeignKey(i => i.ProductId).IsRequired(false);
        builder.HasOne(i => i.Inventory).WithMany(i => i.Products).HasForeignKey(i => i.InventoryId).IsRequired(false);
    }
}
