using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain;
using BlazorAppDemo.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppDemo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
    {
        var result = await _categoryService.GetCategoriesAsync();
        return Ok(result);
    }
}