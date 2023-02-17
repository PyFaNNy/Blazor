using AutoMapper;

namespace BlazorAppDemo.Application.Mappings;

public interface IMapFrom
{
    void Mapping(Profile profile) => profile.CreateMap(GetType(), GetType());
}