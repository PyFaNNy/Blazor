using System.Net.Http.Json;
using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain;
using BlazorAppDemo.Domain.Entity;

namespace BlazorAppDemo.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public string LastSearchText { get; set; } = string.Empty;
    public event Action? ProductsChanged;
    public List<Product> Products { get; set; } = new List<Product>();
    public string Message { get; set; } = "Loading Products";
    public int CurrentPage { get; set; } = 1;
    public int PageCount { get; set; } = 0;
    public int PageSize { get; set; } = 5;

    public async Task GetProducts(string? categoryUrl = null)
    {
        var result = categoryUrl == null
            ? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product/feature")
            : await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/category/{categoryUrl}");
        if (result?.Data != null)
        {
            Products = result.Data;
        }

        CurrentPage = 1;
        PageCount = 0;

        if (Products.Count == 0)
        {
            Message = "No products found";
        }
        
        ProductsChanged?.Invoke();
    }

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
        return result;
    }
    
    public async Task SearchProducts(string searchText, int pageIndex, int pageSize)
    {
        LastSearchText = searchText;
        PageSize = pageSize;
        var result = await _http
            .GetFromJsonAsync<ServiceResponse<PaginatedList<Product>>>($"api/product/search/{searchText}/{pageIndex}/{pageSize}");

        if (result?.Data != null)
        {
            Products = result.Data.Items;
            CurrentPage = result.Data.PageIndex;
            PageCount = result.Data.TotalPages;
        }

        if (Products.Count == 0)
        {
            Message = "No products found.";
        }

        ProductsChanged?.Invoke();
    }

    public async Task<List<string>> GetProductsSearchSuggestions(string searchText)
    {
        var result = await _http
            .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");

        return result.Data;
    }
}