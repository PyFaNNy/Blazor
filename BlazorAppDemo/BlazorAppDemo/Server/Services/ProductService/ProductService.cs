using BlazorAppDemo.Application.Interfaces;
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
            Data = await _dbContext.Products
                .Include(x => x.ProductVariants)
                .ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
    {
        var response = new ServiceResponse<Product>();
        var product = await _dbContext.Products
            .Include(x => x.ProductVariants)
            .ThenInclude(x => x.ProductType)
            .FirstOrDefaultAsync(x => x.Id == productId);
        if (product is null)
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
                .Include(x => x.ProductVariants)
                .ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchText)
    {
        var respone = new ServiceResponse<List<Product>>()
        {
            Data = await FindProductsBySearchTextAsync(searchText)
        };

        return respone;
    }

    private async Task<List<Product>> FindProductsBySearchTextAsync(string searchText)
    {
        return await _dbContext.Products
            .Where(p => p.Title.ToLower().Contains((searchText.ToLower())) ||
                        p.Description.ToLower().Contains(searchText.ToLower()))
            .Include(p => p.ProductVariants)
            .ToListAsync();
    }

    public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
    {
        var products = await FindProductsBySearchTextAsync(searchText);
        List<string> result = new List<string>();

        foreach (var product in products)
        {
            if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(product.Title);
            }

            if (product.Description != null)
            {
                var punctuation = product.Description.Where(char.IsPunctuation)
                    .Distinct().ToArray();
                var words = product.Description.Split()
                    .Select(s => s.Trim(punctuation));

                foreach (var word in words)
                {
                    if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                        && !result.Contains(word))
                    {
                        result.Add(word);
                    }
                }
            }
        }

        return new ServiceResponse<List<string>> {Data = result};
    }

    public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
    {
        var response = new ServiceResponse<List<Product>>
        {
            Data = await _dbContext.Products
                .Where(x => x.Featured)
                .Include(x => x.ProductVariants)
                .ToListAsync()
        };

        return response;
    }
}