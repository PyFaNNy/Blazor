using BlazorAppDemo.Domain;
using BlazorAppDemo.Domain.Entity;

namespace BlazorAppDemo.Client.Services.CategoryService;

public interface ICategoryService
{
    List<Category> Categories { get; set; }
    Task GetCategories();
}