using System.Text.Json.Serialization;

namespace BlazorAppDemo.Domain.Entity;

public class ProductType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [JsonIgnore]
    public List<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
}