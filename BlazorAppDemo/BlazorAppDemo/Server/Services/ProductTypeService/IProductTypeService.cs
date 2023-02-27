using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain.Entity.Products;

namespace BlazorAppDemo.Server.Services.ProductTypeService;

public interface IProductTypeService
{
    Task<ServiceResponse<List<ProductType>>> GetProductTypes();
    Task<ServiceResponse<List<ProductType>>> AddProductType(ProductType productType);
    Task<ServiceResponse<List<ProductType>>> UpdateProductType(ProductType productType);
}