using System.ComponentModel.DataAnnotations.Schema;
using BlazorAppDemo.Domain.Entity.Products;

namespace BlazorAppDemo.Domain.Entity;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool Visible { get; set; } = true;
    public bool Deleted { get; set; } = false;
    [NotMapped]
    public bool Editing { get; set; } = false;
    [NotMapped]
    public bool IsNew { get; set; } = false;
    public List<Product> Products { get; set; } = new List<Product>();
}