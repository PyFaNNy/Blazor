using BlazorAppDemo.Domain.Entity.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorAppDemo.Persistence.Configurations.Orders;

public class OrderItemConfiguration: IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => new { oi.OrderId, oi.ProductId, oi.ProductTypeId });
        
        builder.Property(x => x.Quantity)
            .IsRequired();
        
        builder.Property(x => x.TotalPrice)
            .IsRequired();
        
        builder.HasOne(p => p.Product)
            .WithMany(b => b.OrderItems)
            .HasForeignKey(p => p.ProductId);

        builder.HasOne(p => p.ProductType)
            .WithMany(pt => pt.OrderItems)
            .HasForeignKey(p => p.ProductTypeId);
        
        builder.HasOne(p => p.Order)
            .WithMany(pt => pt.OrderItems)
            .HasForeignKey(p => p.OrderId);
    }
}