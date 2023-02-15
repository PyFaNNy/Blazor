using BlazorAppDemo.Domain;

namespace BlazorAppDemo.Client.Services.ProductService;

public interface IProductService
{
    List<Product> Products { get; set; }
    Task GetProducts();
}