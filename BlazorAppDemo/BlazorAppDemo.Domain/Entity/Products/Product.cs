using System.ComponentModel.DataAnnotations.Schema;
using BlazorAppDemo.Domain.Entity.Orders;

namespace BlazorAppDemo.Domain.Entity.Products;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    public bool Featured { get; set; } = false;
    public List<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    public List<OrderItem> OrderItems { get; set; } = null;
    public bool Visible { get; set; } = true;
    public bool Deleted { get; set; } = false;
    [NotMapped]
    public bool Editing { get; set; } = false;
    [NotMapped]
    public bool IsNew { get; set; } = false;
}