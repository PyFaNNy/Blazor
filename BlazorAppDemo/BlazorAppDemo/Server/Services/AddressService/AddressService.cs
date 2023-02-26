using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDemo.Server.Services.AddressService;
public class AddressService : IAddressService
{
    private readonly IBlazorDbContext _dbContext;
    private readonly IAuthService _authService;

    public AddressService(IBlazorDbContext context, IAuthService authService)
    {
        _dbContext = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address)
    {
        var response = new ServiceResponse<Address>();
        var dbAddress = (await GetAddress()).Data;
        if (dbAddress == null)
        {
            address.UserId = _authService.GetUserId();
            _dbContext.Addresses.Add(address);
            response.Data = address;
        }
        else
        {
            dbAddress.FirstName = address.FirstName;
            dbAddress.LastName = address.LastName;
            dbAddress.State = address.State;
            dbAddress.Country = address.Country;
            dbAddress.City = address.City;
            dbAddress.Zip = address.Zip;
            dbAddress.Street = address.Street;
            response.Data = dbAddress;
        }

        await _dbContext.SaveChangesAsync(new CancellationToken());

        return response;
    }

    public async Task<ServiceResponse<Address>> GetAddress()
    {
        int userId = _authService.GetUserId();
        var address = await _dbContext.Addresses
            .FirstOrDefaultAsync(a => a.UserId == userId);
        return new ServiceResponse<Address> { Data = address };
    }
}