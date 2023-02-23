using BlazorAppDemo.Domain.Entity.Orders;

namespace BlazorAppDemo.Domain.Entity;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public CartItem CartItem { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
}