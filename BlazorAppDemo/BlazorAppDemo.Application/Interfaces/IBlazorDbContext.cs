using BlazorAppDemo.Domain;
using BlazorAppDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDemo.Application.Interfaces;

public interface IBlazorDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<ProductType> ProductTypes { get; set; }
    DbSet<ProductVariant> ProductVariants { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<CartItem> CartItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    int SaveChanges();
}