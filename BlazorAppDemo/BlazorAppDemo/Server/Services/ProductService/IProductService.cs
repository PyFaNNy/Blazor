using BlazorAppDemo.Domain;
using BlazorAppDemo.Shared;

namespace BlazorAppDemo.Server.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<List<Product>>> GetProductsAsync();
    Task<ServiceResponse<Product>> GetProductAsync(int id);
    Task<ServiceResponse<List<Product>>> GetProductByCategoryAsync(string category);
    Task<ServiceResponse<List<Product>>> SearchProducts(string searchText);
    Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
}