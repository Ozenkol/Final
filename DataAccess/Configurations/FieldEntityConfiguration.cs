using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class FieldEntityConfiguration: IEntityTypeConfiguration<FieldEntity>
{
    public void Configure(EntityTypeBuilder<FieldEntity> builder) {
        builder.HasKey(f => f.FieldId);
        builder.Property(f => f.Name);
        builder.HasOne(i => i.Inventory).WithMany(i => i.Fields).HasForeignKey(i => i.InventoryId).IsRequired(false);
        builder.HasMany(i => i.Values).WithOne(i => i.Field).HasForeignKey(i => i.FieldId).IsRequired(false);
    }
}
