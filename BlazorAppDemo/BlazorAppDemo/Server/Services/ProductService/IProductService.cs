using BlazorAppDemo.Domain;
using BlazorAppDemo.Shared;

namespace BlazorAppDemo.Server.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<List<Product>>> GetProductsAsync();
}