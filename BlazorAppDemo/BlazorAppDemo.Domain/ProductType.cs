using System.Text.Json.Serialization;

namespace BlazorAppDemo.Domain;

public class ProductType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [JsonIgnore]
    public List<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}