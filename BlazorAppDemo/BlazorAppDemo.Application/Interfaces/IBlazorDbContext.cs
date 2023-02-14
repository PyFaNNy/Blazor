using BlazorAppDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDemo.Application.Interfaces;

public interface IBlazorDbContext
{
    DbSet<Product> Products
    {
        get;
        set;
    }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    int SaveChanges();
}