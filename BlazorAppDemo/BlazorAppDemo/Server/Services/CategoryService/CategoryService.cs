using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain;
using BlazorAppDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDemo.Server.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly IBlazorDbContext _dbContext;

    public CategoryService(IBlazorDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
    {
        var categories = await _dbContext.Categories.ToListAsync();
        return new ServiceResponse<List<Category>>
        {
            Data = categories
        };
    }
}