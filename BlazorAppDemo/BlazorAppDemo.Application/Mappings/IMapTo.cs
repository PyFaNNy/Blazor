using AutoMapper;

namespace BlazorAppDemo.Application.Mappings;

public interface IMapTo
{
    public void Mapping(Profile profile) => profile.CreateMap(GetType(), GetType());
}