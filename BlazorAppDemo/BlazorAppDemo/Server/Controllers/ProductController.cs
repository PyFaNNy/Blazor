using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain;
using BlazorAppDemo.Domain.Entity;
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
    {
        var result = await _productService.GetProductsAsync();
        return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpGet("{productId}")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProduct(int productId)
    {
        var result = await _productService.GetProductAsync(productId);
        return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="categoryUrl"></param>
    /// <returns></returns>
    [HttpGet("category/{categoryUrl}")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategory(string categoryUrl)
    {
        var result = await _productService.GetProductByCategoryAsync(categoryUrl);
        return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="searchText"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("search/{searchText}/{pageIndex}/{pageSize}")]
    public async Task<ActionResult<ServiceResponse<PaginatedList<Product>>>>
        SearchProducts(string searchText,
            int pageIndex = 1,
            int pageSize = 10)
    {
        var result = await _productService.SearchProducts(searchText, pageIndex, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="searchText"></param>
    /// <returns></returns>
    [HttpGet("searchsuggestions/{searchText}")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> SearchSuggestions(string searchText)
    {
        var result = await _productService.GetProductSearchSuggestions(searchText);
        return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("feature")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProduct()
    {
        var result = await _productService.GetFeaturedProducts();
        return Ok(result);
    }
}