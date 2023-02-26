using BlazorAppDemo.Domain.Entity;

namespace BlazorAppDemo.Client.Services.AddressService;

public interface IAddressService
{
    Task<Address> GetAddress();
    Task<Address> AddOrUpdateAddress(Address address);
}