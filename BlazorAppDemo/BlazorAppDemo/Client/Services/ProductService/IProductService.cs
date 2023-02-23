using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain;
using BlazorAppDemo.Domain.Entity;
using BlazorAppDemo.Domain.Entity.Products;

namespace BlazorAppDemo.Client.Services.ProductService;

public interface IProductService
{
    string Message { get; set; }
    int CurrentPage { get; set; }
    int PageCount { get; set; }
    int PageSize { get; set; }
    string LastSearchText { get; set; }
    event Action ProductsChanged;
    List<Product> Products { get; set; }
    
    Task GetProducts(string? categoryUrl = null);
    Task<ServiceResponse<Product>> GetProduct(int productId);
    Task SearchProducts(string searchText, int pageIndex, int pageSize);
    Task<List<string>> GetProductsSearchSuggestions(string searchText);
}