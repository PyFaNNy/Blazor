using System.Reflection;
using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDemo.Persistence;

public class BlazorDbContext : DbContext, IBlazorDbContext
{
    public DbSet<Product> Products { get; set; }
    
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