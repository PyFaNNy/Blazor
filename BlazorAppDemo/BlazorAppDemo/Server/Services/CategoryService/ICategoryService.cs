using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain;
using BlazorAppDemo.Domain.Entity;

namespace BlazorAppDemo.Server.Services.CategoryService;

public interface ICategoryService
{
    Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
}