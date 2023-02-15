using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDemo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IBlazorDbContext _dbContext;
    public ProductController(IBlazorDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProduct()
    {
        var result = await _dbContext.Products.ToListAsync();
        return Ok(result);
    }
}