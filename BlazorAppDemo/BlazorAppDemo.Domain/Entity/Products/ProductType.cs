using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BlazorAppDemo.Domain.Entity.Orders;

namespace BlazorAppDemo.Domain.Entity.Products;

public class ProductType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [NotMapped]
    public bool Editing { get; set; } = false;
    [NotMapped]
    public bool IsNew { get; set; } = false;
    [JsonIgnore]
    public List<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    public List<OrderItem> OrderItems { get; set; } = null;
}