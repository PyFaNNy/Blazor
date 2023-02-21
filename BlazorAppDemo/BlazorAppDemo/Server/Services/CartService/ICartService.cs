using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain.Entity;

namespace BlazorAppDemo.Server.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems);
}