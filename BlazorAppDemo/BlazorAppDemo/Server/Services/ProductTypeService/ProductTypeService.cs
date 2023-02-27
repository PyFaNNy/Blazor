using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain.Entity.Products;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDemo.Server.Services.ProductTypeService;

public class ProductTypeService : IProductTypeService
{
    private readonly IBlazorDbContext _dbContext;

    public ProductTypeService(IBlazorDbContext context)
    {
        _dbContext = context;
    }

    public async Task<ServiceResponse<List<ProductType>>> AddProductType(ProductType productType)
    {
        productType.Editing = productType.IsNew = false;
        _dbContext.ProductTypes.Add(productType);
        await _dbContext.SaveChangesAsync(new CancellationToken());

        return await GetProductTypes();
    }

    public async Task<ServiceResponse<List<ProductType>>> GetProductTypes()
    {
        var productTypes = await _dbContext.ProductTypes.ToListAsync();
        return new ServiceResponse<List<ProductType>> { Data = productTypes };
    }

    public async Task<ServiceResponse<List<ProductType>>> UpdateProductType(ProductType productType)
    {
        var dbProductType = await _dbContext.ProductTypes.FindAsync(productType.Id);
        if (dbProductType == null)
        {
            return new ServiceResponse<List<ProductType>>
            {
                Success = false,
                Message = "Product Type not found."
            };
        }

        dbProductType.Name = productType.Name;
        await _dbContext.SaveChangesAsync(new CancellationToken());

        return await GetProductTypes();
    }
}