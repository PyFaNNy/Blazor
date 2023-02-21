using System.Security.Claims;
using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDemo.Server.Services.CartService;

public class CartService : ICartService
{
    private readonly IBlazorDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IBlazorDbContext dbcontext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbcontext;
        _httpContextAccessor = httpContextAccessor;
    }

    private int GetUserId() =>
        int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems)
    {
        var result = new ServiceResponse<List<CartProductResponse>>
        {
            Data = new List<CartProductResponse>()
        };

        foreach (var item in cartItems)
        {
            var product = await _dbContext.Products
                .Where(p => p.Id == item.ProductId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                continue;
            }

            var productVariant = await _dbContext.ProductVariants
                .Where(v => v.ProductId == item.ProductId
                            && v.ProductTypeId == item.ProductTypeId)
                .Include(v => v.ProductType)
                .FirstOrDefaultAsync();

            if (productVariant == null)
            {
                continue;
            }

            var cartProduct = new CartProductResponse
            {
                ProductId = product.Id,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Price = productVariant.Price,
                ProductType = productVariant.ProductType.Name,
                ProductTypeId = productVariant.ProductTypeId,
                Quantity = item.Quantity
            };

            result.Data.Add(cartProduct);
        }

        return result;
    }

    public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems)
    {
        cartItems.ForEach(cartItem => cartItem.UserId = GetUserId());
        _dbContext.CartItems.AddRange(cartItems);
        await _dbContext.SaveChangesAsync(new CancellationToken());

        return await GetDbCartProducts();
    }

    public async Task<ServiceResponse<int>> GetCartItemsCount()
    {
        var count = await _dbContext.CartItems.Where(x => x.UserId == GetUserId()).CountAsync();

        return new ServiceResponse<int>
        {
            Data = count
        };
    }

    public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts()
    {
        return await GetCartProducts(await _dbContext.CartItems
            .Where(x => x.UserId == GetUserId())
            .ToListAsync());
    }

    public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
    {
        cartItem.UserId = GetUserId();

        var sameItem = await _dbContext.CartItems
            .FirstOrDefaultAsync(x => x.ProductId == cartItem.ProductId &&
                                      x.ProductTypeId == cartItem.ProductTypeId &&
                                      x.UserId == cartItem.UserId);
        if (sameItem == null)
        {
            _dbContext.CartItems.Add(cartItem);
        }
        else
        {
            sameItem.Quantity += cartItem.Quantity;
        }

        await _dbContext.SaveChangesAsync(new CancellationToken());
        return new ServiceResponse<bool> {Data = true};
    }
}