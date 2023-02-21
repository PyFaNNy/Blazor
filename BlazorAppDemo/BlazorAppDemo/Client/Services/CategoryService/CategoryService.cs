using System.Net.Http.Json;
using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain;
using BlazorAppDemo.Domain.Entity;

namespace BlazorAppDemo.Client.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _http;
    public List<Category> Categories { get; set; } = new List<Category>();

    public CategoryService(HttpClient http)
    {
        Console.Error.Write(http);
        _http = http;
    }
    
    public async Task GetCategories()
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");
        if (result?.Data != null)
        {
            Categories = result.Data;
        }
    }
}