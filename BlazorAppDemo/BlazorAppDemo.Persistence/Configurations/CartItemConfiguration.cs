using BlazorAppDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorAppDemo.Persistence.Configurations;

public class CartItemConfiguration: IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(p => new
        {
            p.UserId, p.ProductId, p.ProductTypeId
        });

        builder.HasOne(p => p.Product)
            .WithMany(b => b.CartItems)
            .HasForeignKey(p => p.ProductId);

        builder.HasOne(p => p.ProductType)
            .WithMany(pt => pt.CartItems)
            .HasForeignKey(p => p.ProductTypeId);
        
        builder.HasOne(p => p.User)
            .WithOne(pt => pt.CartItem);
    }
}