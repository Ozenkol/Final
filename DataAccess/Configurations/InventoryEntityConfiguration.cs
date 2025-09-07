using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class InventoryEntityConfiguration : IEntityTypeConfiguration<InventoryEntity>
{
    public void Configure(EntityTypeBuilder<InventoryEntity> builder)
    {
        builder.HasKey(i => i.InventoryId);
        builder.Property(i => i.InventoryName);
        builder.HasMany<ProductEntity>(i => i.Products).WithOne(i => i.Inventory).HasForeignKey(i => i.InventoryId).IsRequired(false);
        builder.HasMany<FieldEntity>(i => i.Fields).WithOne(i => i.Inventory).HasForeignKey(i => i.FieldId).IsRequired(false);
    }
}
