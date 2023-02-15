﻿using BlazorAppDemo.Domain;
using BlazorAppDemo.Server.Services.CategoryService;
using BlazorAppDemo.Shared;
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
    
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> GetProducts()
    {
        var result = await _categoryService.GetCategoriesAsync();
        return Ok(result);
    }
}