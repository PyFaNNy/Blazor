using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain;
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

    public Task<ServiceResponse<PaginatedList<Product>>> SearchProducts(string searchText)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<PaginatedList<Product>>> SearchProducts(string searchText, int pageIndex,
        int pageSize)
    {
        var products = FindProductsBySearchText(searchText);

        var paginatedList =
            await PaginatedList<Product>.CreateAsync(products, pageIndex, pageSize);

        var respone = new ServiceResponse<PaginatedList<Product>>()
        {
            Data = paginatedList
        };

        return respone;
    }

    private IQueryable<Product> FindProductsBySearchText(string searchText)
    {
        return _dbContext.Products
            .Where(p => p.Title.ToLower().Contains((searchText.ToLower())) ||
                        p.Description.ToLower().Contains(searchText.ToLower()))
            .Include(p => p.ProductVariants);
    }

    public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
    {
        var products = await FindProductsBySearchText(searchText).ToListAsync();
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