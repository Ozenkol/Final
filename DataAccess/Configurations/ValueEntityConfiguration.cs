using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ValueEntityConfiguration: IEntityTypeConfiguration<ValueEntity>
{
    public void Configure(EntityTypeBuilder<ValueEntity> builder) {
        builder.HasKey(i => i.ValueId);
        builder.Property(i => i.Value);
        builder.HasOne(i => i.Product).WithMany(i => i.Values).HasForeignKey(i => i.ProductId).IsRequired(false);
        builder.HasOne(i => i.Field).WithMany(i => i.Values).HasForeignKey(i => i.FieldId).IsRequired(false);
    }
}
