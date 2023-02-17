using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain;

namespace BlazorAppDemo.Server.Services.CategoryService;

public interface ICategoryService
{
    Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
}