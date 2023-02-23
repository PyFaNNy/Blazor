using BlazorAppDemo.Domain.Entity.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorAppDemo.Persistence.Configurations.Orders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.OrderDate)
            .IsRequired();
        
        builder.Property(x => x.TotalPrice)
            .IsRequired();
        
        builder.HasOne(c => c.User)
            .WithMany(e => e.Orders)
            .HasForeignKey(x => x.UserId);
    }
}