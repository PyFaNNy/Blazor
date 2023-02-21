using System.Reflection;
using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Domain;
using BlazorAppDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDemo.Persistence;

public class BlazorDbContext : DbContext, IBlazorDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    
    public BlazorDbContext(DbContextOptions<BlazorDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
    
    public override int SaveChanges()
    {
        var result = base.SaveChanges();
        return result;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}