using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Client.Services.ProductService;
using BlazorAppDemo.Domain;
using BlazorAppDemo.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDemo.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly IBlazorDbContext _dbContext;

    public ProductService(IBlazorDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
    {
        var response = new ServiceResponse<List<Product>>()
        {
            Data = await _dbContext.Products.ToListAsync()
        };

        return response;
    }
}