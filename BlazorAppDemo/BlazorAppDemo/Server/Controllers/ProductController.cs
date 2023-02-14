using Microsoft.AspNetCore.Mvc;

namespace BlazorAppDemo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProduct()
    {
        return Ok(Products);
    }
}