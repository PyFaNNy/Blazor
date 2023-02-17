﻿using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain;

namespace BlazorAppDemo.Server.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<List<Product>>> GetProductsAsync();
    Task<ServiceResponse<Product>> GetProductAsync(int id);
    Task<ServiceResponse<List<Product>>> GetProductByCategoryAsync(string category);
    Task<ServiceResponse<PaginatedList<Product>>> SearchProducts(string searchText, int pageIndex,
        int pageSize);
    Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
    Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
}