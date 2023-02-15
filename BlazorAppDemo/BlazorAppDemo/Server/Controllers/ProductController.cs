﻿using BlazorAppDemo.Client.Services.ProductService;
using BlazorAppDemo.Domain;
using BlazorAppDemo.Shared;
using Microsoft.AspNetCore.Mvc;
using IProductService = BlazorAppDemo.Server.Services.ProductService.IProductService;

namespace BlazorAppDemo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
    {
        var result = await _productService.GetProductsAsync();
        return Ok(result);
    }
    
    [HttpGet("{productId}")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProduct(int productId)
    {
        var result = await _productService.GetProductAsync(productId);
        return Ok(result);
    }
}