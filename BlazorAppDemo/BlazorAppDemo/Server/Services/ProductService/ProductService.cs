using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Domain;
using BlazorAppDemo.Shared;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<ServiceResponse<Product>> GetProductAsync(int id)
    {
        var response = new ServiceResponse<Product>();
        var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
        {
            response.Success = false;
            response.Message = "Sorry, but this product does not exist.";
        }
        else
        {
            response.Data = product;
        }

        return response;
    }


    public async Task<ServiceResponse<List<Product>>> GetProductByCategoryAsync(string categoryUrl)
    {
        var response = new ServiceResponse<List<Product>>()
        {
            Data = await _dbContext.Products
                .Where(x => x.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                .ToListAsync()
        };

        return response;
    }
}