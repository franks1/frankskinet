using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderConfiguration:IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(a => a.Address,
                x=> x.WithOwner());
        builder.Property(a => a.OrderStatus)
            .HasConversion(
                a => a.ToString(),
                a => (OrderStatus)Enum.Parse(typeof(OrderStatus), a)
            );
        builder.HasMany(a => a.Items)
            .WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}