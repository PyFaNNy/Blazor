using BlazorAppDemo.Client;
using BlazorAppDemo.Client.Services.CartService;
using BlazorAppDemo.Client.Services.CategoryService;
using BlazorAppDemo.Client.Services.ProductService;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IProductService, ProductService>(); 
builder.Services.AddScoped<ICategoryService, CategoryService>(); 
builder.Services.AddScoped<ICartService, CartService>();

await builder.Build().RunAsync();
