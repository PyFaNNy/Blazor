using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Application.Models;
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

    public async Task<ServiceResponse<List<Category>>> AddCategory(Category category)
    {
        category.Editing = category.IsNew = false;
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync(new CancellationToken());
        return await GetAdminCategories();
    }

    public async Task<ServiceResponse<List<Category>>> DeleteCategory(int id)
    {
        Category category = await GetCategoryById(id);
        if (category == null)
        {
            return new ServiceResponse<List<Category>>
            {
                Success = false,
                Message = "Category not found."
            };
        }

        category.Deleted = true;
        await _dbContext.SaveChangesAsync(new CancellationToken());

        return await GetAdminCategories();
    }

    private async Task<Category> GetCategoryById(int id)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ServiceResponse<List<Category>>> GetAdminCategories()
    {
        var categories = await _dbContext.Categories
            .Where(c => !c.Deleted)
            .ToListAsync();
        return new ServiceResponse<List<Category>>
        {
            Data = categories
        };
    }

    public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
    {
        var categories = await _dbContext.Categories
            .Where(c => !c.Deleted && c.Visible)
            .ToListAsync();
        return new ServiceResponse<List<Category>>
        {
            Data = categories
        };
    }

    public async Task<ServiceResponse<List<Category>>> UpdateCategory(Category category)
    {
        var dbCategory = await GetCategoryById(category.Id);
        if (dbCategory == null)
        {
            return new ServiceResponse<List<Category>>
            {
                Success = false,
                Message = "Category not found."
            };
        }

        dbCategory.Name = category.Name;
        dbCategory.Url = category.Url;
        dbCategory.Visible = category.Visible;

        await _dbContext.SaveChangesAsync(new CancellationToken());

        return await GetAdminCategories();
    }
}