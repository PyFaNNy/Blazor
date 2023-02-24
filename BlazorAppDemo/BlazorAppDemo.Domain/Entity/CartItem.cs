using BlazorAppDemo.Domain.Entity.Products;

namespace BlazorAppDemo.Domain.Entity;

public class CartItem
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int ProductTypeId { get; set; }
    public int Quantity { get; set; } = 1;
    
    public User? User { get; set; }
    public Product? Product { get; set; }
    public ProductType? ProductType { get; set; }
}